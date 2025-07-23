using Microsoft.Extensions.Configuration;
using MovieProject.Application.Interfaces;
using MovieProject.Application.Interfaces.Redis;
using StackExchange.Redis;

namespace MovieProject.Persistance.Repositories
{
    public class RedisConnectionFactory : IRedisConnectionFactory, IDisposable
    {
        private IConnectionMultiplexer _connection;
        private readonly string _connectionString;
        private DateTime _lastConnectionAttempt = DateTime.MinValue;
        private readonly TimeSpan _reconnectCooldown = TimeSpan.FromMinutes(1); // 1 dakika bekle
        private readonly SemaphoreSlim _reconnectSemaphore = new(1, 1);

        public bool IsRedisAvailable { get; private set; }

        public RedisConnectionFactory(IConfiguration configuration)
        {
            _connectionString = configuration.GetValue<string>("Redis:ConnectionString") ?? "localhost:6379";
            TryConnect();
        }

        private void TryConnect()
        {
            try
            {
                Console.WriteLine($"Redis'e bağlanma denemesi: {_connectionString}");

                var options = ConfigurationOptions.Parse(_connectionString);
                options.ConnectTimeout = 2000;    // 2 saniye
                options.SyncTimeout = 2000;       // 2 saniye
                options.AbortOnConnectFail = false;  // İlk hatada uygulamayı çökertme

                _connection = ConnectionMultiplexer.Connect(options);

                // Gerçekten bağlanmış mı kontrol et
                if (_connection.IsConnected)
                {
                    // Event'leri dinle
                    _connection.ConnectionFailed += OnConnectionFailed;
                    _connection.ConnectionRestored += OnConnectionRestored;

                    IsRedisAvailable = true;
                    Console.WriteLine("✅ Redis bağlantısı BAŞARILI!");
                }
                else
                {
                    IsRedisAvailable = false;
                    Console.WriteLine("❌ Redis bağlantısı kurulamıyor (IsConnected = false).");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Redis bağlantısı HATA: {ex.Message}");
                _connection = null;
                IsRedisAvailable = false;
            }

            _lastConnectionAttempt = DateTime.UtcNow;
        }

        public IConnectionMultiplexer GetConnection()
        {
            // Bağlantı varsa döndür
            if (_connection?.IsConnected == true)
                return _connection;

            // Otomatik yeniden bağlanma (arka planda)
            _ = Task.Run(async () => await TryReconnectAsync());

            return _connection;
        }

        public async Task<bool> TryReconnectAsync()
        {
            // Çok sık deneme yapma (cooldown kontrolü)
            if (DateTime.UtcNow - _lastConnectionAttempt < _reconnectCooldown)
                return IsRedisAvailable;

            await _reconnectSemaphore.WaitAsync();
            try
            {
                if (!IsRedisAvailable || _connection?.IsConnected != true)
                {
                    Console.WriteLine("🔄 Redis yeniden bağlanma denemesi...");

                    _connection?.Dispose();

                    TryConnect();
                }

                return IsRedisAvailable;
            }
            finally
            {
                _reconnectSemaphore.Release();
            }
        }

        private void OnConnectionFailed(object sender, ConnectionFailedEventArgs e)
        {
            Console.WriteLine($"⚠️ Redis bağlantısı koptu: {e.FailureType} - {e.Exception?.Message}");
            IsRedisAvailable = false;
        }

        private void OnConnectionRestored(object sender, ConnectionFailedEventArgs e)
        {
            Console.WriteLine("✅ Redis bağlantısı yeniden kuruldu!");
            IsRedisAvailable = true;
        }

        public void Dispose()
        {
            _connection?.Dispose();
            _reconnectSemaphore?.Dispose();
        }
    }
}
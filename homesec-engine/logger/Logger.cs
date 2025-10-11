using TestWebAPI1.manager;

namespace TestWebAPI1.logger;

public class Logger
{
    public class NamedLogger
    {
        private readonly string _name;
        
        private readonly Logger _logger;

        public NamedLogger(string name)
        {
            _name = name;
        }
        
        public void Trace(string message)
        {
            _logger.Trace(_name, message);
        }

        public void Debug(string message)
        {
            _logger.Debug(_name, message);
        }

        public void Info(string message)
        {
            _logger.Info(_name, message);
        }

        public void Warn(string message)
        {
            _logger.Warn(_name, message);
        }

        public void Error(string message)
        {
            _logger.Error(_name, message);
        }
    }
    
    /// <summary>
    /// Singleton instance
    /// </summary>
    private static Logger? _instance = null;

    public static Logger GetInstance()
    {
        _instance ??= new Logger();
        return _instance;
    }

    public NamedLogger GetNamedLogger(string name)
    {
        return new NamedLogger(name);
    }

    public void Trace(string module, string message)
    {
        
    }

    public void Debug(string module, string message)
    {
        
    }

    public void Info(string module, string message)
    {
        
    }

    public void Warn(string module, string message)
    {
        
    }

    public void Error(string module, string message)
    {
        
    }
}
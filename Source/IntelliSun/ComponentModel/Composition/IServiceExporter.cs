using System;

namespace IntelliSun.ComponentModel.Composition
{
    public interface IServiceExporter
    {
        void ExportService(Type serviceType, object service, string key);
    }
}

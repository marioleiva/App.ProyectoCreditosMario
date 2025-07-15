using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Service.Implementacion.Responses
{
    public class GeneralResponseDTO
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public TipoAlerta TipoAlerta { get; set; } = TipoAlerta.NoVisible;
    }
    public class GeneralResponse<T>
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public TipoAlerta TipoAlerta { get; set; } = TipoAlerta.NoVisible;
        public T? Data { get; set; } = default(T);
    }

    public enum TipoAlerta
    {
        NoVisible = 0,
        Informativo = 1,
        Restrictivo = 2
    }
}

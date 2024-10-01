namespace BlackFormBackend.Models.DTOs.Response;

public class Response<T>
{
    public bool Estado { get; set; }
    public string Mensaje { get; set; }
    public T? Cuerpo { get; set; }
}
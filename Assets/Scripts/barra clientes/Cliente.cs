public enum ClienteEstado
{
    Acercandose,
    EnLocal
}

[System.Serializable]
public class Cliente
{
    public ClienteEstado estado;
    public TipoCliente tipo;
}
public enum TipoCliente
{
    Cliente1,
    Cliente2,
    Cliente3
}
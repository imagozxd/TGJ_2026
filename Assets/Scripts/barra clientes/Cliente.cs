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
    SusyDiaz,
    Dina,
    Zumba
}
using System.Collections.Generic;

[System.Serializable]
public class Pedido
{
    public TipoPlato plato;

    public bool pideBebida = true;

    public Bebida bebida;
    public List<Crema> cremas;
}

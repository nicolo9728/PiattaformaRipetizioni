using System;

namespace RipetizioniApp.Models.Sessioni.ModalitaAccettazione;

public class DocenteDeveAccettare : IModalitaAccettazione
{
    private StatusAccettazione status = new InAttesa();

    public StatusAccettazione getStatus()
        => status;

    public MetodoPagamento MetodoProposto { get; }

    public DocenteDeveAccettare(MetodoPagamento metodoProposto)
        => MetodoProposto = metodoProposto;

    public void Accetta()
    {
        if (status is InAttesa)
            status = new Accettata(MetodoProposto);
    }

    public void Rifiuta()
    {
        if (status is InAttesa)
            status = new NonAccettata("Il docente non ha accettato la richiesta");
    }

}

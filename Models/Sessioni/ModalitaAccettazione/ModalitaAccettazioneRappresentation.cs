using System;
using System.Reflection;

namespace RipetizioniApp.Models.Sessioni.ModalitaAccettazione;

public record ModalitaAccettazioneRappresentation(
    Type Modalita,
    Type Status,
    string? Causa,
    MetodoPagamentoRappresentation? MetodoPagamentoAccettato,
    MetodoPagamentoRappresentation? MetodoPagamentoProposto);

public static class ConversioniModalita
{

    extension(IModalitaAccettazione modalita)
    {
        public ModalitaAccettazioneRappresentation ToRappresentation()
            => new ModalitaAccettazioneRappresentation(
                    modalita.GetType(),
                    modalita.getStatus().GetType(),
                    modalita.getStatus() is NonAccettata n ? n.Causa : null,
                    modalita.getStatus() is Accettata a ? a.MetodoAccettato.toRappresentation() : null,
                    modalita is DocenteDeveAccettare d ? d.MetodoProposto.toRappresentation() : null
                    );
    }

    extension(ModalitaAccettazioneRappresentation rappresentation)
    {
        public IModalitaAccettazione ToModel()
        {
            IModalitaAccettazione modalita = (IModalitaAccettazione)Activator.CreateInstance(rappresentation.Modalita, rappresentation.MetodoPagamentoProposto != null ? [rappresentation.MetodoPagamentoProposto] : [])!;

            if (modalita is ConcordareSulPrezzo)
            {
                /*
                typeof(ConcordareSulPrezzo)
                    .GetField("proposte", BindingFlags.Instance | BindingFlags.NonPublic)!
                    .SetValue(modalita, rappresentation.Proposte);
                */
            }

            StatusAccettazione status = (StatusAccettazione)Activator.CreateInstance(rappresentation.Status, new Object?[]
            {
                rappresentation.Causa,
                rappresentation.MetodoPagamentoAccettato
            }.Select((p) => p != null))!;

            rappresentation.Modalita
                .GetField("status", BindingFlags.Instance | BindingFlags.NonPublic)!
                .SetValue(modalita, status);

            return modalita;
        }

        public static Type GetTypeFromString(string nome) => new Type[] {
            typeof(ConcordareSulPrezzo),
            typeof(DocenteDeveAccettare)
        }.First((t) => t.Name == nome);
    }

}

public static class ConversioniStatus
{

    extension(StatusAccettazione status)
    {
        public static Type GetTypeFromString(string nome) => new Type[]
        {
            typeof(NonAccettata),
            typeof(Accettata),
            typeof(InAttesa)
        }.First((t) => t.Name == nome);
    }
}

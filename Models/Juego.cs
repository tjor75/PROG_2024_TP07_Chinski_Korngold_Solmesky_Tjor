public static class Juego
{
    const int SUMA_PUNTAJE = 500;
    public static string username { get; private set; }
    public static int puntajeActual { get; private set; }
    public static int cantidadPreguntasCorrectas { get; private set; }
    private static List<Pregunta> preguntas { get; set; }
    private static List<Respuesta> respuestas { get; set; }

    public static void InicializarJuego()
    {
        username = string.Empty;
        puntajeActual = 0;
        cantidadPreguntasCorrectas = 0;
        preguntas.Clear();
        respuestas.Clear();
    }

    public static List<Categoria> ObtenerCategorias()
    {
        return BD.ObtenerCategorias();
    }

    public static List<Dificultad> ObtenerDificultades()
    {
        return BD.ObtenerDificultades();
    }

    public static void CargarPartida(string nuevoUsername, int dificultad, int categoria)
    {
        username = nuevoUsername; // Preguntar para que el username
        preguntas = BD.ObtenerPreguntas(dificultad, categoria);
        respuestas = BD.ObtenerRespuestas(preguntas);
    }

    public static Pregunta? ObtenerProximaPregunta() {
        if (preguntas.Count > 0) {
            Random rnd = new Random();
            int valor_random = rnd.Next(preguntas.Count);
            return preguntas[valor_random];
        }
        
        // cambiar bd para q las preguntas puedan ser nulas(sin cant)
        return null;
    
    }
    public static List<Respuesta> ObtenerProximasRespuestas(int idPregunta)
    {
        List<Respuesta> proximasRespuestas = new List<Respuesta>();
        foreach (Respuesta respuesta in respuestas)
            if (respuesta.IdPregunta == idPregunta)
                proximasRespuestas.Add(respuesta);
        return proximasRespuestas;
    }

    public static bool VerificarRespuesta(int idPregunta, int idRespuesta)
    {
        int posRespuesta = BuscarRespuesta(idPregunta, idRespuesta);
        bool correcta = posRespuesta != -1 && respuestas[posRespuesta].Correcta;

        if (correcta)
        {
            puntajeActual += SUMA_PUNTAJE;
            cantidadPreguntasCorrectas++;
            preguntas.RemoveAt(posRespuesta);
        }

        return correcta;
    }

    private static int BuscarRespuesta(int idPregunta, int idRespuesta)
    {
        int posRespuesta = respuestas.Count - 1;
        while (posRespuesta >= 0 && idPregunta != respuestas[posRespuesta].IdPregunta && idRespuesta != respuestas[posRespuesta].IdRespuesta)
            posRespuesta--;
        return posRespuesta;
    }
}
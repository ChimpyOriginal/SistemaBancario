namespace SistemaBancario
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Menu(); // Llama al método Menu para iniciar
        }
        static void Menu() // Método para inicializar y mostrar el menú
        {
            try
            {
                Menu menu = new Menu(); // Hace una instancia de la clase Menú
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message); // Identifica las excepciones y muestra un mensaje de error
            }
        }
    }
}
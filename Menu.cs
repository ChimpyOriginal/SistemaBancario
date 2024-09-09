using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace SistemaBancario
{
    internal class Menu
    {
        static Banco banco = new Banco();
        static Cliente cliente;
        List<Opcion> opciones; 
        //private Cliente cliente;
        public Menu()
        {
            opciones = new List<Opcion>()
            {
                new Opcion("Agregar cliente.",AgregarCliente),
                new Opcion("Agregar una cuenta a un cliente.",AgregarCuentas),
                new Opcion("Consultar la lista de cuentas del cliente mediante su ID.",ConsultarCuentas),
                new Opcion("Consultar nombre del cliente por su ID", ObtenerClientePorId),
                new Opcion("Realizar depósito.", Depositar),
                new Opcion("Realizar retiro.", Retirar),
                new Opcion("Realizar transferencia entre cuentas de un mismo cliente.", Transferir),
                new Opcion("Consultar saldo.",ConsultarSaldo),
                new Opcion("Salir.\n",()=>Environment.Exit(0))

            };
            while (true)
            {
                Console.WriteLine("BIENVENIDO AL BANCO RKMJJ.\n");
                MostrarMenu();
                ElegirOpcion();
            }
        }

        public void MostrarMenu()
        {
            for (int i = 0; i < opciones.Count; i++)
            {
                Console.WriteLine((i + 1) + "." + opciones[i].Message);
            }
        }
        public void ElegirOpcion()
        {
            Console.Write("Elige una opción: ");
            int numOpcion = Convert.ToInt32(Console.ReadLine());
            numOpcion--;
            Console.Clear();
            if (numOpcion < opciones.Count)
            {
                opciones[numOpcion].Action.Invoke();
            }
            else
            {
                Console.WriteLine("\nOpción no válida. Por favor, elige una registra en el menú.");
                Continuar();
            }
            Console.Clear();
        }
        public void Continuar()
        {
            Console.WriteLine("\nPresione cualquier tecla para continuar...");
            Console.ReadLine();
        }

        //MÉTODOS PARA LAS OPCIONES DEL MENÚ
        public void AgregarCliente()
        {
            Console.Write("Nombre del cliente: ");
            string nombre = Console.ReadLine();

            Console.Write("Apellidos del cliente: ");
            string apellido = Console.ReadLine();

            Console.Write("ID del cliente: ");
            string idCliente = Console.ReadLine();

            banco.AgregarCliente(nombre, apellido, idCliente);

            Console.WriteLine("\nEl cliente ha sido registrado.");
            Continuar();

        }
        public void AgregarCuentas()
        {
            Console.Write("ID del cliente propietario de la cuenta : ");
            string idPropietario = Console.ReadLine();

            Console.Write("Numero de cuenta que se le asignará: ");
            string numeroCuenta = Console.ReadLine();

            Console.Write("Saldo inicial de la cuenta: ");
            decimal saldo = decimal.Parse(Console.ReadLine());

            Cliente cliente = banco.ObtenerClientePorId(idPropietario);

            if (cliente == null)
            {
                Console.WriteLine("\nEl ID no coincide con ningún cliente registrado.");
                Continuar();
                return;
            }

            cliente.AgregarCuentas(idPropietario, numeroCuenta, saldo);

            Console.WriteLine("\nLa cuenta ha sido registrada.");
            Continuar();
        }
        public void ConsultarCuentas()
        {
            Console.Write("Ingrese el ID del cliente: ");
            string idCliente = Console.ReadLine();
            Cliente cliente = banco.ObtenerClientePorId(idCliente);

            if (cliente == null)
            {
                Console.WriteLine("\nEl ID no coincide con ningún cliente registrado.");
                Continuar();
                return;
            }
            short status = cliente.ConsultarCuentas(idCliente);
            if (status == 2)
            {
                Console.WriteLine("\nEl cliente no tiene asociada ninguna cuenta.");
            }
            else if (status == 3)
            {
                Console.WriteLine("\nAún no se ha registrado ninguna cuenta en el sistema.");
            }
            Continuar();
        }
        public void ObtenerClientePorId()
        {
            Console.Write("Ingrese el ID del cliente: ");
            string idCliente = Console.ReadLine();

            Cliente cliente = banco.ObtenerClientePorId(idCliente);
            if (cliente != null)
            {
                banco.ImprimirClientePorId(cliente);
            }
            else
            {
                Console.WriteLine("\nEl ID no coincide con ningún cliente registrado. ");
            }
            Continuar();
        }

        public void Depositar()
        {
            Console.Write("Número de cuenta a la que se realizará el depósito: ");
            string numeroCuentaDestino = Console.ReadLine();

            Console.Write("ID del destinatario: ");
            string idDestinatario = Console.ReadLine();

            Console.Write("Monto a depositar: ");
            decimal monto = decimal.Parse(Console.ReadLine());

            short status = banco.Depositar(numeroCuentaDestino, idDestinatario, monto);
            if (status == 1)
            {
                Console.WriteLine("\nSe realizó el depósito con éxito.");
            }
            else if (status == 2)
            {
                Console.WriteLine("\nEl numero de cuenta del destinatario no se encuentra registrado.");
            }
            else if (status== 3)
            {
                Console.WriteLine("\nEl ID del destinatario no se encuentra registrado.");
            }
            Continuar();
        }

        public void Retirar()
        {
            Console.Write("ID del cliente: ");
            string idCliente = Console.ReadLine();

            Console.Write("Numero de cuenta: ");
            string numeroCuenta = Console.ReadLine();

            Console.Write("Monto a retirar: ");
            decimal monto = decimal.Parse(Console.ReadLine());

            
            short status = banco.Retirar(idCliente, numeroCuenta, monto);

            if (status == 1)
            {
                Console.WriteLine("\nEl retiro ha sido exitoso.");
            }
            else if (status == 2)
            {
                Console.WriteLine("\nEl saldo de la cuenta es insuficiente.");
            }
            else if (status == 3)
            {
                Console.WriteLine("\nNumero de cuenta no encontrado.");
            }
            else
            {
                Console.WriteLine("\nEl ID no coincide con ningún cliente registrado.");
            }
            Continuar();
        }
        public void Transferir()
        {
            Console.Write("ID del cliente de la cuenta de origen: ");
            string idClienteOrigen = Console.ReadLine();

            Console.Write("Número de cuenta desde la que se hará la transferencia: ");
            string cuentaOrigen = Console.ReadLine();

            Console.Write("Número de cuenta destinataria de la transferencia: ");
            string cuentaDestino = Console.ReadLine();

            Console.Write("Monto a transferir: ");
            decimal monto = decimal.Parse(Console.ReadLine());

            short status = banco.Transferir(idClienteOrigen, cuentaOrigen, cuentaDestino, monto);
            if (status == 1)
            {
                Console.WriteLine("\nTransferencia exitosa.");
            }
            else if(status == 2)
            {
                Console.WriteLine("\nFondos insuficientes en la cuenta de origen.");
            }
            else if (status == 3)
            {
                Console.WriteLine("\nUna o ambas cuentas del cliente no fueron encontradas.");
            }
            else
            {
                Console.WriteLine("\nEl ID no coincide con ningún cliente registrado.");
            }

            Continuar();
        }
        public void ConsultarSaldo()
        {
            Console.Write("Número de identificación del cliente: ");
            string idCliente = Console.ReadLine();

            Console.Write("Número de cuenta: ");
            string numeroCuenta = Console.ReadLine();

            short status = banco.ConsultarSaldo(idCliente, numeroCuenta);
            if (status == 2)
            {
                Console.WriteLine("\nNumero de cuenta no encontrado.");
            }
            else if (status ==3)
            {
                Console.WriteLine("\nEl ID no coincide con ningún cliente registrado.");
            }
            Continuar();
        }
    }
}

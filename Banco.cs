using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SistemaBancario
{
    internal class Banco
    {
        private List<Cliente> clientes;
        public Banco()
        {
            clientes = new List<Cliente>();
        }

        public void AgregarCliente(string nombre, string apellido, string idCliente)
        {
            Cliente cliente = new Cliente(nombre, apellido, idCliente);
            clientes.Add(cliente);
        }


        public short Depositar(string numeroCuentaDestino, string idDestinatario, decimal monto)
        {
            Cliente cliente = ObtenerClientePorId(idDestinatario);

            if (cliente !=null)
            {
                CuentaBancaria cuenta = cliente.ObtenerCuentaPorNum(numeroCuentaDestino);
                if (cuenta != null)
                {
                    cuenta.Depositar(monto);
                    return 1;//Se realizó el depósito con éxito.
                }
                else 
                { 
                    return 2; 
                }//La cuenta del destinatario no se encuentra registrada.
            }
            else 
            { 
                return 3; 
            }// El ID del destinatario no se encuentra registrado.
        }

        public short Retirar(string idCliente, string numeroCuenta, decimal monto)
        {

            Cliente cliente = ObtenerClientePorId(idCliente);

            if (cliente != null)
            {
                CuentaBancaria cuenta = cliente.ObtenerCuentaPorNum(numeroCuenta);

                if (cuenta != null)
                {
                    short status= cuenta.Retirar(monto);
                    return status; //retirho exitoso
                }
                else
                {
                    return 3; // Console.WriteLine("Cuenta no encontrada.");
                }
            }
            else
            {
                return 4;// Console.WriteLine("Cliente no encontrado.");
            }

        }

        public short Transferir(string idClienteOrigen, string cuentaOrigen, string cuentaDestino, decimal monto) //programando
        {
            Cliente cliente = ObtenerClientePorId(idClienteOrigen);

            if (cliente != null)
            {
                CuentaBancaria origen = cliente.ObtenerCuentaPorNum(cuentaOrigen);
                CuentaBancaria destino = cliente.ObtenerCuentaPorNum(cuentaDestino);

                if (origen != null && destino != null)
                {
                    if (origen.Saldo >= monto)
                    {
                        origen.Retirar(monto);
                        destino.Depositar(monto);
                        return 1;
                    }
                    else
                    {
                        return 2; // Console.WriteLine("Fondos insuficientes en la cuenta de origen.");
                    }
                }
                else
                {
                    return 3; // Console.WriteLine("Una o ambas cuentas no fueron encontradas.");
                }
            }
            else
            {
                return 4;//Console.WriteLine("Cliente no encontrado.");
            }
        }

        public short ConsultarSaldo(string idCliente, string numeroCuenta)
        {
            Cliente cliente = ObtenerClientePorId(idCliente);

            if (cliente != null)
            {
                CuentaBancaria cuenta = cliente.ObtenerCuentaPorNum(numeroCuenta);

                if (cuenta != null)
                {
                    cuenta.ConsultarSaldo();
                    return 1;
                }
                else
                {
                    return 2; // Console.WriteLine("Cuenta no encontrada.");
                }
            }
            else
            {
                return 3;//Console.WriteLine("Cliente no encontrado.");
            }
        }
        public Cliente ObtenerClientePorId(string id)
        {
            foreach (var cliente in clientes)
            {
                if (cliente.IdCliente == id)
                {
                    return cliente; 
                }
            }
            return null; 
        }
        public short ImprimirClientePorId(Cliente cliente)
        {
            foreach (var clienteInfo in clientes)
            {
                Console.WriteLine("Nombre: " + cliente.Nombre + "\nApellido: " + cliente.Apellido +"\n");
            }
            return 1;
        }
            
    }

}


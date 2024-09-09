using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaBancario
{
    internal class Cliente
    {
        private string nombre;
        private string apellido;
        private string idCliente;
        private List<CuentaBancaria> cuentas;

        public Cliente(string nombre, string apellido, string idCliente)
        {
            this.nombre = nombre;
            this.apellido = apellido;
            this.idCliente = idCliente;
            cuentas = new List<CuentaBancaria>();
        }

        public string Nombre { get {  return this.nombre; } set {  this.nombre = value; } }
        public string Apellido { get {  return this.apellido; } set {  this.apellido = value; } }
        public string IdCliente { get {  return this.idCliente; } set {  this.idCliente = value; } }
        public List<CuentaBancaria> Cuentas { get { return cuentas; } set { cuentas = value ?? new List<CuentaBancaria>(); } }

        public void AgregarCuentas(string idPpropietario, string numeroCuenta, decimal saldo) //Método para asociar una nueva cuenta bancaria al cliente
        {
            CuentaBancaria cuenta = new CuentaBancaria(idPpropietario, numeroCuenta, saldo); //dudas sobre si esta línea debería estar en este método y en esta clase
            cuentas.Add(cuenta);
        }

        public short ConsultarCuentas(string idCliente)
        {
            bool clienteTieneCuentas = false;

            if (cuentas.Count != 0)
            {
                foreach (var cuenta in cuentas)
                {
                    if (cuenta.IdPropietario == idCliente)
                    {
                        Console.WriteLine("Número de cuenta: " + cuenta.NumeroCuenta + "\nSaldo disponible: $" + cuenta.Saldo + "\n");
                        clienteTieneCuentas = true; // Indica que se encontró al menos una cuenta
                    }
                }

                if (clienteTieneCuentas)
                {
                    return 1; // El cliente tiene al menos una cuenta
                }
                else
                {
                    return 2; // No se encontraron cuentas para este cliente
                }
            }
            else
            {
                return 3; // No hay cuentas registradas en el sistema
            }
        }
        public CuentaBancaria ObtenerCuentaPorNum(string numeroCuenta)
        {
            foreach (var cuenta in cuentas)
            {
                if (cuenta.NumeroCuenta == numeroCuenta)
                {
                    return cuenta;
                }
            }
            return null;
        }

        
    }
}

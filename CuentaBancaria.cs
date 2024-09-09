using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaBancario
{
    internal class CuentaBancaria
    {
        private string idPropietario;
        private string numeroCuenta;
        private decimal saldo;

        public CuentaBancaria( string idPropietario, string numeroCuenta, decimal saldo)
        {
            this.idPropietario = idPropietario;
            this.numeroCuenta = numeroCuenta;
            this.saldo = saldo;
        }
        public string IdPropietario { get { return this.idPropietario; } set { this.idPropietario = value; } }
        public string  NumeroCuenta {  get { return this.numeroCuenta; } set { this.numeroCuenta = value;} }
        public decimal Saldo { get { return this.saldo; } set { this.saldo = value; } }

        
        public short Depositar(decimal monto)
        {
            if (monto > 0)
            {
                Saldo += monto;
                return 1;//deposito exitoso
            }
            else
            {
                return -1; //monto debe ser positivo
            }
        }
        public short Retirar(decimal monto)
        {
            if (monto > 0 && Saldo > monto)
            {
                Saldo -= monto;
                return 1;//retiroexitoso
            }
            return 2;//el saldo es insuficiente
        }
        public void ConsultarSaldo()
        {
            Console.WriteLine("El saldo en la cuenta es de: $" + Saldo);
        }

        
        
    }
}

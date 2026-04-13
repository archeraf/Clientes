using System;
using System.Collections.Generic;
using System.Web.Mvc;
using FI.AtividadeEntrevista.BLL;
using FI.AtividadeEntrevista.BLL.Interfaces;
using FI.AtividadeEntrevista.DAL;
using WebAtividadeEntrevista.Controllers;

namespace WebAtividadeEntrevista.Infrastructure
{
    /// <summary>
    /// Provedor de resolução de dependências para o pipeline do ASP.NET MVC.
    /// </summary>
    public class SimpleDependencyResolver : IDependencyResolver
    {
        public object GetService(Type serviceType)
        {
            // Registro do ClienteController e suas dependências
            if (serviceType == typeof(ClienteController))
            {
                var daoCliente = new DaoCliente();
                var daoBenef = new DaoBeneficiario();
                var boCliente = new BoCliente(daoCliente);
                var boBenef = new BoBeneficiario(daoBenef);
                return new ClienteController(boCliente, boBenef);
            }

            // Registro das BLLs caso sejam solicitadas individualmente
            if (serviceType == typeof(IBoCliente))
                return new BoCliente(new DaoCliente());

            if (serviceType == typeof(IBoBeneficiario))
                return new BoBeneficiario(new DaoBeneficiario());

            // Instancia tipos que possuem construtor sem parâmetros.
            if (typeof(IController).IsAssignableFrom(serviceType))
            {
                try
                {
                    return Activator.CreateInstance(serviceType);
                }
                catch
                {
                    return null;
                }
            }

            return null;
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return new List<object>();
        }
    }
}

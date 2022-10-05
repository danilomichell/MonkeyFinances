﻿using Microsoft.EntityFrameworkCore;
using MonkeyFinances.Core;
using MonkeyFinances.Core.Data;
using MonkeyFinances.Financas.Api.Models.Entities;
using MonkeyFinances.Financas.Api.Models.Enuns;

namespace MonkeyFinances.Financas.Api.Data.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        void Adicionar(User user);
        void Update(User user);
        Task<User?> ObterPorEmail(string email);
        Task<Tipo?> ObterTipos(EnumTipo tipo);
        Task<FormaPagamento?> ObterFormaPagamento(EnumFormaPagamento tipo);
    }
    public class UserRepository : IUserRepository
    {
        private readonly FinancasContext _context;

        public UserRepository(FinancasContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public void Adicionar(User user)
        {
            _context.Users.Add(user);
        }

        public async Task<User?> ObterPorEmail(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x!.Email == email);
        }

        public void Update(User user)
        {
            _context.Update(user);
        }

        public async Task<Tipo?> ObterTipos(EnumTipo tipo)
        {
            return await _context.Tipos.FirstOrDefaultAsync(x => x.Descricao.Equals(tipo.GetDescription()));
        }

        public async Task<FormaPagamento?> ObterFormaPagamento(EnumFormaPagamento tipo)
        {
            return await _context.FormaPagamentos.FirstOrDefaultAsync(x => x.Descricao.Equals(tipo.GetDescription()));
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing)
            {
                _context.Dispose();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using curso_ef_core.Domain;
using curso_ef_core.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace curso_ef_core
{
  class Program
  {
    static void Main(string[] args)
    {
      // Data.ApplicationContext db = new Data.ApplicationContext();

      // bool existeMigracaoPendente = db.Database.GetPendingMigrations().Any();

      // if (existeMigracaoPendente)
      // {
      //   Console.Write("Migrações pendentes foram encontradas.");
      // }

      // InserirDadosEmMassa();
      // ConsultarDados();
      // CadastrarPedido();
      // ConsultarPedidoCarregamentoAdiantado();
      // AtualizarDados();
      // AtualizarDadosDesconectos();
      // RemoverRegistro();
      RemoverRegistroDesconecto();
    }

    private static void RemoverRegistroDesconecto()
    {
      DbContext db = new Data.ApplicationContext();

      int cliente_id = 5;
      Cliente cliente = new Cliente { Id = cliente_id };

      db.Entry<Cliente>(cliente).State = EntityState.Deleted;

      db.SaveChanges();
    }

    private static void RemoverRegistro()
    {
      DbContext db = new Data.ApplicationContext();

      int cliente_id = 3;
      Cliente cliente = db.Set<Cliente>().Find(cliente_id);

      if (Object.Equals(cliente, null))
      {
        Console.WriteLine($"Cliente de id {cliente_id} não encontrado.");
        return;
      }

      // db.Set<Cliente>().Remove(cliente);
      // db.Remove(cliente);

      db.Entry(cliente).State = EntityState.Deleted;

      db.SaveChanges();
    }

    private static void AtualizarDadosDesconectos()
    {
      DbContext db = new Data.ApplicationContext();

      int cliente_id = 3;
      // Cliente cliente = db.Set<Cliente>().Find(cliente_id);

      // if (Object.Equals(cliente, null))
      // {
      //   Console.WriteLine($"Cliente de id {cliente_id} não encontrado.");
      //   return;
      // }

      Cliente cliente = new Cliente
      {
        Id = cliente_id,
      };

      Object clienteDesconecto = new
      {
        Nome = "Cliente Desconecto",
        Telefone = "111111111"
      };

      db.Attach(cliente);
      db.Entry(cliente).CurrentValues.SetValues(clienteDesconecto);
      /**
        Sempre irá atualizar,
        pois o 'SetValues' está alterando o 'State' para "Modified"
      */

      db.SaveChanges();
    }

    private static void AtualizarDados()
    {
      DbContext db = new Data.ApplicationContext();

      int cliente_id = 3;

      Cliente cliente = db.Set<Cliente>().Find(cliente_id);

      if (Object.Equals(cliente, null))
      {
        Console.WriteLine($"Cliente de id {cliente_id} não encontrado.");
        return;
      }

      cliente.Nome = "Angelo";

      // db.Set<Cliente>().Update(cliente);
      // Comentado para evitar a atualização de todas as colunas
      db.SaveChanges();
    }

    private static void ConsultarPedidoCarregamentoAdiantado()
    {
      DbContext db = new Data.ApplicationContext();

      List<Pedido> pedidos = db.Set<Pedido>()
        .Include(pedido => pedido.Itens)
          .ThenInclude(item => item.Produto)
        .ToList();

      Console.WriteLine(pedidos.Count);
    }

    private static void CadastrarPedido()
    {
      DbContext db = new Data.ApplicationContext();

      Cliente cliente = db.Set<Cliente>().FirstOrDefault();
      Produto produto = db.Set<Produto>().FirstOrDefault();

      Pedido pedido = new Pedido
      {
        ClienteId = cliente.Id,
        IniciadoEm = DateTime.Now,
        Observacao = "Pedido teste",
        Status = StatusPedido.Finalizado,
        TipoFrete = TipoFrete.SemFrete,
        Itens = new List<PedidoItem>
        {
          new PedidoItem
          {
            ProdutoId = produto.Id,
            Desconto = 0,
            Quantidade = 1,
            Valor = 10
          }
        }
      };

      db.Set<Pedido>().Add(pedido);

      db.SaveChanges();
    }

    private static void ConsultarDados()
    {
      DbContext db = new Data.ApplicationContext();

      // List<Cliente> consultaPorSintaxe = (
      //   from cliente
      //   in db.Set<Cliente>()
      //   where cliente.Id > 0
      //   select cliente
      // )
      // .AsNoTracking()
      // .ToList();

      List<Cliente> consultaPorMetodo = db.Set<Cliente>()
      .AsNoTracking()
      .Where(cliente => cliente.Id > 0)
      .ToList();

      /**
        AsNoTracking

        Inibe o rastreio de objetos em memória.

        Apenas o 'Find' utiliza a consulta em memória,
          então o uso do 'AsNoTracking' força uma re-consulta
          no banco em caso de utilização do 'Find'.
      */
    }

    private static int InserirDadosEmMassa()
    {
      Produto produto = new Produto
      {
        Ativo = true,
        Descricao = "Produto de teste",
        CodigoBarras = "1234567891234",
        Valor = 10m,
        TipoProduto = TipoProduto.MercadoriaParaRevenda
      };

      Cliente cliente = new Cliente
      {
        Nome = "Angelo",
        CEP = "12345789",
        Estado = "SP",
        Cidade = "Praia Grande",
        Telefone = "13997244863",
      };

      DbContext db = new Data.ApplicationContext();

      db.AddRange(new object[] { produto, cliente });

      int quantidadeLinhasAfetadas = db.SaveChanges();

      return quantidadeLinhasAfetadas;
    }

    private static int InserirProduto()
    {
      Produto produto = new Produto()
      {
        Descricao = "Produto de teste",
        Ativo = true,
        CodigoBarras = "1234567891234",
        Valor = 10m,
        TipoProduto = TipoProduto.MercadoriaParaRevenda
      };

      Data.ApplicationContext db = new Data.ApplicationContext();

      db.Produtos.Add(produto);

      int quantidadeLinhasAfetadas = db.SaveChanges();

      return quantidadeLinhasAfetadas;
    }
  }
}

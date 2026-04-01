using Gestao_Patrimonio.Applications.Regras;
using Gestao_Patrimonio.Domains;
using Gestao_Patrimonio.DTOs.EnderecoDto;
using Gestao_Patrimonio.Exceptions;
using Gestao_Patrimonio.Interfaces;
using System.Runtime.ConstrainedExecution;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Gestao_Patrimonio.Applications.Services
{
    public class EnderecoService
    {
        private readonly IEnderecoRepository _repository;

        public EnderecoService(IEnderecoRepository repository)
        {
            _repository = repository;
        }

        public List<ListarEnderecoDto> Listar()
        {
            List<Endereco> enderecos = _repository.Listar();

            List<ListarEnderecoDto> enderecosDto = enderecos.Select(endereco => new ListarEnderecoDto
            {
                EnderecoID = endereco.EnderecoID,
                Logradouro = endereco.Logradouro,
                Numero = endereco.Numero,
                Complemento = endereco.Complemento,
                CEP = endereco.CEP,
                BairroID = endereco.BairroID
            }).ToList();

            return enderecosDto;
        }

        public ListarEnderecoDto BuscarPorId(Guid enderecoId)
        {
            Endereco endereco = _repository.BuscarPorId(enderecoId);

            if (endereco == null)
            {
                throw new DomainException("Endereco nao encontrado");
            }

            ListarEnderecoDto enderecoDto = new ListarEnderecoDto
            {
                EnderecoID = endereco.EnderecoID,
                Logradouro = endereco.Logradouro,
                Numero = endereco.Numero,
                Complemento = endereco.Complemento,
                CEP = endereco.CEP,
                BairroID = endereco.BairroID
            };

            return enderecoDto;
        }

        public ListarEnderecoDto BuscarPorLogradouroENumero(string logradouro, int? numero, Guid bairroId)
        {
            Endereco endereco = _repository.BuscarPorLogradouroENumero(logradouro, numero, bairroId);

            if (endereco == null)
            {
                throw new DomainException("Endereco nao encontrado");
            }

            ListarEnderecoDto enderecoDto = new ListarEnderecoDto
            {
                Logradouro = endereco.Logradouro,
                Numero = endereco.Numero,
                Complemento = endereco.Complemento,
                CEP = endereco.CEP,
                BairroID = endereco.BairroID
            };

            return enderecoDto;
        }

        public void Adicionar(CriarEnderecoDto enderecoDto)
        {
            Validar.ValidarNome(enderecoDto.Logradouro);

            if (!_repository.BairroExiste(enderecoDto.BairroID))
            {
                throw new DomainException("Bairro informado não existe.");
            }

            Endereco enderecoExiste = _repository.BuscarPorLogradouroENumero(enderecoDto.Logradouro, enderecoDto.Numero, enderecoDto.BairroID);

            if (enderecoExiste != null)
            {
                throw new DomainException("Ja existe um endereco cadastrado com esse nome");
            }

            Endereco endereco = new Endereco
            {
                Logradouro = enderecoDto.Logradouro,
                Numero = enderecoDto.Numero,
                Complemento = enderecoDto.Complemento,
                CEP = enderecoDto.CEP,
                BairroID = enderecoDto.BairroID
            };

            _repository.Adicionar(endereco);
        }

        public void Atualizar(CriarEnderecoDto enderecoDto, Guid EnderecoId)
        {
            Validar.ValidarNome(enderecoDto.Logradouro);

            Endereco enderecoBanco = _repository.BuscarPorId(EnderecoId);

            if (enderecoBanco == null)
            {
                throw new DomainException("Endereco nao encontrado");
            }

            Endereco enderecoExistente = _repository.BuscarPorLogradouroENumero(enderecoDto.Logradouro, enderecoDto.Numero, enderecoDto.BairroID);

            if (enderecoExistente != null)
            {
                throw new DomainException("Ja existe um endereco cadastrado com esse nome");
            }

            enderecoBanco.Logradouro = enderecoDto.Logradouro;
            enderecoBanco.Numero = enderecoDto.Numero;
            enderecoBanco.Complemento = enderecoDto.Complemento;
            enderecoBanco.CEP = enderecoDto.CEP;
            enderecoBanco.BairroID = enderecoDto.BairroID;

            _repository.Atualizar(enderecoBanco);
        }

    }
}

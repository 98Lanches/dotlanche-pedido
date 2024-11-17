Funcionalidade: Criar um novo Pedido

Contexto:
    Dado que a API está em execução

Cenário: Criar um pedido com sucesso
    Dado que o payload do pedido é válido
      | ClienteCpf    | ProdutoId                              | Preco |
      | "12345678909" | 257c6ee9-774b-4c54-b564-6fca37573348 | 10.50 |
    Quando envio uma requisição POST para "/Pedido" com o payload
    Então a resposta deve ter o status 201 Created
    E a resposta deve conter o ID do pedido criado

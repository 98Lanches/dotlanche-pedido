Funcionalidade: Criar um novo Pedido

Contexto:
    Dado que a API está em execução
    E que o endpoint "POST /api/pedido" está disponível

Cenário: Criar um pedido com sucesso
    Dado que o payload do pedido é válido
      | ClienteCpf    | Combos                             |
      | "12345678909" | [{"ProdutoId": 1, "Preco": 10.50}] |
    Quando envio uma requisição POST para "/api/pedido" com o payload
    Então a resposta deve ter o status 201 Created
    E a resposta deve conter o ID do pedido criado

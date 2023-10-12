# TEST DRIVE DE VEÍCULOS

Projeto em .NET 6 com 3 microsserviços: Clientes, Reservas, e Veículos. A ideia é que cada microsserviço execute alguma rotina envolvida no processo de agendar um test drive: cadastro de veículo, associação de cliente e veículo, criação de reserva, envio de email de confirmação.

Além dos 3 microsserviços, o projeto também tem uma 'API Gateway', para receber as requisições e redirecionar para o microsserviço certo.
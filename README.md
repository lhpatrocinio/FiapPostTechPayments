# FiapPostTechPayments - Microserviço de Pagamentos

Microserviço de processamento de pagamentos e transações com **observabilidade completa** implementando:
- 💳 **Processamento de Pagamentos**: Múltiplos métodos de pagamento e gateways
- 📊 **Distributed Tracing**: OpenTelemetry + Jaeger para rastreamento distribuído
- 📈 **Monitoramento**: Prometheus + Grafana + ELK Stack
- 🚀 **Infraestrutura**: Docker containerizado com health checks

## 🎯 **Como o Microserviço Payments está Funcionando**

O sistema utiliza arquitetura limpa para:
- **💳 Pagamentos**: Cartão de crédito, débito, PIX, boleto bancário
- **🔄 Transações**: Processamento assíncrono com estados controlados
- **💰 Financeiro**: Cálculo de taxas, parcelamentos e estornos
- **📊 Relatórios**: Analytics financeiros e reconciliação
- **⚡ Performance**: Processamento otimizado com retry policies

### **🏗️ Arquitetura do Microserviço**
- **Domain Layer**: Entidades financeiras, regras de negócio e validações
- **Application Layer**: Serviços de pagamento, DTOs e processadores
- **Infrastructure Layer**: Gateways de pagamento e persistência
- **API Layer**: Controllers de transações e webhooks
- **Payment Gateways**: Integração com provedores externos
- **Observabilidade**: OpenTelemetry integration completa

### **🔍 Observabilidade & Distributed Tracing**
- **OpenTelemetry**: Instrumentação automática de HTTP requests e ASP.NET Core
- **Jaeger**: Visualização de traces distribuídos na porta 16686
- **Service Name**: "Payments.Api" para identificação no tracing
- **Transaction Tracing**: Rastreamento completo do fluxo de pagamento
- **Performance Monitoring**: Medição de latência e identificação de gargalos
- **Gateway Monitoring**: Monitoramento de integrações externas

## 🚀 **Como Iniciar**

### **1. Pré-requisitos**
```bash
# Ferramentas necessárias
- Docker Desktop
- .NET 8 SDK
- Git
```

### **2. Infraestrutura (Docker)**
```bash
# 1. Navegar para o diretório de infraestrutura
cd ../FiapPostTechDocker

# 2. Subir infraestrutura completa
docker-compose up -d sqlserver elasticsearch kibana logstash prometheus grafana jaeger rabbitmq

# 3. Verificar containers rodando
docker ps
# Deve mostrar: sqlserver (1433), elasticsearch (9200), jaeger (16686), prometheus (9090), etc.

# 4. Testar serviços principais
curl http://localhost:9200     # Elasticsearch
curl http://localhost:16686    # Jaeger UI
curl http://localhost:9090     # Prometheus
curl http://localhost:3000     # Grafana
```

### **3. Aplicação (.NET)**
```bash
# 1. Navegar para a API
cd src/Payments.Api

# 2. Restaurar pacotes
dotnet restore

# 3. Executar aplicação
dotnet run
# Aplicação disponível em: http://localhost:9201
# Swagger disponível em: http://localhost:9201/swagger
```

### **4. Inicialização Automática**
A aplicação faz automaticamente:
- ✅ **Aplicação de migrations**: DatabaseStructure + dados iniciais
- ✅ **Configuração de Gateways**: Setup dos provedores de pagamento
- ✅ **Setup RabbitMQ**: Queues para processamento assíncrono
- ✅ **Health checks**: Monitoramento de dependências

## 💳 **Sistema de Pagamentos**

### **Criar Transação**
```http
POST /api/v1/payments/transactions
```
**Função**: Inicia nova transação de pagamento com validações completas.
**Métodos**: Cartão, PIX, boleto, débito.
**Validações**: Dados do cartão, limites, disponibilidade.

### **Consultar Transação**
```http
GET /api/v1/payments/transactions/{id}
```
**Função**: Retorna detalhes completos da transação.
**Estados**: Pendente, Processando, Aprovado, Rejeitado, Cancelado.

### **Processar Pagamento**
```http
POST /api/v1/payments/process/{transactionId}
```
**Função**: Processa pagamento através dos gateways configurados.
**Assíncrono**: Processamento em background com notificações.

### **Webhook de Confirmação**
```http
POST /api/v1/payments/webhook/{gateway}
```
**Função**: Recebe confirmações dos gateways de pagamento.
**Segurança**: Validação de assinatura e autenticação.

## 💰 **Gestão Financeira**

### **Calcular Parcelamento**
```http
POST /api/v1/payments/installments/calculate
```
**Função**: Calcula parcelas com juros e taxas aplicáveis.
**Parâmetros**: Valor, quantidade de parcelas, tipo de produto.

### **Processar Estorno**
```http
POST /api/v1/payments/refund/{transactionId}
```
**Função**: Processa estorno total ou parcial da transação.
**Validações**: Tempo limite, status da transação, motivo.

### **Relatório de Vendas**
```http
GET /api/v1/payments/reports/sales
```
**Função**: Relatório financeiro com vendas por período.
**Filtros**: Data, método de pagamento, status.

### **Conciliação Bancária**
```http
GET /api/v1/payments/reconciliation
```
**Função**: Relatório de conciliação com movimento bancário.
**Uso**: Auditoria e controle financeiro.

## 📊 **Analytics e Métricas**

### **Dashboard Financeiro**
```http
GET /api/v1/payments/analytics/dashboard
```
**Função**: KPIs financeiros em tempo real.
**Métricas**: Volume, ticket médio, taxa de aprovação, métodos populares.

### **Análise de Performance**
```http
GET /api/v1/payments/analytics/performance
```
**Função**: Performance dos gateways de pagamento.
**Métricas**: Tempo de resposta, taxa de sucesso, erros.

### **Detecção de Fraude**
```http
GET /api/v1/payments/analytics/fraud-detection
```
**Função**: Análise de padrões suspeitos e tentativas de fraude.
**Algoritmos**: Machine learning para detecção automatizada.

## ⚙️ **Características Técnicas**

### **🎯 Arquitetura de Pagamentos**
- **Gateway Pattern**: Abstração para múltiplos provedores
- **State Machine**: Controle rigoroso de estados das transações
- **Retry Policies**: Tentativas automáticas com backoff exponencial
- **Idempotency**: Prevenção de cobranças duplicadas
- **Saga Pattern**: Transações distribuídas com compensação

### **💳 Gateways Suportados**
- **Cartão de Crédito**: Visa, Mastercard, American Express
- **Cartão de Débito**: Débito online com autenticação
- **PIX**: Integração completa com BACEN
- **Boleto Bancário**: Geração e conciliação automática
- **Carteiras Digitais**: PayPal, Apple Pay, Google Pay

### **🔐 Segurança Financeira**
- **PCI Compliance**: Padrões de segurança para dados de cartão
- **Tokenization**: Substituição de dados sensíveis por tokens
- **Encryption**: Criptografia AES-256 para dados em trânsito e repouso
- **Fraud Detection**: Algoritmos de ML para detecção de fraude
- **3D Secure**: Autenticação adicional para cartões

## 🔍 **Monitoramento e Observabilidade**

### **Health Checks e Observabilidade**
- **`/health`**: Status geral da aplicação + gateways
- **`/health/ready`**: Verificação de conectividade com provedores
- **`/health/live`**: Liveness probe para containers
- **`/metrics`**: Métricas Prometheus para observabilidade

### **🔍 Distributed Tracing Endpoints**
- **Jaeger UI**: `http://localhost:16686` - Visualização de traces
- **Service Name**: "Payments.Api" - Identificação no Jaeger
- **Transaction Tracing**: Rastreamento completo do fluxo de pagamento
- **Gateway Tracing**: Monitoramento de chamadas para provedores externos

### **📊 Dashboards de Monitoramento**
- **Grafana**: `http://localhost:3000` (admin/admin)
- **Prometheus**: `http://localhost:9090` - Métricas coletadas
- **Kibana**: `http://localhost:5601` - Logs centralizados
- **RabbitMQ**: `http://localhost:15672` (guest/guest)

### **📝 Logs Estruturados**
O sistema gera logs estruturados para:
- **Transações**: Criação, processamento, aprovação/rejeição
- **Gateways**: Chamadas para provedores externos
- **Fraude**: Tentativas suspeitas e bloqueios
- **Performance**: Tempo de resposta e throughput

## 💡 **Eventos e Integrações**

### **Eventos Publicados**
- **PaymentCreated**: Quando transação é criada
- **PaymentProcessed**: Quando pagamento é processado
- **PaymentApproved**: Quando pagamento é aprovado
- **PaymentRejected**: Quando pagamento é rejeitado
- **RefundProcessed**: Quando estorno é processado

### **Consumidores de Eventos**
O microserviço consome eventos de outros serviços para:
- Processar pagamentos de pedidos
- Atualizar status de compras
- Enviar notificações

## 🔧 **Configuração e Deploy**

### **Variáveis de Ambiente**
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "SQL Server connection"
  },
  "PaymentGateways": {
    "CreditCard": {
      "Provider": "Stripe",
      "ApiKey": "your-stripe-key",
      "WebhookSecret": "webhook-secret"
    },
    "PIX": {
      "Provider": "BACEN",
      "CertificatePath": "certificate.p12",
      "Environment": "sandbox"
    }
  },
  "Security": {
    "EncryptionKey": "your-encryption-key",
    "TokenizationKey": "tokenization-key"
  }
}
```

### **Docker Setup**
```yaml
payments.api:
  image: paymentsapi
  container_name: FiapPayments
  ports: ["9201:80"]
  networks: [postech-network]
  depends_on: [sqlserver, rabbitmq]
```

## 🧪 **Verificação de Funcionamento**

### **Testando os Endpoints**

**Swagger UI**: Acesse `http://localhost:9201/swagger` para testar todos os endpoints interativamente.

**Principais Testes**:
- **Transação**: `POST /api/v1/payments/transactions` com dados de cartão
- **Consulta**: `GET /api/v1/payments/transactions/{id}` 
- **Parcelamento**: `POST /api/v1/payments/installments/calculate`
- **Health**: `GET /health` para verificar status

### **Teste de Pagamento Completo**
```bash
# 1. Criar transação
curl -X POST http://localhost:9201/api/v1/payments/transactions \
  -H "Content-Type: application/json" \
  -d '{
    "amount": 100.00,
    "currency": "BRL",
    "paymentMethod": "credit_card",
    "cardNumber": "4111111111111111",
    "expiryMonth": "12",
    "expiryYear": "2025",
    "cvv": "123"
  }'

# 2. Processar pagamento
curl -X POST http://localhost:9201/api/v1/payments/process/{transactionId}

# 3. Consultar status
curl -X GET http://localhost:9201/api/v1/payments/transactions/{transactionId}
```

## 🚨 **Troubleshooting Completo**

### **Problemas de Pagamento**

**Gateway não responde**:
1. Verificar conectividade com provedor
2. Validar chaves de API e certificados
3. Verificar logs de rede e timeouts

**Transação rejeitada**:
1. Verificar dados do cartão
2. Validar limites e disponibilidade
3. Verificar regras antifraude

### **Problemas de Infraestrutura**

**Performance lenta**:
1. Verificar traces no Jaeger para identificar gargalos
2. Monitorar métricas no Grafana
3. Verificar load dos gateways externos

**Dados inconsistentes**:
1. Verificar logs de conciliação
2. Executar relatórios de auditoria
3. Verificar eventos publicados no RabbitMQ

## ✅ **Requisitos FIAP Tech Challenge Atendidos**

### **Processamento de Pagamentos - 100% Implementado**
- ✅ **Múltiplos Métodos**: Cartão, PIX, boleto, débito
- ✅ **Gateways Integrados**: Múltiplos provedores suportados
- ✅ **Segurança PCI**: Compliance com padrões financeiros
- ✅ **Estados Controlados**: State machine para transações

### **Distributed Tracing - 100% Implementado**
- ✅ **OpenTelemetry**: Instrumentação automática completa
- ✅ **Jaeger**: Coleta e visualização de traces distribuídos
- ✅ **Transaction Tracing**: Rastreamento completo do fluxo de pagamento
- ✅ **Performance Monitoring**: Identificação de gargalos

### **Funcionalidades Extras Implementadas**
- 💳 **Gateways Múltiplos**: Abstração para diferentes provedores
- 📊 **Analytics Financeiros**: Dashboards e relatórios em tempo real
- 🔐 **Segurança Avançada**: Tokenização, criptografia, antifraude
- ⚙️ **Observabilidade Completa**: ELK + Prometheus + Grafana + Jaeger
- 🚀 **Performance**: Processamento otimizado com retry policies

## 👥 **Ecossistema FIAP Tech Challenge**

Este projeto faz parte da arquitetura de microserviços:
- **💳 FiapPosTechPayments**: Microserviço de pagamentos (este projeto)
- **🎮 FiapPosTechGames**: Microserviço de jogos com Elasticsearch
- **👤 FiapPosTechUsers**: Microserviço de usuários e autenticação
- **🚀 FiapPosTechDocker**: Infraestrutura Docker compartilhada

## 📄 **Documentação Técnica**

- **Swagger API**: `http://localhost:9201/swagger` (durante execução)
- **Arquitetura**: Clean Architecture com patterns financeiros
- **Compliance**: PCI DSS, LGPD, regulamentações BACEN

---

**🎆 Microserviço Payments com observabilidade completa em produção:**
- **Processamento seguro** de múltiplos métodos de pagamento
- **Distributed Tracing** com OpenTelemetry + Jaeger
- **Monitoramento completo** com Prometheus + Grafana + ELK Stack
- **Health checks** em todos os componentes e gateways
# EscalaSistema — Documento de Regra de Negócio v1

## 1) Objetivo
Definir a regra de negócio para geração e gestão de **escala mensal de cultos** com foco inicial em **músicos**, garantindo consistência operacional, previsibilidade de fluxo e base para expansão futura (ex.: ministros, mídia, intercessão).

---

## 2) Escopo da v1
A versão v1 cobre:
- Cadastro de cultos.
- Cadastro de músicos.
- Cadastro de músicas por culto.
- Criação de uma escala por culto.
- Atribuição manual e em lote de músicos na escala.
- Publicação e fechamento da escala.
- Confirmação/recusa de participação por atribuição.

Fora do escopo da v1 (futuro):
- Regras avançadas de carga mensal (limite de escalas por músico).
- Priorização automática por disponibilidade histórica.
- Suporte pleno a múltiplos ministérios com catálogo dinâmico de papéis.

---

## 3) Glossário
- **Culto**: evento com data/hora em que a equipe irá ministrar.
- **Escala**: composição da equipe de um culto específico.
- **Atribuição**: vínculo entre músico e escala com um papel/função.
- **Papel/Função**: atuação do músico no culto (ex.: teclado, violão, vocal).
- **Publicar escala**: tornar a escala oficial para consulta e confirmação.
- **Fechar escala**: encerrar alterações da escala.

---

## 4) Entidades e Relações

## 4.1 Culto
- Um culto possui: nome, data/hora, status de publicação e músicas associadas.
- Um culto possui **uma única escala** (relação 1:1 com escala).

## 4.2 Músico
- Um músico possui: nome, instrumento base, status ativo/inativo e histórico de atribuições.

## 4.3 Música
- Uma música pertence a um culto.
- Um culto pode conter várias músicas (setlist).

## 4.4 Escala
- Uma escala pertence a um culto.
- Uma escala contém várias atribuições de músicos.
- Uma escala tem ciclo de vida com estados de negócio.

## 4.5 Atribuição de Escala
- Associa: `Escala + Músico + Papel`.
- Possui status de confirmação: `Pending`, `Confirmed`, `Declined`.

---

## 5) Estados da Escala (máquina de estado)

## 5.1 Estados
- **Draft**: escala criada, editável.
- **Published**: escala publicada, visível/oficial.
- **Closed**: escala encerrada, imutável.

## 5.2 Transições válidas
- `Draft -> Published`
- `Published -> Closed`

## 5.3 Transições inválidas
- `Closed -> qualquer estado`
- `Published -> Draft` (na v1, sem “despublicar”)
- `Draft -> Closed` (fechamento só após publicação)

---

## 6) Regras de Negócio (RN)

## 6.1 Cadastro de Culto
- **RN-CULT-01**: nome é obrigatório.
- **RN-CULT-02**: nome deve respeitar tamanho mínimo e máximo definidos pelo domínio.
- **RN-CULT-03**: data/hora do culto não pode ser no passado.
- **RN-CULT-04**: recomenda-se unicidade por data/hora para evitar conflito operacional.

## 6.2 Cadastro de Músico
- **RN-MUS-01**: nome é obrigatório.
- **RN-MUS-02**: músico é criado ativo por padrão (se definido pela operação).
- **RN-MUS-03**: músico inativo não pode ser atribuído em novas escalas.

## 6.3 Cadastro de Música
- **RN-MSC-01**: música deve referenciar um culto válido.
- **RN-MSC-02**: culto deve respeitar limite máximo de músicas (se definido).
- **RN-MSC-03**: título/chave/link devem respeitar validações de formato da aplicação.

## 6.4 Criação de Escala
- **RN-SCL-01**: só pode existir **uma escala por culto**.
- **RN-SCL-02**: escala nasce em estado `Draft`.
- **RN-SCL-03**: escala não pode ser criada para culto inexistente.

## 6.5 Atribuição Manual de Músico
- **RN-ATR-01**: escala deve existir.
- **RN-ATR-02**: músico deve existir e estar ativo.
- **RN-ATR-03**: escala não pode estar `Published` ou `Closed`.
- **RN-ATR-04**: o mesmo músico não pode ser atribuído duas vezes na mesma escala.
- **RN-ATR-05**: papel/função deve ser válido no catálogo atual (enum na v1).
- **RN-ATR-06**: após atribuição, alterações devem ser persistidas na mesma unidade transacional.

## 6.6 Atribuição em Lote
- **RN-BLK-01**: deve validar todos os músicos informados antes de persistir parcialmente.
- **RN-BLK-02**: para cultos sem escala, sistema pode criar escala automaticamente em `Draft`.
- **RN-BLK-03**: não duplicar atribuições já existentes.
- **RN-BLK-04**: operação deve ser idempotente para a mesma carga (quando possível).

## 6.7 Publicação de Escala
- **RN-PUB-01**: escala deve existir.
- **RN-PUB-02**: escala não pode estar fechada.
- **RN-PUB-03**: escala não pode já estar publicada.
- **RN-PUB-04**: escala deve ter ao menos uma atribuição.
- **RN-PUB-05**: recomendado validar composição mínima da equipe antes de publicar.

## 6.8 Fechamento de Escala
- **RN-FCH-01**: escala deve existir.
- **RN-FCH-02**: apenas escala publicada pode ser fechada.
- **RN-FCH-03**: escala fechada não aceita alteração.

## 6.9 Confirmação de Participação
- **RN-CNF-01**: apenas músico da atribuição pode confirmar/recusar.
- **RN-CNF-02**: confirmação deve registrar data/hora da ação.
- **RN-CNF-03**: recusa deve atualizar status para `Declined`.

---

## 7) Regras Operacionais Recomendadas (v1.1)
Estas regras podem ser habilitadas gradualmente para melhorar a qualidade da escala:
- **RO-01**: impedir conflito de horário (mesmo músico em dois cultos simultâneos).
- **RO-02**: descanso mínimo entre cultos do mesmo músico.
- **RO-03**: limite mensal de participações por músico para equilíbrio de escala.
- **RO-04**: distribuição justa automática em geração assistida.

---

## 8) Casos de Uso e Critérios de Aceite (Given/When/Then)

## 8.1 Criar escala com sucesso
- **Given** culto válido e sem escala
- **When** líder solicita criação de escala
- **Then** sistema cria escala em `Draft`
- **And** retorna identificador da escala

## 8.2 Impedir segunda escala no mesmo culto
- **Given** culto já possui escala
- **When** usuário tenta criar nova escala
- **Then** sistema rejeita com erro de conflito

## 8.3 Atribuir músico com sucesso
- **Given** escala em `Draft` e músico ativo
- **When** líder atribui músico com papel válido
- **Then** sistema registra atribuição

## 8.4 Impedir atribuição duplicada
- **Given** músico já atribuído na escala
- **When** líder tenta nova atribuição do mesmo músico
- **Then** sistema rejeita a operação

## 8.5 Publicar escala válida
- **Given** escala em `Draft` com atribuições
- **When** líder publica a escala
- **Then** sistema marca estado `Published`
- **And** registra data de publicação

## 8.6 Impedir fechamento de escala não publicada
- **Given** escala em `Draft`
- **When** líder tenta fechar escala
- **Then** sistema rejeita com regra de estado inválido

## 8.7 Fechar escala publicada
- **Given** escala em `Published`
- **When** líder fecha escala
- **Then** sistema marca estado `Closed`
- **And** bloqueia alterações futuras

---

## 9) Matriz de Permissões (mínima)
- **Líder**: criar escala, atribuir, publicar, fechar.
- **Músico**: consultar próprias escalas, confirmar/recusar atribuição.
- **Administrador** (opcional): gestão global e auditoria.

---

## 10) Não-funcionais e Governança
- Registrar auditoria de operações críticas (`quem`, `quando`, `ação`, `antes/depois`).
- Garantir mensagens de erro semânticas e consistentes por domínio.
- Priorizar transações atômicas em operações em lote.
- Padronizar nomenclatura de erros para evitar ambiguidade (ex.: músico vs música).

---

## 11) Roadmap de Evolução

## Fase 1 (atual)
- Consolidar fluxo de músicos com estados da escala.

## Fase 2
- Introduzir regras automáticas de disponibilidade e conflito.

## Fase 3
- Expandir para outros ministérios (ministros, mídia, etc.) com catálogo dinâmico de papéis.

---

## 12) Definição de Pronto (DoD) da v1
A v1 é considerada pronta quando:
1. Todas as RN de cadastro/escala/publicação/fechamento estiverem implementadas.
2. Houver testes unitários cobrindo caminho feliz, validações e exceções dos casos críticos.
3. Endpoints refletirem os mesmos estados/regras definidos neste documento.
4. Erros retornados pela API estiverem coerentes com o domínio.


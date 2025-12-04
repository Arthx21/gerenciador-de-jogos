# Descrição Detalhada do Projeto

O sistema desenvolvido é uma aplicação de console em C# que simula uma plataforma de gerenciamento de jogos e conquistas, semelhante a serviços como Steam, Xbox Live ou PlayStation Network. O programa permite o cadastro de usuários, autenticação, gerenciamento de jogos pela parte administrativa e acompanhamento de conquistas por parte dos usuários comuns.

A aplicação trabalha com dois tipos de perfis:

- Administrador (Admin)
Possui permissões especiais, como registrar novos jogos e definir suas conquistas.

- Usuário
Pode montar sua biblioteca, desbloquear conquistas, acumular XP, subir de nível e comparar progresso com outros usuários.

## Funcionalidades principais:
###1. Sistema de Login e Cadastro

- Login com validação de credenciais.
- Cadastro de novos usuários.
- Diferenciação automática entre Admin e Usuário.

###2. Funções do Administrador

Cadastrar jogos com:

- ID automático
- Nome
- Gênero
- Lista de conquistas

Cadastrar conquistas para cada jogo, contendo:

- Nome
- XP
- Dificuldade

###3. Funções do Usuário

- Visualizar sua biblioteca com todos os jogos e suas conquistas.
- Adicionar jogos cadastrados pelo Admin à sua biblioteca pessoal.
- Marcar conquistas como desbloqueadas.
- Ganhar XP ao desbloquear conquistas.
- Sistema automático de nível (1 nível a cada 1000 XP).
Comparar:

- Seu progresso com outro usuário.
- Seu progresso em um jogo específico com outro usuário.

### 4. Armazenamento persistente

O sistema salva todos os dados em arquivo JSON (dados.json):

- Usuários
- Admins
- Jogos cadastrados globalmente
- Bibliotecas individuais com conquistas e status

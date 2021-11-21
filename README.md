# AppSecretsDemoProject

## Manter segredos de configurações de aplicações .NET fora do controle de versão de código

Se o sua aplicação é uma aplicação para clientes de desktop ou um site, é importante manter senhas, strings de conexão e chaves de API fora do controle de origem. No .NET, essas configurações são armazenadas em um arquivo app.config ou web.config, dependendo do tipo de aplicativo que você está construindo e esses arquivos seriam verificados no controle de origem. Já fiz isso no passado para muitos projetos. Claro, eu nunca cometi segredos de produção, mas ainda era um grande não- não, mas eu nunca tive tempo de investigar a maneira adequada de lidar com esta situação ... até hoje.

Uma pesquisa rápida no Google retornou este resultado: http://www.asp.net/identity/overview/features-api/best-practices-for-deploying-passwords-and-other-sensitive-data-to-aspnet-and-azure. Os arquivos Config têm uma maneira simples de lidar com isso adicionando um atributo de arquivo à seção **appSettings** do arquivo web.config, assim

```cs
<appSettings file="secrets.config">
  <add key="testSetting" value="not a secret" />
</appSettings/>
```

E o arquivo secrets.config se parece com este



```cs
<appSettings>
  <add key="secretTestSetting" value="very secret" />
  <add key="testSetting" value="I will overwrite" />
</appSettings>
```

Este é o arquivo inteiro, é importante que o elemento raiz seja <**appSettings**>, caso contrário você terá um erro de compilação ou tempo de execução. Quaisquer novas chaves definidas serão adicionadas e todas as chaves existentes substituirão o valor da web.config.

A substituição de valores na web.config é muito útil para o desenvolvimento local. Uma vez que o arquivo secrets.config nunca é verificado, cada desenvolvedor pode manter seus próprios valores de configurações de aplicativos locais sem se preocupar em verificar erroneamente e substituir os valores padrão da web.config. Chega de comentar valores em sua web.config!

A mesma coisa funciona para a seção **connectionStrings**, exceto que neste caso toda a seção é substituída. Na verdade, o web.config não pode ter nenhum elemento sob ela ou você terá erros. A web.config vai ficar assim

```cs
<connectionStrings configSource="connectionStrings.config">
</connectionStrings>
```

E a conexãoStrings.config será assim

```cs
<connectionStrings>
  <clear/>
  <add name="Database" connectionString="very secret" />
</connectionStrings>
```



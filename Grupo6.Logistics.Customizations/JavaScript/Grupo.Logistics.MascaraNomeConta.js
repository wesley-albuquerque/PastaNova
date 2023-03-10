if (typeof (LogisticsPrincipal) == "undefined") { LogisticsPrincipal = {} }
if (typeof (LogisticsPrincipal.Conta) == "undefined") { LogisticsPrincipal.Conta = {} }


LogisticsPrincipal.Conta = {
    OnCreateConta: function (executionContext) {
        var formContext = executionContext.getFormContext();
        var nomeConta = formContext.getAttribute("name").getValue().toLowerCase();
        nomeConta = nomeConta.replace(/\s\s+/g, ' ');
        var formatedName = nomeConta.split(" ");

        if (nomeConta != null) {

            for (var i = 0; i < formatedName.length; i++) {
                var n = formatedName[i];

                var firstLetter = n[0];

                if (n.length > 2) {
                    n = firstLetter.toUpperCase() + n.slice(1);
                }
                else {
                    n = firstLetter + n.slice(1);
                }

                formatedName[i] = n;

            }

            nomeConta = formatedName.join(" ");
        }
        else {
            return;
        }

        formContext.getAttribute("name").setValue(nomeConta);
    }



}


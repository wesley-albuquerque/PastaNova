if (typeof (TccDynacoop) == "undefined") { TccDynacoop = {} }
if (typeof (TccDynacoop.Account) == "undefined") { TccDynacoop.Account = {} }

TccDynacoop.Account = {
    OnLoad: function (executionContext) {
        var formContext = executionContext.getFormContext();
    },
    CNPJOnChange: function (executionContext) {
        var formContext = executionContext.getFormContext();
        var cnpj = formContext.getAttribute("gp6_cnpj").getValue();
        if (cnpj == null || cnpj == "") return false;
        cnpj = cnpj.replace(/[^\d]+/g, '');
        
        if (cnpj.length != 14) return TccDynacoop.Account.DynamicsAlert("O CNPJ digitado não é valido", "CNPJ inválido!");

        if (/^(\d)\1+$/.test(cnpj)) return TccDynacoop.Account.DynamicsAlert("O CNPJ digitado não é valido", "CNPJ inválido!");

        if (cnpj.length == 14) {
            // Validação do primeiro dígito verificador
            var soma = 0;
            var array = [5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2];
            for (var i = 0; i < 12; i++) {
                soma += parseInt(cnpj.charAt(i)) * array[i];
            }
            var mod = soma % 11;
            var dv1 = mod < 2 ? 0 : 11 - mod;
            if (parseInt(cnpj.charAt(12)) != dv1)
                return TccDynacoop.Account.DynamicsAlert("O CNPJ digitado não é valido", "CNPJ inválido!");

            // Validação do segundo dígito verificador
            soma = 0;
            var array2 = [6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2];
            for (let i = 0; i < 13; i++) {
                soma += parseInt(cnpj.charAt(i)) * array2[i];
            }
            mod = soma % 11;
            let dv2 = mod < 2 ? 0 : 11 - mod;
            if (parseInt(cnpj.charAt(13)) != dv2)
                return TccDynacoop.Account.DynamicsAlert("O CNPJ digitado não é valido", "CNPJ inválido!");

            //Formatação do CNPJ
            var formattedCNPJ = cnpj.replace(/^(\d{2})(\d{3})(\d{3})(\d{4})(\d{2})/, "$1.$2.$3/$4-$5");
            formContext.getAttribute("gp6_cnpj").setValue(formattedCNPJ);
        }
    },
    DynamicsAlert: function (alertText, alertTitle) {
        var alertStrings = {
            confirmButtonLabel: "OK",
            text: alertText,
            title: alertTitle
        };

        var alertOptions = {
            height: 120,
            width: 200
        };

        Xrm.Navigation.openAlertDialog(alertStrings, alertOptions);
    }
}


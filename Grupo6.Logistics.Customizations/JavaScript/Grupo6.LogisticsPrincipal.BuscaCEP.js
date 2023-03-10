if (typeof (LogisticsPrincipal) == "undefined") { LogisticsPrincipal = {} }

LogisticsPrincipal.BuscaCEP =
{
    OnChangeCEP: function (executionContext) {
        var formContext = executionContext.getFormContext();

        var id = Xrm.Page.data.entity.getId();

        var cep = formContext.getAttribute("address1_postalcode").getValue();

        if (cep != null) {
            cep = cep.replace(/[^\d]/g, "");

        }


        if (cep != null && cep.length == 8) {
            var execute_dgp6_BuscaCep_Request = {
                CEP: cep,

                getMetadata: function () {
                    return {
                        parameterTypes: {
                            CEP: { typeName: "Edm.String", structuralProperty: 1 }
                        },
                        operationType: 0, operationName: "gp6_BuscaCEP"
                    };
                }
            };

            Xrm.WebApi.online.execute(execute_dgp6_BuscaCep_Request).then(
                function success(response) {
                    debugger;

                    if (response.ok) { return response.json(); }
                }
            ).then(function (responseBody) {
                debugger;

                var result = responseBody;
                console.log(result);

                var logradouro = result["logradouro"];
                formContext.getAttribute("address1_line1").setValue(logradouro);
                formContext.getAttribute("address1_line3").setValue(result["complemento"]);
                formContext.getAttribute("address1_upszone").setValue(result["bairro"]);
                formContext.getAttribute("address1_city").setValue(result["localidade"]);
                formContext.getAttribute("address1_stateorprovince").setValue(result["uf"]);
                formContext.getAttribute("gp6_ibge").setValue(result["ibge"]);
                formContext.getAttribute("gp6_ddd").setValue(result["ddd"]);
                var cepFormatado = cep.replace(/(\d{5})(\d{3})/, "$1-$2");
                formContext.getAttribute("address1_postalcode").setValue(cepFormatado);

                if (result["erro"]) {
                    LogisticsPrincipal.BuscaCEP.DynamicsAlert("CEP inexistente", "CEP inválido").then(function close(data) {

                        formContext.getControl("address1_postalcode").setFocus();
                    });;
                }


            }).catch(function (error) {
                debugger;

                console.log(error.message);
            });


        }
        else {
            if (cep == null) {
                return
            }
            else {
                LogisticsPrincipal.BuscaCEP.DynamicsAlert("Informe um CEP", "CEP inválido", formContext).then(function close(data) {

                    formContext.getControl("address1_postalcode").setFocus();
                });
                formContext.getAttribute("address1_line1").setValue("");
                formContext.getAttribute("address1_line3").setValue("");
                formContext.getAttribute("address1_upszone").setValue("");
                formContext.getAttribute("address1_line2").setValue("");
                formContext.getAttribute("address1_city").setValue("");
                formContext.getAttribute("address1_stateorprovince").setValue("");
                formContext.getAttribute("gp6_ibge").setValue("");
                formContext.getAttribute("gp6_ddd").setValue("");

            }
        }
    },
    DynamicsAlert: function (alertText, alertTitle) {

        var alerStrings = {
            confimButtonLabel: "OK",
            text: alertText,
            title: alertTitle
        };
        var alertOptions = {
            height: 120,
            width: 200
        };

        return Xrm.Navigation.openAlertDialog(alerStrings, alertOptions)

    }
}

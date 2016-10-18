using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Collections.Specialized;
using HelloSign;
using RestSharp;
using RestSharp.Authenticators;



namespace HelloSignConsoleTester
{
    class Program
    {
        //https://support.microsoft.com/en-us/kb/815786
        //http://stackoverflow.com/questions/11451535/gitignore-not-working

        static void Main(string[] args)
        {

            Console.WriteLine("HelloWorld HelloSign");

            var sAttr = ConfigurationManager.AppSettings.Get("Key0");
            var apiKey = ConfigurationManager.AppSettings.Get("ApiKeyAlexmac2017");
            var hslogin = ConfigurationManager.AppSettings.Get("hs_email");
            var hspassword = ConfigurationManager.AppSettings.Get("hs_password");
            Console.WriteLine(sAttr + " Is key");

            var client = new Client(apiKey);
            Console.WriteLine(client.GetType().ToString());
            var account = client.GetAccount();
            Console.WriteLine("My Account ID is: " + account.AccountId);

            string menu_item = "01";

            var clientRestSharp = new RestClient();
            string buildTheRequest = "https://" + apiKey + ":@api.hellosign.com/v3/signature_request/create_embedded_with_template";
            Console.WriteLine(buildTheRequest);
            // clientRestSharp.BaseUrl = new Uri("")


            while (menu_item != "111")
            {
                Console.WriteLine("\n");
                Console.WriteLine("");


                Console.WriteLine("\n");
                Console.WriteLine("MENU\n");
                Console.WriteLine("");

                Console.WriteLine("1 - Signature Request Get files - Get a copy of the document");
                Console.WriteLine("2 - GET signature_request");
                Console.WriteLine("3 - Send signature_request");
                Console.WriteLine("4 - Send_with_template");
                Console.WriteLine("5 - Embedded Requesting");
                Console.WriteLine("6 - Embedded Signature request with template");
                Console.WriteLine("7 - Embedded Signature - part two - get the URL with the signature ID");

                Console.WriteLine("8 - Delete all unsigned documents that are out for signature");
                Console.WriteLine("9 - Delete all unsigned documents that are out for signature");
                Console.WriteLine("10 - Embedded Signature request");



                Console.WriteLine("11 - Get a url for an unathenticated link to a file to download");
                Console.WriteLine("12 - Create an unclaimed draft embedded");
                Console.WriteLine("13 - Create a nonembedded signature request");
                Console.WriteLine("14: Create embedded unclaimed draft with template\n");
                Console.WriteLine("15: AccountInfoReturned\n");

                Console.WriteLine("16: Send a reminder for a request");
                Console.WriteLine("17: Cancel a signature request");
                Console.WriteLine("22: Cancel a signature request");
                Console.WriteLine("111: EXIT because these go to 11!!!!\n");

                var inputAtCommandLineMenuItem = Console.ReadLine();
                menu_item = Convert.ToString(inputAtCommandLineMenuItem);


                if (menu_item == "1")
                {
                    client.DownloadSignatureRequestFiles("cd93fd0e46de230ebb7f941975ebbd9046313642", "files.pdf", SignatureRequest.FileType.PDF);
// Console.WriteLine(response);

                }
                else if (menu_item == "2")
                {

                }
                else if (menu_item == "3")
                {
                    sendSignatureRequest(client);
                }
                else if (menu_item == "4")
                {
                    sendTemplateSignatureRequest(client);
                }
                else if (menu_item == "22")
                {
                    //sendPureRequest(client, apiKey, "getAccount", hslogin, hspassword);
                    //sendPureRequest(client, apiKey, "sendSignatureRequest", hslogin, hspassword);
                    sendPureRequest(client, apiKey, "sendSignatureRequestCustomFieldsTemplate", hslogin, hspassword);
                }
                

           

            }


        }

        static void methodName(HelloSign.Client client)
        {

        }


        /*
         * data = {
            'client_id': '21f3f5352a6a1419b8875db11dca5dd8',
            'template_id': 'cc93d9348fd415f1c54f632e75519d3296e7acb9',
            'subject': 'ticket214090',
            'message': 'ticket214090',
            'signers[Client][name]': 'George',
            'signers[Client][email_address]':'alex.mcferron@hellosign.com',
            'custom_fields': '[{'name':'newline', 'value':'$20,000', 'editor':'Client', 'required':true}]',
            'test_mode': '1'
        }
         * 
         * 
         * 
         */
        static void sendPureRequest(HelloSign.Client client, string apikey, string function, string hs_login, string hs_password)
        {

            if (function == "getAccount")
            {
                try
                {


                    var restClient = new RestClient();



                    string buildTheRequest = "https://" + apikey + ":@api.hellosign.com";
                    restClient.BaseUrl = new Uri(buildTheRequest);
                    restClient.Authenticator = new HttpBasicAuthenticator(hs_login, hs_password);

                    var request = new RestRequest();
                    request.Resource = "/v3/account";
                    IRestResponse response = restClient.Execute(request);

                    Console.WriteLine(response.Content);

                    if (response.ErrorException != null)
                    {
                        const string message = "Error retrieving response.  Check inner details for more info.";
                        var helloSignException = new ApplicationException(message, response.ErrorException);
                        throw helloSignException;
                    }



                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message, ex.InnerException.Message);
                }
            }
            else if (function == "sendSignatureRequest")
            {

                try
                {
                    var restClient = new RestClient();



                    string buildTheRequest = "https://" + apikey + ":@api.hellosign.com";
                    restClient.BaseUrl = new Uri(buildTheRequest);
                    restClient.Authenticator = new HttpBasicAuthenticator(hs_login, hs_password);

                    var request = new RestRequest();
                    request.Resource = "v3/signature_request/send_with_template";
                    request.Method = Method.POST;
                    request.AddParameter("template_id", "422eb83ea77961cee63298a6bb9a1e867bf5ba8d");
                    request.AddParameter("title", "test");
                    request.AddParameter("message", "testing");
                    request.AddParameter("signers[Client][name]", "George");
                    request.AddParameter("signers[Client][email_address]", "alex.mcferron@hellosign.com");
                    request.AddParameter("test_mode", 1);



                    IRestResponse response = restClient.Execute(request);

                    Console.WriteLine(response.Content);

                    if (response.ErrorException != null)
                    {
                        const string message = "Error retrieving response.  Check inner details for more info.";
                        var helloSignException = new ApplicationException(message, response.ErrorException);
                        throw helloSignException;
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message, ex.InnerException.Message);
                }





            }
            else if (function == "sendSignatureRequestCustomFieldsTemplate")
            {
                try
                {
                    var restClient = new RestClient();


                    // -F 'custom_fields=[{"name":"newline", "value":"$20,000", "editor":"Client", "required":true}]' \
                    //// -F 'custom_fields=[{'name':'newline', 'value':'$20,000', 'editor':'Client', 'required':true}]' \
                    string buildTheRequest = "https://" + apikey + ":@api.hellosign.com";
                    restClient.BaseUrl = new Uri(buildTheRequest);
                    restClient.Authenticator = new HttpBasicAuthenticator(hs_login, hs_password);

                    var request = new RestRequest();
                    request.Resource = "v3/signature_request/send_with_template";
                    request.Method = Method.POST;
                    request.AddParameter("template_id", "cc93d9348fd415f1c54f632e75519d3296e7acb9");
                    request.AddParameter("title", "test");
                    request.AddParameter("message", "testing");
                    request.AddParameter("signers[Client][name]", "George");
                    request.AddParameter("signers[Client][email_address]", "alex.mcferron@hellosign.com");
                    request.AddParameter("custom_fields", "[{\"name\":\"newline\", \"value\":\"$20,000\", \"editor\":\"Client\", \"required\":true}]");
                    request.AddParameter("test_mode", 1);



                    IRestResponse response = restClient.Execute(request);

                    Console.WriteLine(response.Content);

                    if (response.ErrorException != null)
                    {
                        const string message = "Error retrieving response.  Check inner details for more info.";
                        var helloSignException = new ApplicationException(message, response.ErrorException);
                        throw helloSignException;
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message, ex.InnerException.Message);
                }


            }




            /*
            try
            {
                var restClient = new RestClient();
                string buildTheRequest = "https://" + apikey + ":@api.hellosign.com/v3/signature_request/create_embedded_with_template";
                restClient.BaseUrl = new Uri(buildTheRequest);
                //restClient.Authenticator = new SimpleAuthenticator()
                restClient.Authenticator = new HttpBasicAuthenticator("alexmac2017@gmail.com", "Virginia!");
                Console.WriteLine(buildTheRequest);

                var request = new RestRequest();
                request.AddParameter("client_id", "21f3f5352a6a1419b8875db11dca5dd8", ParameterType.RequestBody);
                //request.AddParameter("template_id", "cc93d9348fd415f1c54f632e75519d3296e7acb9", ParameterType.RequestBody);
                request.AddParameter("template_id", "422eb83ea77961cee63298a6bb9a1e867bf5ba8d", ParameterType.RequestBody);
                request.AddParameter("subject", "ticket", ParameterType.RequestBody);
                request.AddParameter("message", "ticket", ParameterType.RequestBody);
                request.AddParameter("signers[Client][name]", "George", ParameterType.RequestBody);
                request.AddParameter("signers[Client][email_address]", "alex.mcferron@hellosign.com", ParameterType.RequestBody);
                //request.AddParameter("'custom_fields", "[{'name':'newline', 'value':'$20,000', 'editor':'Client', 'required':true}]", ParameterType.RequestBody);
                request.AddParameter("test_mode", "1", ParameterType.RequestBody);
               

                // buildTheRequest = 'https://' + apikey + ':@api.hellosign.com/v3/signature_request/create_embedded_with_template'
                IRestResponse response = restClient.Execute(request);

                Console.WriteLine(response.Content);


                if (response.ErrorException != null)
                {
                    const string message = "Error retrieving response.  Check inner details for more info.";
                    var helloSignException = new ApplicationException(message, response.ErrorException);
                    throw helloSignException;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

    */




        }

        static void sendTemplateSignatureRequest(HelloSign.Client client)
        {
            try
            {
                Console.WriteLine("4 - TemplateSignatureRequest******************************************************* BEGIN");
                //Two roles - Consultant and Canidate
                var requestTemplate = new HelloSign.TemplateSignatureRequest();
                requestTemplate.AddTemplate("54cb9e7b076df6e9a78fc2c523aae4d78a25284d");
                requestTemplate.RequesterEmailAddress = "requester@example.com";
                requestTemplate.TestMode = true;
                requestTemplate.Signers.Add(new Signer("alex.mcferron@hellosign.com", "alex.mcferron@hellosign.com", role: "canidate"));
                requestTemplate.Signers.Add(new Signer("alex.mcferron@zoho.com", "alex.mcferron@zoho.com", role: "consultant"));
                requestTemplate.AddCustomField("firstname", "TEST");
                var response = client.SendSignatureRequest(requestTemplate);
                Console.WriteLine(response);
                Console.WriteLine("4 - TemplateSignatureRequest******************************************************* END");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace + " " + ex.Message);
            }
        }


        static void sendSignatureRequest(HelloSign.Client client)
        {
            Console.WriteLine("4 - endTemplateSignatureRequest******************************************************* BEGIN");

            //var account = client.GetAccount();
            //Console.WriteLine("My Account ID is: " + account.AccountId);
            try
            {
                var request = new SignatureRequest();
                request.Title = "C# sending a signature request";
                request.Subject = "C# send signature Request";
                request.Message = "Please sign this NDA and then we can discuss more. Let me know if you have questions";
                request.AddSigner("alex.mcferron@hellosign.com", "Jack");
                request.AddSigner("alex.mcferron@zoho.com", "Jill");
                request.TestMode = true;
                request.AddFile("C:\\Users\\IEUser\\Downloads\\NDA10.pdf");
                var response = client.SendSignatureRequest(request);
                Console.WriteLine(response.ToString());
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message + " " + ex.StackTrace);
            }


            Console.WriteLine("3 - Send signature_request*************************************************************** END");
        }

    }
}

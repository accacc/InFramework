using IF.Sms.Barabut;


using System;
using System.Collections.Generic;
using System.Text;

namespace IF.SocialAndCommunication.Sms.Integration.InfoBip
{
    public class Messenger
    {
        private readonly XmlServiceClient _client;

        private readonly string _password;

        private readonly string _username;

        public Messenger(string username, string password)
        {
            this._username = username;
            this._password = password;
            this._client = new XmlServiceClient("http://gw.barabut.com/v1/xml/syncreply");
        }

        //public CancelResponse Cancel(long messageId)
        //{
        //    CancelResponse cancelResponse;
        //    Cancel cancel = new Cancel();
        //    Credential credential = new Credential()
        //    {
        //        Username = this._username,
        //        Password = this._password
        //    };
        //    cancel.Credential = credential;
        //    cancel.MessageId = messageId;
        //    CancelResponse cancelResponse1 = this._client.Send<CancelResponse>(cancel);
        //    if (this._client.Status)
        //    {
        //        cancelResponse = cancelResponse1;
        //    }
        //    else
        //    {
        //        cancelResponse = null;
        //    }
        //    return cancelResponse;
        //}

        //public GetBalanceResponse GetBalance()
        //{
        //    GetBalanceResponse getBalanceResponse;
        //    GetBalance getBalance = new GetBalance();
        //    Credential credential = new Credential()
        //    {
        //        Username = this._username,
        //        Password = this._password
        //    };
        //    getBalance.Credential = credential;
        //    GetBalanceResponse getBalanceResponse1 = this._client.Send<GetBalanceResponse>(getBalance);
        //    if (this._client.Status)
        //    {
        //        getBalanceResponse = getBalanceResponse1;
        //    }
        //    else
        //    {
        //        getBalanceResponse = null;
        //    }
        //    return getBalanceResponse;
        //}

        //public GetMoResponse GetMo(DateRange dateRange, string recipient, InboxState state)
        //{
        //    GetMoResponse getMoResponse;
        //    GetMo getMo = new GetMo();
        //    Credential credential = new Credential()
        //    {
        //        Username = this._username,
        //        Password = this._password
        //    };
        //    getMo.Credential = credential;
        //    getMo.Range = dateRange;
        //    getMo.Recipient = recipient;
        //    getMo.State = state;
        //    GetMoResponse getMoResponse1 = this._client.Send<GetMoResponse>(getMo);
        //    if (this._client.Status)
        //    {
        //        getMoResponse = getMoResponse1;
        //    }
        //    else
        //    {
        //        getMoResponse = null;
        //    }
        //    return getMoResponse;
        //}

        //public GetSettingsResponse GetSettings()
        //{
        //    GetSettingsResponse getSettingsResponse;
        //    GetSettings getSetting = new GetSettings();
        //    Credential credential = new Credential()
        //    {
        //        Username = this._username,
        //        Password = this._password
        //    };
        //    getSetting.Credential = credential;
        //    GetSettingsResponse getSettingsResponse1 = this._client.Send<GetSettingsResponse>(getSetting);
        //    if (this._client.Status)
        //    {
        //        getSettingsResponse = getSettingsResponse1;
        //    }
        //    else
        //    {
        //        getSettingsResponse = null;
        //    }
        //    return getSettingsResponse;
        //}

        //public LoginResponse Login()
        //{
        //    LoginResponse loginResponse;
        //    Login login = new Login();
        //    Credential credential = new Credential()
        //    {
        //        Username = this._username,
        //        Password = this._password
        //    };
        //    login.Credential = credential;
        //    LoginResponse loginResponse1 = this._client.Send<LoginResponse>(login);
        //    if (this._client.Status)
        //    {
        //        loginResponse = loginResponse1;
        //    }
        //    else
        //    {
        //        loginResponse = null;
        //    }
        //    return loginResponse;
        //}

        //public QueryResponse Query(long messageId, string msisdn = "")
        //{
        //    QueryResponse queryResponse;
        //    Query query = new Query();
        //    Credential credential = new Credential()
        //    {
        //        Username = this._username,
        //        Password = this._password
        //    };
        //    query.Credential = credential;
        //    query.MessageId = messageId;
        //    query.MSISDN = msisdn;
        //    QueryResponse queryResponse1 = this._client.Send<QueryResponse>(query);
        //    if (this._client.Status)
        //    {
        //        queryResponse = queryResponse1;
        //    }
        //    else
        //    {
        //        queryResponse = null;
        //    }
        //    return queryResponse;
        //}

        //public QueryMultiResponse QueryMulti(DateRange dateRange)
        //{
        //    QueryMultiResponse queryMultiResponse;
        //    QueryMulti queryMulti = new QueryMulti();
        //    Credential credential = new Credential()
        //    {
        //        Username = this._username,
        //        Password = this._password
        //    };
        //    queryMulti.Credential = credential;
        //    queryMulti.Range = dateRange;
        //    QueryMultiResponse queryMultiResponse1 = this._client.Send<QueryMultiResponse>(queryMulti);
        //    if (this._client.Status)
        //    {
        //        queryMultiResponse = queryMultiResponse1;
        //    }
        //    else
        //    {
        //        queryMultiResponse = null;
        //    }
        //    return queryMultiResponse;
        //}

        //public QueryStatsResponse QueryStats()
        //{
        //    QueryStatsResponse queryStatsResponse;
        //    QueryStats queryStat = new QueryStats();
        //    Credential credential = new Credential()
        //    {
        //        Username = this._username,
        //        Password = this._password
        //    };
        //    queryStat.Credential = credential;
        //    QueryStatsResponse queryStatsResponse1 = this._client.Send<QueryStatsResponse>(queryStat);
        //    if (this._client.Status)
        //    {
        //        queryStatsResponse = queryStatsResponse1;
        //    }
        //    else
        //    {
        //        queryStatsResponse = null;
        //    }
        //    return queryStatsResponse;
        //}

        //public ReceiveResponse Receive(DateRange dateRange, InboxState state, string recipient = "")
        //{
        //    ReceiveResponse receiveResponse;
        //    Receive receive = new Receive();
        //    Credential credential = new Credential()
        //    {
        //        Username = this._username,
        //        Password = this._password
        //    };
        //    receive.Credential = credential;
        //    receive.Range = dateRange;
        //    receive.Recipient = recipient;
        //    receive.State = state;
        //    ReceiveResponse receiveResponse1 = this._client.Send<ReceiveResponse>(receive);
        //    if (this._client.Status)
        //    {
        //        receiveResponse = receiveResponse1;
        //    }
        //    else
        //    {
        //        receiveResponse = null;
        //    }
        //    return receiveResponse;
        //}

        public SubmitResponse Submit(string message, List<string> to, Header header, DataCoding dataCoding)
        {
            SubmitResponse submitResponse;
            Submit submit = new Submit();
            Credential credential = new Credential()
            {
                Username = this._username,
                Password = this._password
            };
            submit.Credential = credential;
            submit.Header = header;
            submit.Message = message;
            submit.To = to;
            submit.DataCoding = dataCoding;
            SubmitResponse submitResponse1 = this._client.Send(submit);
            if (this._client.Status)
            {
                submitResponse = submitResponse1;
            }
            else
            {
                submitResponse = null;
            }
            return submitResponse;
        }

        //public SubmitDataResponse SubmitData(List<DataItem> dataItems, List<string> to, Header header)
        //{
        //    SubmitDataResponse submitDataResponse;
        //    SubmitData submitDatum = new SubmitData();
        //    Credential credential = new Credential()
        //    {
        //        Username = this._username,
        //        Password = this._password
        //    };
        //    submitDatum.Credential = credential;
        //    submitDatum.Header = header;
        //    submitDatum.Data = dataItems;
        //    submitDatum.To = to;
        //    SubmitDataResponse submitDataResponse1 = this._client.Send<SubmitDataResponse>(submitDatum);
        //    if (this._client.Status)
        //    {
        //        submitDataResponse = submitDataResponse1;
        //    }
        //    else
        //    {
        //        submitDataResponse = null;
        //    }
        //    return submitDataResponse;
        //}

        //public SubmitDataMultiResponse SubmitDataMulti(List<DataEnvelope> envelopes, Header header)
        //{
        //    SubmitDataMultiResponse submitDataMultiResponse;
        //    SubmitDataMulti submitDataMulti = new SubmitDataMulti();
        //    Credential credential = new Credential()
        //    {
        //        Username = this._username,
        //        Password = this._password
        //    };
        //    submitDataMulti.Credential = credential;
        //    submitDataMulti.Header = header;
        //    submitDataMulti.Envelopes = envelopes;
        //    SubmitDataMultiResponse submitDataMultiResponse1 = this._client.Send<SubmitDataMultiResponse>(submitDataMulti);
        //    if (this._client.Status)
        //    {
        //        submitDataMultiResponse = submitDataMultiResponse1;
        //    }
        //    else
        //    {
        //        submitDataMultiResponse = null;
        //    }
        //    return submitDataMultiResponse;
        //}

        //public SubmitMultiResponse SubmitMulti(List<Envelope> envelopes, Header header, DataCoding dataCoding)
        //{
        //    SubmitMultiResponse submitMultiResponse;
        //    SubmitMulti submitMulti = new SubmitMulti();
        //    Credential credential = new Credential()
        //    {
        //        Username = this._username,
        //        Password = this._password
        //    };
        //    submitMulti.Credential = credential;
        //    submitMulti.Header = header;
        //    submitMulti.Envelopes = envelopes;
        //    submitMulti.DataCoding = dataCoding;
        //    SubmitMultiResponse submitMultiResponse1 = this._client.Send<SubmitMultiResponse>(submitMulti);
        //    if (this._client.Status)
        //    {
        //        submitMultiResponse = submitMultiResponse1;
        //    }
        //    else
        //    {
        //        submitMultiResponse = null;
        //    }
        //    return submitMultiResponse;
        //}
    }


  

   


   
}


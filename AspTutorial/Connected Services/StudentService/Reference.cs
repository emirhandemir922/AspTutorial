﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Bu kod araç tarafından oluşturuldu.
//     Çalışma Zamanı Sürümü:4.0.30319.42000
//
//     Bu dosyada yapılacak değişiklikler yanlış davranışa neden olabilir ve
//     kod yeniden oluşturulursa kaybolur.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AspTutorial.StudentService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="StudentService.WebService1Soap")]
    public interface WebService1Soap {
        
        // CODEGEN: http://tempuri.org/ ad alanındaki ShowStudentResult öğe adı sıfırlanabilir olarak işaretlenmediğinden, ileti sözleşmesi oluşturuluyor
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ShowStudent", ReplyAction="*")]
        AspTutorial.StudentService.ShowStudentResponse ShowStudent(AspTutorial.StudentService.ShowStudentRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ShowStudent", ReplyAction="*")]
        System.Threading.Tasks.Task<AspTutorial.StudentService.ShowStudentResponse> ShowStudentAsync(AspTutorial.StudentService.ShowStudentRequest request);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class ShowStudentRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="ShowStudent", Namespace="http://tempuri.org/", Order=0)]
        public AspTutorial.StudentService.ShowStudentRequestBody Body;
        
        public ShowStudentRequest() {
        }
        
        public ShowStudentRequest(AspTutorial.StudentService.ShowStudentRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute()]
    public partial class ShowStudentRequestBody {
        
        public ShowStudentRequestBody() {
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class ShowStudentResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="ShowStudentResponse", Namespace="http://tempuri.org/", Order=0)]
        public AspTutorial.StudentService.ShowStudentResponseBody Body;
        
        public ShowStudentResponse() {
        }
        
        public ShowStudentResponse(AspTutorial.StudentService.ShowStudentResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class ShowStudentResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string ShowStudentResult;
        
        public ShowStudentResponseBody() {
        }
        
        public ShowStudentResponseBody(string ShowStudentResult) {
            this.ShowStudentResult = ShowStudentResult;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface WebService1SoapChannel : AspTutorial.StudentService.WebService1Soap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class WebService1SoapClient : System.ServiceModel.ClientBase<AspTutorial.StudentService.WebService1Soap>, AspTutorial.StudentService.WebService1Soap {
        
        public WebService1SoapClient() {
        }
        
        public WebService1SoapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public WebService1SoapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public WebService1SoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public WebService1SoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        AspTutorial.StudentService.ShowStudentResponse AspTutorial.StudentService.WebService1Soap.ShowStudent(AspTutorial.StudentService.ShowStudentRequest request) {
            return base.Channel.ShowStudent(request);
        }
        
        public string ShowStudent() {
            AspTutorial.StudentService.ShowStudentRequest inValue = new AspTutorial.StudentService.ShowStudentRequest();
            inValue.Body = new AspTutorial.StudentService.ShowStudentRequestBody();
            AspTutorial.StudentService.ShowStudentResponse retVal = ((AspTutorial.StudentService.WebService1Soap)(this)).ShowStudent(inValue);
            return retVal.Body.ShowStudentResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<AspTutorial.StudentService.ShowStudentResponse> AspTutorial.StudentService.WebService1Soap.ShowStudentAsync(AspTutorial.StudentService.ShowStudentRequest request) {
            return base.Channel.ShowStudentAsync(request);
        }
        
        public System.Threading.Tasks.Task<AspTutorial.StudentService.ShowStudentResponse> ShowStudentAsync() {
            AspTutorial.StudentService.ShowStudentRequest inValue = new AspTutorial.StudentService.ShowStudentRequest();
            inValue.Body = new AspTutorial.StudentService.ShowStudentRequestBody();
            return ((AspTutorial.StudentService.WebService1Soap)(this)).ShowStudentAsync(inValue);
        }
    }
}
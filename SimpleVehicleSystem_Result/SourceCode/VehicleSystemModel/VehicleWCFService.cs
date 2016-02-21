﻿//------------------------------------------------------------------------------
// <auto-generated>
// </auto-generated>
//------------------------------------------------------------------------------



[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.ServiceContractAttribute(ConfigurationName="IVehicleWCFService")]
public interface IVehicleWCFService
{
    
    [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IVehicleWCFService/GetVehicle", ReplyAction="http://tempuri.org/IVehicleWCFService/GetVehicleResponse")]
    SimpleVehicleSystem.VehicleDataContract.Vehicle[] GetVehicle();
    
    [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IVehicleWCFService/GetVehicle", ReplyAction="http://tempuri.org/IVehicleWCFService/GetVehicleResponse")]
    System.Threading.Tasks.Task<SimpleVehicleSystem.VehicleDataContract.Vehicle[]> GetVehicleAsync();
    
    [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IVehicleWCFService/DeleteVehicle", ReplyAction="http://tempuri.org/IVehicleWCFService/DeleteVehicleResponse")]
    bool DeleteVehicle(long vid);
    
    [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IVehicleWCFService/DeleteVehicle", ReplyAction="http://tempuri.org/IVehicleWCFService/DeleteVehicleResponse")]
    System.Threading.Tasks.Task<bool> DeleteVehicleAsync(long vid);
    
    [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IVehicleWCFService/AddVehicle", ReplyAction="http://tempuri.org/IVehicleWCFService/AddVehicleResponse")]
    bool AddVehicle(SimpleVehicleSystem.VehicleDataContract.Vehicle newVehicle);
    
    [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IVehicleWCFService/AddVehicle", ReplyAction="http://tempuri.org/IVehicleWCFService/AddVehicleResponse")]
    System.Threading.Tasks.Task<bool> AddVehicleAsync(SimpleVehicleSystem.VehicleDataContract.Vehicle newVehicle);
    
    [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IVehicleWCFService/UpdateVehicle", ReplyAction="http://tempuri.org/IVehicleWCFService/UpdateVehicleResponse")]
    bool UpdateVehicle(SimpleVehicleSystem.VehicleDataContract.Vehicle updatingVehicle);
    
    [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IVehicleWCFService/UpdateVehicle", ReplyAction="http://tempuri.org/IVehicleWCFService/UpdateVehicleResponse")]
    System.Threading.Tasks.Task<bool> UpdateVehicleAsync(SimpleVehicleSystem.VehicleDataContract.Vehicle updatingVehicle);
}

[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
public interface IVehicleWCFServiceChannel : IVehicleWCFService, System.ServiceModel.IClientChannel
{
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
public partial class VehicleWCFServiceClient : System.ServiceModel.ClientBase<IVehicleWCFService>, IVehicleWCFService
{
    
    public VehicleWCFServiceClient()
    {
    }
    
    public VehicleWCFServiceClient(string endpointConfigurationName) : 
            base(endpointConfigurationName)
    {
    }
    
    public VehicleWCFServiceClient(string endpointConfigurationName, string remoteAddress) : 
            base(endpointConfigurationName, remoteAddress)
    {
    }
    
    public VehicleWCFServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
            base(endpointConfigurationName, remoteAddress)
    {
    }
    
    public VehicleWCFServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
            base(binding, remoteAddress)
    {
    }
    
    public SimpleVehicleSystem.VehicleDataContract.Vehicle[] GetVehicle()
    {
        return base.Channel.GetVehicle();
    }
    
    public System.Threading.Tasks.Task<SimpleVehicleSystem.VehicleDataContract.Vehicle[]> GetVehicleAsync()
    {
        return base.Channel.GetVehicleAsync();
    }
    
    public bool DeleteVehicle(long vid)
    {
        return base.Channel.DeleteVehicle(vid);
    }
    
    public System.Threading.Tasks.Task<bool> DeleteVehicleAsync(long vid)
    {
        return base.Channel.DeleteVehicleAsync(vid);
    }
    
    public bool AddVehicle(SimpleVehicleSystem.VehicleDataContract.Vehicle newVehicle)
    {
        return base.Channel.AddVehicle(newVehicle);
    }
    
    public System.Threading.Tasks.Task<bool> AddVehicleAsync(SimpleVehicleSystem.VehicleDataContract.Vehicle newVehicle)
    {
        return base.Channel.AddVehicleAsync(newVehicle);
    }
    
    public bool UpdateVehicle(SimpleVehicleSystem.VehicleDataContract.Vehicle updatingVehicle)
    {
        return base.Channel.UpdateVehicle(updatingVehicle);
    }
    
    public System.Threading.Tasks.Task<bool> UpdateVehicleAsync(SimpleVehicleSystem.VehicleDataContract.Vehicle updatingVehicle)
    {
        return base.Channel.UpdateVehicleAsync(updatingVehicle);
    }
}

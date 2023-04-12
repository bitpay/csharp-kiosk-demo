namespace CsharpKioskDemoDotnet.Shared.Domain;

public interface IConverter<out TU, in T>
{ 
   TU Execute(T item);
}
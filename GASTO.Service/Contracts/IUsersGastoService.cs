namespace GASTO.Service.Contracts
{
    public interface IUsersGastoService 
    {
        GASTO.Domain.Usuario GetById(int id);
        //NEY TANGOA 14/10/2020
        GASTO.Domain.Usuario GetUsuarioByUserName(string UserName);
        //FIN CAMBIOS
    }
}

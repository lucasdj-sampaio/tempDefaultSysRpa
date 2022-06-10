#region - Imports
using Microsoft.EntityFrameworkCore;
using COE000.Portal.NomeProjeto.Enum;
using COE000.Portal.NomeProjeto.Models;
using COE000.Portal.NomeProjeto.Models.Entity;
using COE000.Portal.NomeProjeto.Reposity.Entity;
using COE000.Portal.NomeProjeto.Areas.Identity.Data;
#pragma warning disable CS8602, CS8603, CS8604, IDE1006
#endregion

namespace COE000.Portal.NomeProjeto.Reposity
{
    public class EntityRepository
    {
        private DataBaseContext? _context { get; set; }


        public EntityRepository(DataBaseContext context)
            => _context = context;


        public async Task DbSaveChanges()
            => await _context.SaveChangesAsync();

        public async Task<ICollection<EnvironmentModel>> GetSelectEnvItems()
            => await _context.DbEnvironment.ToListAsync();

        public async Task<NotifyModel> InsertHistoric(HistoricModel historic, bool autoCommit = false)
        {
            try
            {
                await _context.DbHistoric.AddAsync(historic);

                if (autoCommit)
                    await DbSaveChanges();

                return new(EModalNotification.Sucess) { Message = historic.Observation };
            }
            catch (Exception ex)
            {
                return new(EModalNotification.Error) { Message = ex.Message };
            }
        }

        public async Task<ICollection<RpaCredentialModel>> GetRpaCredentialItems() =>
            await _context.DbRpaCredential
                .Include(e => e.Environment)
                .Take(80)
                .ToListAsync();


        public async Task<ICollection<RpaCredentialModel>> GetRpaCredentialItems(string userNameFilter) =>
            userNameFilter is not null ?
                await _context.DbRpaCredential
                    .Where(u => u.UserName.Contains(userNameFilter))
                    .Include(e => e.Environment)
                    .ToListAsync()
                : await GetRpaCredentialItems();


        public async Task<NotifyModel> UpdateRpaUser(RpaCredentialModel user, bool autoCommit = false)
        {
            try
            {
                var entityUser = user.Password is null 
                    ? _context.DbRpaCredential.Find(user.Id) 
                    : await GetUserWithNewEncriptPass(user);

                entityUser.UserName = user.UserName is not null ? user.UserName : entityUser.UserName;
                entityUser.Url = user.Url is not null ? user.Url : entityUser.Url;
                entityUser.EnvironmentId = user.EnvironmentId != 0 ? user.EnvironmentId : entityUser.EnvironmentId;

                if (autoCommit)
                    await DbSaveChanges();

                return new(EModalNotification.Sucess) { Message = "Usuário alterado com sucesso!" };
            }
            catch (Exception ex)
            {
                return new(EModalNotification.Error) { Message = ex.Message };
            }
        }
        
        public async Task<NotifyModel> CreateHashCode(bool autoCommit = false)
        {
            try
            {
                HashModel newCode = new() {
                    DateOn = DateTime.Now
                };

                await _context.DbHash.AddAsync(newCode);

                if (autoCommit)
                    await DbSaveChanges();

                return new(EModalNotification.Sucess);
            }
            catch (Exception ex)
            {
                return new(EModalNotification.Error) { Message = ex.Message };
            }
        }

        public async Task<HashModel> GetLastHashCreated()
            => await _context.DbHash
                .OrderByDescending(d => d.DateOn)
                .FirstOrDefaultAsync();

        public async Task<HashModel> GetHash(Guid token) 
            => await _context.DbHash
                .FirstOrDefaultAsync(t => t.Id == token 
                    && t.DateOn >= DateTime.Now.AddHours(-1));

        public async Task<ICollection<IncriseUserModel>> GetUser(string currentUserName) => 
            await _context.DbUser
                .Where(n => n.Id != GetUserIdByName(currentUserName).GetAwaiter().GetResult())
                .Take(80)
                .ToListAsync();

        public async Task<ICollection<IncriseUserModel>> GetUser(string currentUserName, string userNameFilter) =>
            userNameFilter is not null ?
                await _context.DbUser
                    .Where(n => n.Nick.Contains(userNameFilter) 
                        || n.Email.Contains(userNameFilter))
                    .ToListAsync()
                : await GetUser(currentUserName);
        
        public async Task<NotifyModel> DeleteUser(Guid id, bool autoCommit = false)
        {
            try
            {
                var user = await _context.DbUser.FirstOrDefaultAsync(i => i.Id == id.ToString());
                _context.DbUser.Remove(user);

                if (autoCommit)
                    await DbSaveChanges();

                return new(EModalNotification.Sucess) { Message = "Usuário deletado com sucesso!" };
            }
            catch (Exception ex)
            {
                return new(EModalNotification.Error) { Message = ex.Message };
            }
        }

        public async Task<string> GetUserIdByName(string name)
            => (await _context.DbUser
                    .FirstOrDefaultAsync(u => u.UserName == name)).Id;

        private async Task<RpaCredentialModel> GetUserWithNewEncriptPass(RpaCredentialModel rpaUser)
        {
            string key = "";

            return await _context.DbRpaCredential
                .FromSqlRaw("SELECT CredentionCode, UserName, " +
                    $"(SELECT dbo.fn_Proteger('{ rpaUser.Password }', '{ key }')) AS Password, Url, EnvCode " +
                    $"FROM TB_RpaCredential WHERE CredentionCode = '{ rpaUser.Id }'")
                .FirstOrDefaultAsync();
        }
    }
}
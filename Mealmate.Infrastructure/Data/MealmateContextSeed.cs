using Mealmate.Core.Entities;
using Mealmate.Core.Entities.Lookup;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mealmate.Infrastructure.Data
{
    public class MealmateContextSeed
    {
        private readonly MealmateContext _mealmateContext;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;

        public MealmateContextSeed(
            MealmateContext mealmateContext,
            UserManager<User> userManager,
            RoleManager<Role> roleManager
            )
        {
            _mealmateContext = mealmateContext;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task SeedAsync()
        {
            //// TODO: Only run this if using a real database
            await _mealmateContext.Database.MigrateAsync();
            //await _mealmateContext.Database.EnsureCreatedAsync();

            // Users 
            await SeedUsersAsync();

            // Roles
            await SeedRolesAsync();

            // Lookups
            await SeedLookupsAsync();

        }

        #region Seeding Users
        private async Task SeedUsersAsync()
        {
            try
            {
                if (!_mealmateContext.Users.Any())
                {
                    var user = await _userManager.FindByEmailAsync("admin@gmail.com");
                    if (user == null)
                    {
                        user = new User
                        {
                            FirstName = "System",
                            LastName = "Administrator",
                            Email = "admin@gmail.com",
                            UserName = "admin@gmail.com"
                        };

                        var result = await _userManager.CreateAsync(user, "Server@123");
                        if (result != IdentityResult.Success)
                        {
                            throw new InvalidOperationException("Could not create user in Seeding");
                        }

                        _mealmateContext.Entry(user).State = EntityState.Unchanged;
                    }
                }
            }
            catch (Exception)
            {
                throw new InvalidOperationException("Could not create role in Seeding");
            }
        }
        #endregion

        #region Seeding Roles
        private async Task SeedRolesAsync()
        {
            try
            {
                if (!_mealmateContext.Roles.Any())
                {
                    var roles = new List<string>()
                    {
                        "SuperAdmin",
                        "RestaurantAdmin",
                        "FrontDesk",
                        "Waiter",
                        "Client"
                    };

                    foreach (var role in roles)
                    {
                        await _roleManager.CreateAsync(new Role() { Name = role, Created = DateTime.Now });
                    }
                }

            }
            catch (Exception)
            {
                throw new InvalidOperationException("Could not seed roles");
            }

        }
        #endregion

        #region Seeding Lookups
        private async Task SeedLookupsAsync()
        {
            try
            {
                if (!_mealmateContext.Allergens.Any())
                {
                    var allergens = new List<Allergen>()
                    {
                        new Allergen
                        {
                            Name =  "Celery",
                            Photo =  Encoding.ASCII.GetBytes("iVBORw0KGgoAAAANSUhEUgAAAEoAAABKCAYAAAAc0MJxAAAACXBIWXMAAAsSAAALEgHS3X78AAACJElEQVR42u2cTY7CMAyFScRqTscsmC1zAcpZhhOwZsPpYOtZVUJVS5PGL3abF6kSi/7YX5+dpCQOIrJjm297i4f+/H4XvZ377RE2C+odTqmjQ9A1wAVk6GnCsX4OBFRvuEWIoJ6tCsoSENwWESk+jqeDHE8H0biX9qFllxtD0LBK7dykihAvdbMq0ra9KUglPriD9OzO8uzO4g1WzOlu0d3+69LJ2G9Eu98eIWcqFT1C8ggreoBk2VJhxR1bEqxooabXpZPUfJRzLhLW3lte0jgX0VYZehaqii0mcLdfOBEKGN7z6+8aNFU1FEpEq8k6tzSdoyyGC3EruQmtXLeKys05WjlqSlXRk8PDc1Ov1Yb0UVHIUTja2RodhtvQy3UeAes9/OJawZgMD5C93VjeKc0pY9ej81SVkfmUY0tU1N+rRgLngHNtoKzHSlQUoOdbDShLNVFRBEVQBEVQBNXW/I+KWiMoj18Q+g8GVBRDDwAqd1EVFcVGUCWJnKCWKIp5iqFXHHYEtVRRDD+GXlHYjYKiqqioxWqaBNWqqj4tLZhUFEOQoZetpllQragqZTXPrKIYghmhh4Rl/Vd56tqw5BxlCQsFM2sBnbfN1/2eYvS+4lw/mtvOX2WX+tphlRS12Hy5ES1bi6v9eKrwA7XPU6EYr5V+VBTlUWEIG2Cl22oDQ5dvC+jykkgHatXQqwJqyrHhqH/JtTXDO3goWJoyNbLuVf8BVGrbxwnBI4gAAAAASUVORK5CYII="),
                            PhotoSelected =  Encoding.ASCII.GetBytes("iVBORw0KGgoAAAANSUhEUgAAAEkAAABJCAYAAABxcwvcAAAACXBIWXMAAAsSAAALEgHS3X78AAACYklEQVR42u2cPVIDMQyFvRoqGk7AoWioc4LkLHCC1GlyKE5Ak3apmMns7PpH1pNlW64ogoO+eXorm5WWdV2D5vo8fVR94e16X4LyWtCQaqFYgAaDFINzfXtn7Xn6/WkCSxQSAowFYGKQ9gBJgykBJgmqGlJLOFqwqiBtAbWCk4JVC4oFySocFCwaFdDe38YtR2hUQJKgstPteXMUnNev7xBCCI/LGZ5+JalH1gBtf0apqkRRZA2QRVDUGlBrn8oBRa0Opj2BotINpVMrllYln1W/BUCmWW2g0k++nCce9QQIoaactCNN2aLqH9V0Q6gI7SNS4GNpB1WShtFqfAeNXhNJeBMFX+UV94wqSsVOyOq6xFSfP8v9Pcn1zISQKso11b1Ac4NH1k3uSSWeNPpBViLlCGnY25SR8A/EnqmUe9E+ijwuZ7aP/O+lfbxp4kmcIFue+0wYt5V7I3OQLNRCXSmpu2OJlWUp5cihpGslTzf3pPp1u94Xh+RKckgOySE5pIEhxV4en21tWVCLhpaeaiRPN44necrtM3Al5SrJfenYj3aVNHPKHcVOe+RcRRnvJ82opljMFCPoKko83WZSUypWipGcARTrFWVfmZBmUVNuaxflGNiIoEp63yjX6UcCVdocSNzNJVaL//FzYkhC2pLWVpQkSG53N6sHNwT8S6jIbqTSwrloLkCP3dy1gIoh9Qar2fAECz6lCYitpCNFWVAVYqiLyGggC7CQ44HgQ6bQsNCzk8QhxWBJAjvyQNR9GHTwXU5LRgpczkMBfVm4aI1QlO5h0bxFXbTnTHLAtb5W/gOFZ6leviH92wAAAABJRU5ErkJggg=="),
                        },
                        new Allergen
                        {
                            Name =  "Crystacena",
                            Photo =  Encoding.ASCII.GetBytes("iVBORw0KGgoAAAANSUhEUgAAAEoAAABKCAYAAAAc0MJxAAAACXBIWXMAAAsSAAALEgHS3X78AAACmElEQVR42u2cQXLDIAxFY41XOV27SLftBeKcJTlB1tnkdMmWrui4HhuD+ZIAi5msYoP0+AiBjTvn3MHKeumlG/z6+YT0zOP+7JoDNYaDcpCjzlDpuIaepCMSbcFBeaOlhwZ3+zBQ2oDY7XHOZf9O3x8OUQ/HD2VblqJKUxGrnS2qiMPm3UDKtX1XkHJ8SKq8BUhbYe1KSTk+7RZSqm+7hpTi4+4hxfrah5I0ZCL5vgyLme3xeuu47kUV4m7gfRlcyNFS6n/cn11or4y41BRyYKyCGEXEXI8AFoJFXL3MOVRCdXCpl9BqioGU44y/lwvWkqqoJiVpKos4Z7q1OJICb3wt9wQxpyqSUBNX7JBsk6TUhBiKUjmTSh5Va5kOPxVQOcrQUhXVBEkTFqHiU2qGHRt0Y7J7CaCkrSQPYg5I6D8JZY3jVK851KYA1pQ1V9fxeus4cypfek0VoZYviKVRccEc4YyEglRBIR2UhkVSGbnGEqYaRa3NWrW0ITL0JHpdog1b62mA0lzdc9tiitICVYKqOGzo/XoG8XiqlN7fut3Mqiju/etSZkRqRUXcnWnB3EDll3HspsNh/QWFWnKnYhU1t2lWanqQaxc0mEvtNsbYVWxmXpq6cjtsmlv+gdoSp0IglsBxPq5a2iKGZOYoo9ey4bmXwVJ7PbUO5BD8N/RyZr8ty4hYhcVehwIzt6SDP4WRfvlCbALhem36NZzdaziLvH7t20K0t+R7E5m5xOw6CwoZq2pdsthabwOkxRhlRzzs0BDPoSE7hmYHG5N9IgveGQln6+eKWQ9f23F++0AEL6iaYCFCRhFGNP8Rm+ks4teJpcxqSHvsQ1taoDSAVfnptpATSEea+RhgLLgQxJRrmwOloT5E+QWsLjxtq2B2/wAAAABJRU5ErkJggg=="),
                            PhotoSelected =  Encoding.ASCII.GetBytes("iVBORw0KGgoAAAANSUhEUgAAAEoAAABKCAYAAAAc0MJxAAAACXBIWXMAAAsSAAALEgHS3X78AAACzklEQVR42u2cvZHDIBCFrR1FTq6CK8qJY1dg12JX4NiJi7oKLnGqizTD6Phd3i5/IvPIEvDxFh4SMC3LcthTOM3aGZ4vJ0jLvJ7vSbPck4aiTDjPr2/IMy+/P6rQxEBJwCkJDQ5qBSQNJwQNDQwGqjQgaWAQUOfLaakFkA0YAlYWqNpUJKkuNqiaVSShLhoF0qp8ro+jUSDlwqKRIOXAotEgcWHRiJA4sGhUSKmwaGRIKbDUXrMc7w/ntc/tyr435n5EImk1He+PYEUlGwGlKqszR0CKLXysGtbnfW7XLHVy3TuVCLPcyvjul1IvodUk2dpbZUnAcoUgHZSSWbHcVpeGFVSUpJpyK7CFIjlA2FQFU1So4LbRLzcUY/IUtwc1Jg2/FASl7cJLVpoTftSaMkoBphbDpwQsQoVdTOFd//F1uq5rGrDM8KPSSlpB2ID4rsXAQsKca4AUqyzbc0JzvyZAoZ2zy5GvvyWBqXfmiNcuGgoqCgpZQW1YJBlqZmUkKqYJizQKLlmh0MiIsgjUUriVzEM09HpK1GvF0I1GtRewdFo/NlALrVlD/jBnXsIExpQFBY16gyRVtrlXQDFzxH3U2+1BYVCv53syt0n0Zge4ZTTXIVBuAdDf6qQm57nlgk9haujgEWCcoDjht/UrJdUVUlFqA26X/5BU7LvUhfpclRpeufnOqJZMWT7Ifcdte1duU7H5jkrEmeeMfpy+KnZRme9/yOVErrCDzvVyhmd0XyYxqPwD9Xq+J86X49r8FMI7qTpzDYDFlk+j+qrWkm8/n1NRo8EKbXqcWw4Hzfy8fdQoqorZQhvszHuHFbvPOGrU6xVWymbsaHvQG6zUHetJPqoXWJxt/cmGs3VY3LMPWM68VVg5B0TsR45ogDKB7YfYNKquKo9FqglY9QdtuYBpQGvy6DYtaN0cBhgDzZZMkD4L0uXxklyQ2jB86Q9i40hvGNs17AAAAABJRU5ErkJggg=="),
                        },
                        new Allergen
                        {
                            Name =  "Egg",
                            Photo =  Encoding.ASCII.GetBytes("iVBORw0KGgoAAAANSUhEUgAAAEoAAABKCAYAAAAc0MJxAAAACXBIWXMAAAsSAAALEgHS3X78AAACMUlEQVR42u1cO5KDMAwFTaqcLltk2+wJds+SnCB1mtwurbfKDpMNYIyePkieoQOs9/wkJGO7L6V02u3z62PSiNv13mvbuLNAyBwRrc9xth6tqCFIbmDId4sR9QQhNero/tiJkiZIqn82orQJQtuzmihrBMHsK6U0X8fToax5XvJaa2sIkjhsXux61l0NZv/WVcSFJSRJLZioVq5eXY2rUWTwt+u9nyvIq2PUFl2uBWN4kmqxjqYHknHp8fM9Kf/9+SJWWI9hVo9RcyTV3qMyHyWhplbwaHWNYSerKrKmLsqcqS5lIE9q0lQVSaqJEyCSrHeqIm0l7c+XvjVASyqLNGOTVH7kptZ7N/JDkix+BV/dL3RRbEpRSDVJxirSzJ0slCa17keaakrXU1ATWp2iRHlVE5QojfiD7JO24HYSbZcEVSoqf0Upup6mclB9ZwljNT1wm3Au+lsaOKCLKsr1Vy+jTxKVRCVRSZRsCgIjijtF8DydMmzD8i5dL6LrIZX8R1S637jbbUpR6IEhz8arxShEgSxBFncf7yYzybtbSKn2H1GoaRcEIMQ7Tazh5AQmHf/U1plbXBU8hXlyv56FRfkWFuN3nfB/PdepRG4aYtg0FIGsWmy5AxSxSz33FAcjqwVDmDMP1toe5oCI4+lQ1tis2rmX40aaTtIYy2qfBbW1WUo2u6yNnFVbYAdtaSgM2Tf0jDspl5ToB34Y4OtIcwDifp8ZouaALm0aHw0Vojy2X4a3VRFoVILwAAAAAElFTkSuQmCC"),
                            PhotoSelected =  Encoding.ASCII.GetBytes("iVBORw0KGgoAAAANSUhEUgAAAEoAAABKCAYAAAAc0MJxAAAACXBIWXMAAAsSAAALEgHS3X78AAACV0lEQVR42u2cvW3DMBCFqUOqNJkgQ7lxrQnsWZIJXKfJUJkgjVulCBQIjkSJ5L37IUnAlX8ofXp3PBPHN0zTFLTHeTxFL+Lj9jloX+MgCWoLyO3lNfq98ftLHSAc1BLOHpDUsQSIhgYDNQPihrMHDQWMHZQ0IClgbKC0AaGBFYOyBggFrAjUeTxNVgGtASuBRS1AmhW/V6+xKsp6qKFCMQmUNxVxhiK1CCknFKlFSDmwKPTBo6ha1ZSqKmoZUgqszVVPEtLz23v0/fv1or4SqueoPUhHP4Meq4qSUFPuzaPVtaUqsqoia+oijwkcCWsrsVNNNymmKLSaOCFJq4q0lXS/Xv5eltVJmrlpCcdaSD6qijTV5GkVVCs4paptN6DWnjh3yEmoijRrJ+ulwjJPUS0hh4ZOWorxVnhSDWpyDUpDMcg53e0eaI2nDsh4wellzCUC1aYc1NxdUVbLA28lwryH3hXVV72eozqoDqqD6qDc7hKg5qYQfhs/tw7mtDyWfQg99LRzlOdNuiioGsKP8+E8tv+QlwvXnqvnqBxQiPCTUBX3HGtdd+Q9LKTC+x8oVFJH3BDiN030cHLemHT5odZnbrErONZnHj2GZqEp30IzfgjCO5yeK/lojmrlz/KRQ467ybx2WEdPgh5a9WqFlXJc9nB5UBss2JnimmDleCAkF5zeYeUaRWRV5l5hlbhpFFuOhGDfLILDn4XF7ccqME7HH1b/KEtn/UpNa6CglurSUBjSyg3qcScVkmjbNjioNZVxgHtccSXcEwcNH84SHycpMCZAeRw/sAu/kSG/PssAAAAASUVORK5CYII="),
                        },
                        new Allergen
                        {
                            Name =  "Fish",
                            Photo =  Encoding.ASCII.GetBytes("iVBORw0KGgoAAAANSUhEUgAAAEoAAABKCAYAAAAc0MJxAAAACXBIWXMAAAsSAAALEgHS3X78AAACJElEQVR42u2cQXLDIAxFY4aVT9cu2m17Avsszgmyzia385au6NCpPQZLQsJ8ZrJJG/z1/CXACQwhhJtW+/x+L7r48/EatLR6TTClgW+BrQVvkHZUGpxEUNL9i4KqJX7vuhLXZAUlKVRdRwiB5fXx9Ra4+uLUxKXLlBhJYKqgrAPivKFdQOLQfclUk4DVhYs4YukS0pmY3K3j9ny8huz1Zq9uKo2ve0i5cbqjpYD2csRKc0CQWa96T7ncuB1SDqnHmoIOboKjWF3lrbhpnadfYeNyN+dob/GOptC0wEVXReM4i2BK/161RqGIo5i3D2qdp5CTVlrFPR39fE0oLTvKS9YnLjjrPAXtKYNvxT2xTy1grrUUy61r1M+IgqpZh3Kvlf7fGX2xoPvWAJWkI6cu1yqko9Ta0kWpb751SLX0YGYOULyzezgKoAAKoCyPiCRQFp9tc7f4wKCr1KO4yqXrmau4alzuQ3yZqlFWYG3BSd+j6HScIjHhJNzRKxTyP6AoderqwMhPD0rszrmyH5f7UPsreK9VI86AS/uQhvPvCxf80i7vl3dY6+W4aWvU4yzqWBR35qZdUHAVHHXKTZujXq8jIGmLB1Iw01HYXYV5VF5dKnUUdoBiT7HsLvXeIJ0+96DlLf3Vzj1o2V0UveTTfqyc8COuEaf9VHKURXdJ6BA9kaw2sGZOJNsTLgmt6TPujgKiBMXVj1lQRwHnNq36pwaqtfYDV6SbVHeGRw4AAAAASUVORK5CYII="),
                            PhotoSelected =  Encoding.ASCII.GetBytes("iVBORw0KGgoAAAANSUhEUgAAAEoAAABKCAYAAAAc0MJxAAAACXBIWXMAAAsSAAALEgHS3X78AAACRklEQVR42u3cMW7DMAwFUJvQlKUn6KGyZPYJkrPEJ8icpYfqCbp4dYdCgeM6sWiSEmnRQIEibRLh5YuRbEvtOI5NqePUHVFvfr99taXa2uaEmsPcPj5Rz+9+vovhiUNNcbAwWDxJNBEoaZw1NAkwVqgIlBMnFxgb1Kk7jqWBJMHIUFpStAZGxSJBaUyRVLo2Q1lC4kgXGspCV5PAQkFZTREHFtSIFHsEZgoFTcUHBgtqTNMWLKgZCYMFtSN5jWJOFXia0rDAkbzrsaYKPE2eKNZUBS1pOlz7x+/D5awOL2j8RKdopeBiquLEGTTCYP+etUZ5Efdibh/qcO2TulWp4j799gs5USwfQbI+ceEcrn3xIUOwkp74mqXAwFoXS61r1OeIQuWsQ6nvNf2/Le2LBT1YA8J0R852gVWkta611C5KfQvWkXK1x0fmDsU7uvdEOZRDOZTmb0QSlMZz29xHvI+qqq5HSRU0zd8NoEvLJ6ymaricHz+qapQWrCWc6WOUdgJnI33ASfhE91DIn6AodWrvYOSzB5i4c87sh8s5+yX4p/vMc14E3QKXM6Xz+9D/3ZDvV4yXoXyul4C0CMVZ1H1SXFmaXkJ5qjxRm9L0Fqq2VK0tS3ubKO+CiK5XA1bKIsfqa1TqStAkqL2mCrNcNjlRe8MSW1O8JyzxVerTiXPT2FzSn23fA8tnGihbj1SzN0ssHVtfw3f7yQ2lKV2q94/SAGZmR7JXYJJopve4W0OjwM3HcbvZNTEVDjPoLdHetuSGpZaOX+clyzP41USdAAAAAElFTkSuQmCC"),
                        },
                        new Allergen
                        {
                            Name =  "Lupin",
                            Photo =  Encoding.ASCII.GetBytes("iVBORw0KGgoAAAANSUhEUgAAAEoAAABKCAYAAAAc0MJxAAAACXBIWXMAAAsSAAALEgHS3X78AAACX0lEQVR42u2cO27DMAyGbUKTT5cO6ZqeIDmLc4LOXXq7rOpQGEhd29GDP0k9BGSKY5FffomibHH03g+9vW5Oo9P3j7esf+fr83usEtQaTK6j3PcLaSNy6C0OoR15BofqCwJKCpBk36ygNAHBbfHeZ3/Ol5M/X06e417cHy67zBiChpVrZ5UqQvyp1aqI2/amIOX40BykVF+ahJTiU7OQYn1rGlKMj9Q3UH5X7692NNyrNEAyHXncrpvGTvNdPSXazfUkIe0BkoZ25LP60IuBlHI91xAkTTWlOv24XT0SWBAo6XlJA3SKqqh0J6WURbUoAa0qKnXYSYOnrqYwVVHpalKLer11UB2UxDxFNcxPEklzV5RVUBa2TIpRFCcsKfDw1362FpjTfB+n+T7mLj4l1emk4Gx9nwNLCtIS+Uga0vraFIc15jmnAegI1tF9NAOB04S0BctqVHTakGKGYUhfKNBuKKDFznsIaGRBTUf3y+mD077qU5hcWEseTDU7yXmfnhRLgEKGcon5rysKpaiQ1146qEK2OTTa886vqaFnGTqttxO0HLSuTLKgBouQ1g9cHGLohIZiSUC5ff1RFFf0W7Z6U7+3pib47kFNEfHfHGVlTaX1pGbvYbDplbmlaGri9WlUrhYLKen1aWtpTUwAgASL1s/BhPraDw0F+tiPoQX6FhT1+jbM0O5RWcgJ0Bphwc4U1wQLfkq9dFg5RS2qLzfCZWt2tR9LFX6g9lkqFGO10g+LoiwqDGEDrHSbNDB0+TZojTu0AxK17cRA7Tm2TpFSfis5vEVBxQLUgrLVfgBAXvErKZrGgwAAAABJRU5ErkJggg=="),
                            PhotoSelected =  Encoding.ASCII.GetBytes("iVBORw0KGgoAAAANSUhEUgAAAEoAAABKCAYAAAAc0MJxAAAACXBIWXMAAAsSAAALEgHS3X78AAAClElEQVR42u2cu5WDMBBFYY4iJ1vBFuXEMRVALXYFjp24KFfghFQbscfLgpA0H40+Cm0fQNdPmpHQvN5a27V23EyKm16GM+rfedyfvfQz9xKKWoO5f32jrje8X+LgWEEtgLBgQsBxQWMBJQXIBY0aGCmolIC4gZGA0gRoCxgFLDSoy3C2GgFRqysalGYVcagrClQOKqKGBTVBWkZATMILNUHCwILaIMXCghohxcCCWiGFwoKuNS9YRpOaTtfb5ufzNCaHuZtHSULaAyQNzZVjJR96IZBifk81BCGlmmI7fbreWIF5gcopynHA2lMV5N5JKWVBKUrgVhWUkFxKgIemJj9VQS1LlSznqAaqgWqLZShhfpJYNDdFaQWlYcskG0VRwpIC31trWeeorQRz6Rw2+ZSCNLxfPAfJjgAs38/TGA1LegiDNKT1b2M6nGKeMykAuWC5rpMyEJiUkLZgaY2KJjWkkGHocy8u0KbLoIXOexzQQIOaXNfD3IPy+YpfwmBhLe/6oOROUl6nLYolQHGGcon5rymKS1GP+7Nf15e0RqyoXPeXQiKeuqGnGfovKMzwo+igdmWCBjVohLQ+VGY4ho5vKJYEhL3XH0VRRb95Gp0PdvS9NjWx7x6UFBH/zVFacqpUb2r2Dryqzsw1RVMVx6e51mqhkFzHp531ehrPJHBuB7tAZbEVLBEgjgoenXNULYtln6rQw8m87SwERL2SYfnWGHunByXCCinEDsqjSoIVWq0enHCWACumpB9lEJHbuU+MowbKciQnWFiPFhJvlq7Taz1C5fpDZoukERiV0w8pKE3AOMy22KzbpIFx27exmwF+lnJRQ5PwthMDtQfts/kA3MrdJG0mew2GpT6WHym8Nz/bD/g75mXa3GFNAAAAAElFTkSuQmCC"),
                        },
                        new Allergen
                        {
                            Name =  "Milk",
                            Photo =  Encoding.ASCII.GetBytes("iVBORw0KGgoAAAANSUhEUgAAAEoAAABKCAYAAAAc0MJxAAAACXBIWXMAAAsSAAALEgHS3X78AAACEklEQVR42u2cO5LCMAyG7UwqTgcFtHACOAucgJqG29GaamfY7MZBsV62f82khEhffil+xIoppWBth9Nu1onH/RmDAxs9AMnBWPMbCYvSivoMlDO4KUBpcGKgfgLRevLS92MHpQ1I6/5soKwBifuTUiq69sdt2h+3qfR/pC4u31w4oQGr1NfmIXH53FyqScFqXkVccXQHaW08XUJaE9cQOrbH/RlzE3LSOKpVNVFjzI7MD6ddkhxpvy5n0rRgc71FyZF8LtahFkhrf8NlXdcoSr0aLNRUIywzRa2pN5I1ipx6vappSVVmirIszMWK8q4mLbj/qQpvPaqieq9NLsdRNdSnafpVl3pWkAekndPUq21YUPVbz2KYgOEBVg8qBlVrfQohhAFvvO9stLrxdMmEqrbX5Zw0l13c1KjN9RYt15uqK+ZeYY0encrBMp3CwAAKoNRBkfbfoSgYQAEUQLmxz3kwQFEUhTcfUg+gNOvTL1BIPyiqWE1/QEFVUBQ/KC1VlX4VLLUSOrfZoqqokg0FjZXN3I7U7Af5kttYpUFrq8msRpUEagEpqyhpVVHVZXm8I4SAQ0Pfxtj1eT1KbN2eAKXG1OWZYrGjsq3BEj183QosleP8NcMq7dfQdF8WzofqqlGMx54srKA8qovbl6YabUm1iVuc63l12uLhiDcDnELjCEi7EaAaqKVAqWaR1iagarQ3YpGV4ZXzg8sAAAAASUVORK5CYII="),
                            PhotoSelected =  Encoding.ASCII.GetBytes("iVBORw0KGgoAAAANSUhEUgAAAEoAAABKCAYAAAAc0MJxAAAACXBIWXMAAAsSAAALEgHS3X78AAACJ0lEQVR42u2cPW7DMAyFZSFTl56gh8qS2SdIztKewHOWHKon6JLVHQIDRmD9kCJFSiLnGLA/vEcxkshpXVcnHZf5HH2J+/KYpN9xqgkqBGT5/Io+N//9isNjB7WHkwICiXd43ODYQG2AKOHkgOMCRg6qNqBawMhASQPiBlYMShsgLmBFoC7zedUK6AhYCSw/AqRN8al6jVRR2q3GpSwQqNZURAnLjwYJa0M/GiQsLO8GDggsP6KaMLCiyZwb0sf3D+j3z9tVLMH7ViBhn6GKoXMUxIJ+xLyEgSWmKEy+4cxRYOuNqqaUqixHYRRVU02aV70jVZmioIoaPTeprKMkC0es/ZqznhRky1EQUNpXOw32a1JRErDNegaqYVAt5aduFFUb+kmqIn/fMtGuNjWKet6uovtNzVlPK6yTxpeKwbK/MLbqdQLqvjymo+vJFqYoA2WgDJSB6iL2N1y8c6872LbymaIMlAgos184P5mizHrMoMx+x7YzRZVYj1NV+w250vtRXDuhoWvUooqCwJI+fAheyOc8nSn96NpqiiqqlgVbgOSc4OHC9sG56pI+nUk2No5wZTGn0TGZzHuvrXK7QbNWvV5hQVpms8uD3mBB+4pBdVQvsFibr3uBhW3nR1XmrcIqmaZRPHLEOf3DIijms5BM+9Faa1FO/Ol6LBLlDKmuBm3t86baQVshYDWgcY9tYwUVgkYBrvYgwGqgUuAw5Ym63QOLV/wDjXuVOGqaM40AAAAASUVORK5CYII="),
                        },
                        new Allergen
                        {
                            Name =  "Mollucus",
                            Photo =  Encoding.ASCII.GetBytes("iVBORw0KGgoAAAANSUhEUgAAAEoAAABKCAYAAAAc0MJxAAAACXBIWXMAAAsSAAALEgHS3X78AAACGklEQVR42u2cPVIDMQyFvZqtOB0U0MIJ4CxwgtRpcjta0zGZxQ5yrCfJtjSTMivp8/PvrrXlnJOlvbw9/RvA+XTZkrHt1lA4EGowNQFuaEVdJymdGPLZKqA0Ezj6RPkTBYUO1jSGnLPI7/n1MUs9SyoeyZi6FeVBRSrxzaQiZKwuZD0CrKlVJBn/UpB68lgO0r35LAnpnryWhdSaH6XF7Xy6bJwTjKXV1JIr3VrRel1tT38eVbLvj3fWHurh82vT6II1cRT3eig1caHUQB3/j4BXy33XgNQDiPNctNpSSthZDwUI6as2C+4INSEAcZ6JVBiNrKKaz94YSqqi0SFpGc26bpJuNEIHZqmyHt/H7rf8Xq9JUbNuVyRnP/I4Hkh2PanYousxx6kAFYoKUDag4oAuFBWgLNZS5CmYUFSszNdRFYWaDEB5URXkKJj9SjmWB/5b04N/GilYS780YtAW/mjk4NF+rvfBlFLDN0IxmI9tGkfRcXrgAdTIb46P53S/oEYfp9CNEl2PoaY/oGL2C0V1qakIKlRVtt1LIKXVdcsALbE6v/lGKj7G5+UcNxd6by7EAN4w68XAzhijVumC3NyWvQEqfrFxVliQq7KzwYJevp4Flsp1/pFh9Ra1cFUoxmu5kW5Q3tUl2ZBi9aM8Vf1BxCJekcwSGNI3rMadVvk2LT/wYoDHZCQSsqihpwLqP3CtZtGtTUCNaD/hQ4BuzASZ+wAAAABJRU5ErkJggg=="),
                            PhotoSelected =  Encoding.ASCII.GetBytes("iVBORw0KGgoAAAANSUhEUgAAAEoAAABKCAYAAAAc0MJxAAAACXBIWXMAAAsSAAALEgHS3X78AAACQ0lEQVR42u2cMXaDMAyGQY+pS0/QQ2XJnBO0Z2lPwJwlh+oJunSlU97jpSbISL8s22IOWPnyyShga1yWZSh5nC8nVgDX+TaWjHO0BJWCMr++sc69/HwXhQcHtYbDhcI91vDQ0GCg7oC04exBQwFTB2UNyAqYGqjSgNDAxKC8AUIBE4E6X06LV0ApYBJY1AOku/Hcmk3FKO+phkrFLFC1WaSZitQjpCOpSD1COgKLhjh0jGrVplyrqGdIObCod0hcWFPpAF8+v1if+/14LxrnZGnTI5TcLy89n2tVqr6aSlrDtWnvuha2EdImKYica2mNtTVXTZYGWZmLMIy0bSoFSTOGlFXUgkkWsZD1na6WVH+0ilq0CRFT/CkOUABQ0vnJY9ppxLaep8KoSL0AFaACVIDqCFTrj3y1niSQp2Ai9Sq3SRVU61bRdb6NqRW3YRMw9Vq2irz/kl5ioEi5gqBKWYUcl2oMusR4VHPw5pM5skSwgIUaY73OEz6Z1/q6Kp4eSEG1WqFrpB3cKMu3M+ixIvWOgIr0S6edC6NqqbX+gbK2yttb5q09MhPaFsmq3xyIaDM3d1f1ts78mU1P56jeJva9bWlRHmjUUb1YxdnkuGtU67C4O0FZqdcqrJztsuw5qjVYsD3FLcE60gMh+65XO6yjjSIOd9KorSCVth4RtxwZBv/NIqTtRsSgvAPT7Pij2j/KUzpqWAQDtbarhGHIVm7QHndWKYlu2wYHlbJMA9xjeWLRPXEs0YdT0sfJCowLUDUefwaUrN/XFndVAAAAAElFTkSuQmCC"),
                        },
                        new Allergen
                        {
                            Name =  "Mustard",
                            Photo =  Encoding.ASCII.GetBytes("iVBORw0KGgoAAAANSUhEUgAAAEoAAABKCAYAAAAc0MJxAAAACXBIWXMAAAsSAAALEgHS3X78AAAB6klEQVR42u2cMZLCMAxFYw0Vp2MLtoUTwFngBNQ03I5WWzETskCELUty/DVDxSSRnr9sx46VmHnwtN/9j8iB6+WWPP1ceUORAii5VsNSbUWNA9QOrOa9TUBZBjB9Zq3nqYKq7aynDyqgIgCq7hMzF/22uw2X3qPmb7vbsIaPi4ak6atrK7UEa9Eq0vS/K0glGdEdpNyYSDrURhr6PWx2HmUJ6X48PDmzPp2TxXxLEh8Nndv1ckuSFQxCyslgEfolmREQyFRFUJMMFgESUk9VVRRFTdM5VDRbeT34HZj16Zwe/3lMQMeqGoumqdTzVB1F68SjpiA6c2GnDlDfKApzp4Cpdz8euKQfKr0+N/2QehEVFX1SiVEPoAxBYcSDogAKoAAKoLpZZYCiAEoZlHRLGYqCARRA9QLKatup1MbvwTQM8m+EoCgYQAGUYf/0BMqyn2px7RyKEqjpHyiMflBUkZpegoKqoKhsNb0FVUNVLe8Sf1RUbyk4t7+J1Btknz19BKWpqlZWDLIVFTUFtcCLPynACVCcKVaNpbtT6rkxLL4whMtx/pZhlRa1CFH3JHq5EWZebrUfdb+itVykCj/qinrVih4Kq6nsqjXuLFLSqmGSRXnJ6StQSUCa9woHai7Yb8xrwEjeBUtbsT9IvFPZZcwkrgAAAABJRU5ErkJggg=="),
                            PhotoSelected =  Encoding.ASCII.GetBytes("iVBORw0KGgoAAAANSUhEUgAAAEoAAABKCAYAAAAc0MJxAAAACXBIWXMAAAsSAAALEgHS3X78AAACDUlEQVR42u3cvXHDMAwFYJPnKk0myFBuXGuCZJZkAtVuPJQncJNWqZQoOkr8A0BAfOpcKCd/eYCoswg3TdOp5XEdLskXcBvvrtV1OkmoEMr4+pZ8/vB8NMNjh1ri5KCU4HGisUHNQBw4MTQOMHKoFkASYGRQGoA4wUigrsNl0gQUAqvFqoLSmCKudBVDaU8Rdbp8T0hz+nMWucVQlpFqsHxvSKVYvkekEqykZi6J9PL59e/z98e7igbvNSNpSpbXWBISKSK96x2xL5WmygMpDcufcJSXXo9piqUKiSpNVM9p2kvVueUFpaybWixAo4mysLiUWpSuU6WyR4UwWi9Cm0NpXIXvQqGJ75cflgeaS2/Zg1Kbc8k5pqGovqQ0lkd/SutT6FFWlgeAAhSgcGiEsvLogkQBClCAApSiZzUkShLqNt5daFcAjr83XZAolB6gjg9FfafjvnMu38T7hUJDR+nRQyFV4bJDolB6AlAov/B750hUTen1nKqtXQybieoRa2+rB0qP4q7XU6piG4eiieoBi2R3FSUW9W95FH8vdY+x+OvT1n74zG7mRyzBnB3r2dv5Lc062AOa//mp52DuAfdDscVSrBk9Uj2bxUIpUgyzIZ32oxGLYoANKZS2dFHPkGKZSNYSjGsqGeuMO+75dmscDiARqC20Wrj13VZicqJrNYezZDSRJIwaKGvHD+V3fjfYnAHwAAAAAElFTkSuQmCC"),
                        },
                        new Allergen
                        {
                            Name =  "Peanut",
                            Photo =  Encoding.ASCII.GetBytes("iVBORw0KGgoAAAANSUhEUgAAAEoAAABKCAYAAAAc0MJxAAAACXBIWXMAAAsSAAALEgHS3X78AAACE0lEQVR42u2cO3ICMQyGvZpUOR0U0MIJ4CzJCVKnye1onSoZ4smCvHpYkqUZOh7Wxy9ZGq+11FqLFTue97+L+fz4WoohexkJo7V7OI/eNwLkIq2o1mEuB7XVJwbqxxENJzSgsYPSBKT5+2ygRgMSX0+tlfQ6nHb1cNpV6vdIvbjWZmIRGrCoaw0PiWvN4UJNClZ4FXH5MR2krf6gyoPjeV+1t/3b9YKqW17f3hdKCYH1C4oxu10vFQtpy/vbdupZT4kGpammrQ5TPouFBREgacCCKJCkbXiO4oYkpSoYqSZrSnoEC0pAk/gDINWEU1VIRYkn8xEVuBdVpaJ6FeVBTW1fR+nz3NVRlA1AckNowy9Dr0dRmcSDhV6CEk76HHkKZgYRXlEj2h/IsEOCirTjSYYkRAkd6bwFCckgKK+QSlF8hpMbknaJAAnJgKI8h5qaojyc1XWB6jl/n7F1EVVUNDW5a2FG/gGQapq0Kea0+z44QfUoytPOl6GXoPzUSmv56Q8oK+FnddfM0NsCKpP6/2HnTlGmer3ZVbV22AKpJkIyp6jKyvbOqSY3itKA/+x8cxWUFVVZUehDRY2GpQUJc1oufl9vS6WtqSKsb+i5B9RnFDDAtMOsy6e8Kit0+ToCMNHL11HUpXKdPwdE5MgRWVAzDbHJsUhIy0Fbo0C1C9aA5nJ0m5YjYYYBYhykWLjxklHsG5+azChNOic+AAAAAElFTkSuQmCC"),
                            PhotoSelected =  Encoding.ASCII.GetBytes("iVBORw0KGgoAAAANSUhEUgAAAEoAAABKCAYAAAAc0MJxAAAACXBIWXMAAAsSAAALEgHS3X78AAACMUlEQVR42u2cO3KDQAxAQZPKTU6QQ7lxzQmSs9gncO3Gh8oJ0qQlFQyzDqD96ItUufAw3seTEN5PP45jpyUuw3n+MY/7s+8URc8Nagkjjfv7x/x5+PnevA43SHJQKZgljJpYguSARgZqAtQKjDS05qA4AW1Baw2sGShpQNTAqkFpA/QfsBawqkBdhvOoFVBru4pBWYHUyq5sUNpTjQpWFiiLFrWCBUeDNGXD1htCsVESkE7XG+p7v1+fLGaBtrt9ut7QkHKA1pq1axSnTTWDrrELYxZ4gURds8ATJErY4jWq9eBKr7dnFUjaJJlu1UZ56pdaWgVeUo66ZYAuIj/1jpx25jpzrbUKLPVNaedd857n2qgUMOUDIbUqUi/HKO4izpkyUcwDlB5jl3UKjgzCvVESL9PgrRunss1VjaJMSfCSOtR1CwISLt4sQ3L5rmcZEotRVlON1SgvkEhBWZphEQNFBUnyXwd43J/93uL3iPibRQ6U1vm6MIowlsuBAlSOUVHQmVPPW++kukZpmaFJlyvOoLSkn1Yro5gjbHoBVWuVxYlNl0Zx3Ii1pdQvoKJVMG6UpE2roGqsohiQhtoXTz2ETZugtFglnXIoo6RhaWo3yPfrlXTanICwe/bIp6umQWOAcRuUs7ERvafY26qX3H3F6Keep0a0ZPN1bOenAmU5FVkPiLAIS/TIESupqOIQG83A1B2LpA2Y+oO2JIGZPLptDdgUcRhgIbjScHe8pJf4AzROxKArpoU1AAAAAElFTkSuQmCC"),
                        },
                        new Allergen
                        {
                            Name =  "Seasame",
                            Photo =  Encoding.ASCII.GetBytes("iVBORw0KGgoAAAANSUhEUgAAAEoAAABKCAYAAAAc0MJxAAAACXBIWXMAAAsSAAALEgHS3X78AAACgUlEQVR42u2cPXKDMBCFYYfKp7MLu7VPgM9inyB1mtyOVqnIMESGlbS/QppxabT78d5KSKA+hNC1tt8GrY5vj0vyHfr++umrB7UGk5M0xTVyW89pvWViHElxX58d1JyA5B3nhkYKSgOQWBwhhOLf9X4O1/s5UFyL8kcZU5GirChIJEYLd8uDug4BiSLuw0Aqjf9QkEryQBfz2+MSuIr29Bz/BXF6vXvuIp+SDwoUF6QYIElgKXntgtKEJAEMmx94gJT7H+yKBGYlAzRqkrWGgQWeEuJSFaaBhRHOg6rAm004QW/BgiPXpmLrHbl9UhVIq4liPqRR58QVZbWYo0FJqMkLpJj9oDYlTc8xcPQFtUHi6hNqmWDG+inpe20/t9MD6XoHtVmOK4Y24cSCsrTmlDMZxfSTq6plnTKlqOk5Bu61clPWK60LWFgp/ZTGBBRQOCZ5p9e73wImrTygVA7HKLcHzK31uKYEa2DSGxTmpwfr5LQUBtaUE+vDwqoDeFGSNixToGZLWVy3Amu2k3huc60oqzNyU6BSLKelKsC+pHBEJS0XDAYrgKxvPAwe1TOvMki+Y2WiRnnYxoKuw79MZa2lKCRVTesFTfCuHAwACssPNdgutqpAPZr+Kcqb/T4BplhdiO0jtF2YnFHPa1E3+wiTK23rz3OfbBcF1VRFPOFMVQe1mjjUubUZHAWFVRU2WM+WIxv1SvffPEDsup2PhqRepdb+gAiT56aipAo75zMbBSSU9SzB0oK0az0NG8asKD3CRVv7prh9pa73lXo796CdpNHOZqGKkeRYJIvAyGOilrcFS3LE0E4kk7SeVvDuz7jbSyo3sWpPTUxJGtM0B4tfcd6kkQJLQDgAAAAASUVORK5CYII="),
                            PhotoSelected =  Encoding.ASCII.GetBytes("iVBORw0KGgoAAAANSUhEUgAAAEoAAABKCAYAAAAc0MJxAAAACXBIWXMAAAsSAAALEgHS3X78AAAClUlEQVR42u2cPZLCMAyFE00qGk7AoWioOQGcBU6QmoZDcYJtaLPFTnaybBJLtn49cckwsfn8niyL2O0wDM3W0q2z7Px0PpJn6dE/W4uxtlqKmoPS7w/k55y/XibgxEGNgHKgUMFJQhMBNVWPFKA1aBLAWEFJq8cSGBuo0/k4WAOSBFYMyouKUsBKYRWB8qgiKXVlg4oEiUNdZFARrCYBiwQqqoo4YKFBSUPa3e7/PntfL25goUBJQpoDpAkMCwss5Y+BRPleTuv3B9TmHKzURP3x1rAgUvCWhJWtKOu4pA0rpSpotoaCBbXnS+LW89wsAjtsaqpYURaxCrTVxGUb7VRBVVGWeVCpqqq03u52Z58UsKwOSPfD2SfUZDnJfqAmu3HDm8YpqEVNKquel1KKVD8c49gSzqig3tcLqfSrVSUFKztggHlKPIELikSSR1WX5OQBd+cSAXwJluaKCh5tt6auEZq2NcEbEEw/JZByx+w6PZjGPevEFTxD0trwioKqZWsSWlFrk2A1Qa5Ava8Xd0odX+IAT5A26yEhUfZt2mC7pvl5AVT7/zzvCpoF5cFiWrEpd4KyrOch4GrYb/o2XvjCHQVWCdhfUI/+2X4e8fKQN3EA4CjXdLVYZvrMETxnPyAtdys7lo7x823hP6A07BctLXBfPfCwbVkFZRnUq1RUVAvlqGkRFFZVVFiR4RbHKOyPjwBp7VzMIihKrMIkfN5Xz9ThoeTpKq2qAjU71waVtJ7WKqi1Z8uB5C6PwgCwgISynrYFl6woEefYT4BawbJa4YpBjbCaZjulXr261O49iArL7CaNSFY0v5vFu7pc3fbjUV2u74+aA6YNLcyNZBYqC33HXUplJWqr9tZEKjzMJt1irN8EWg4MOHJPqgAAAABJRU5ErkJggg=="),
                        }
                    };

                    foreach (var item in allergens)
                    {
                        await _mealmateContext.Allergens.AddAsync(
                            new Allergen
                            {
                                Name = item.Name,
                                Created = DateTime.Now,
                                IsActive = true,
                                Photo = item.Photo,
                                PhotoSelected = item.PhotoSelected
                            });
                    }
                }

                if (!_mealmateContext.Dietaries.Any())
                {
                    var dietaries= new List<Dietary>()
                    {
                        new Dietary
                        {
                            Name =  "Gluten free",
                            Photo =  Encoding.ASCII.GetBytes("iVBORw0KGgoAAAANSUhEUgAAAEsAAABKCAYAAADzEqlPAAAACXBIWXMAAAsSAAALEgHS3X78AAACHUlEQVR42u2cPXLDIBCFDaMqt1OjWidIzpKcIHWa3C4tqTyDNEJmgf1l6S2xn98+HrJMSCk9uMa2r+Cb/3z/Bq75BipYJTCQ4kdcQyysc3EYRVHcAxVWXgBl22DfdygsLkhU8xgCSwok7Hl1w3pOSBIkrDl2wdIAaqTKmmBJbTvsLxgMS5OaRtcQZwOVzx+6g4izgeoBFmcE1QoszgqqBVh8+BjjWdZVBVVXnB0UBJi3YW8bzqaqWnW5sgDAoquqfixaJ/738V404rfPr9Crrm1f07avKRdN1KiqO1BsOctBFWBpUBUlqCujV+tZIz3KbBuK3Rt65jq24kLpVyXPebZRqyfln8NsSTLPugPBucKJa0MtMNhhWQHFGh1yb6ndurzyPJOr4bk4qmJFw7LQgnl88JzlodRhOSyHZWjkW0GHJUVZpfykNVKwJXiNwNDbUEs6F2PwVoDFc6TnAKYFJqln9UKhhnp+guzRQVso5X5OVTsO78Fj/XDRGhM4YV2xENuGEk3/AItiVdQcNRbK4l+1oxRIJTu6/O/O7C+0ler36AAQyiUsKd4lCZQra1QonU1dNT59q6xZgNUuaN6GI/eG1tUFiUlVyrIKDJonq9vQGrCW4A3yLCvAWncofq4DNiyNe0i2E0O0ARNxFo30thR3ypFEaKLPz5ICTdXJbHeTxypA/Zl/NUW1FjbqOqJh1RQNDcfUI3Ae6qpt/AOj3+HmNDxEmAAAAABJRU5ErkJggg=="),
                            PhotoSelected =  Encoding.ASCII.GetBytes("iVBORw0KGgoAAAANSUhEUgAAAEoAAABKCAYAAAAc0MJxAAAACXBIWXMAAAsSAAALEgHS3X78AAACL0lEQVR42u3cO27DMAwGYJvw1KUn6KGyZPYJkrO0J8icJYfKCbpkdYfCheHKlkSLIilSYwwkyIefNPOQ+mmaOo51Hk/ZL3y/PfqOafW1oNYwt/eP7OcYv59scKRQSxwMTA4cNRoJ1AxEgcOFVhSKA2gLrTRWESgpQKGElQI7BCURiAoMDXUeT5NkoNLlCBaQ5tRjZjc0lEakElhgBekoFlhCOoIF1pCwWGARCYMFVpFyscAyUg4WdL7wPcpSmlJT5YlKxAJPE+FnPYupGrSm6e3zK/j463rxRMWQqpSeljTVQAqVH3iS0tagvflS9aRg6flIEC8/Hw+klV6ovyzLZn19vhbrS8vrlGUIXEjz45wNOguKuj+lQEjFWvYp4EiS9yjE7TyGudfHqo8HnDNPrTcqFqqlsvM5yqEcyqEcyqEcimxm0jwyVJ/MtWLB/fbo1zsCKFOlaS3/90neo7RjVW3mW1iv60UNZLUeVQKEExW67vfP6lR9qoX+JGKO4vyOSVTptfJ1y1+ipJSfhDSFtoOI+ggj+Q74b9MQ1a8yeyUoDSiUqMHTEkcKlp6PCoyTudbZKQplMVWxjY+eqKOlZylVKdtodxNlASt1r3G09FrGytmQndSjWsTK3bWe3MxbwsJs7c+667WAhT3/IHs80Ix15JAI1BylEevoSRp+NksNKOlgJY9Havb8qBIpIoPiBFN1ItkWGBWa+jPuYmhYuGZPTcyBSx1LuPoeG5S29QO6NqEV9Qmf/QAAAABJRU5ErkJggg=="),
                        },
                        new Dietary
                        {
                            Name =  "Halal",
                            Photo =  Encoding.ASCII.GetBytes("iVBORw0KGgoAAAANSUhEUgAAAEsAAABKCAYAAADzEqlPAAAACXBIWXMAAAsSAAALEgHS3X78AAAB4ElEQVR42u2cO3LDMAxELUwq306Nap3APot9Atdpcju3TJfhxPqQ+IkUFjMqTYGPuxBNaTCklC6Isvg66sbTPIpW6fv1M3jnPHgpawkOd8KaYzUD6/+krCaU38cSmgksr+S9F0cV1lGQvHJRgdUSpLXcNPISw9JMpnWVsWG1rCarhWXB6kFNFrlTJFB53pxNMUUCJQVG0UBJgFFEUFxgFBUUBxhdEMXAKLKq1J6G0UCVqAs2rABGUJXhDj6yugiqKg+3Fxbv++1jpa6P56AxDnesLXVN85imeUy5aAiqQs0yqV2AxVEWLAgbqloRsGq3Dkdb0GM70L0N3/dbWgO1BRE1qwNgqFk9wbo+nkN+taau/IlIR4OCsmBDBGABFmABFmABVszIDxkAC8oyhCX5dBDKQizG39fKeGGxXdyhLK4NUbdQs9QsCFgSZcGKsKGKBRdhQV1QllhVq7CiqmtvY76qLNgRNmSrahdWFHWV/i/eVRbsWGnDMwOrOW0prllnBFZ7LFVV4M8EjHN+V/00PAMw7kGnuAlGTyer0sYdovYqPQHTyFWtF03L0LQWVa0lVIvQtHNSbzbWAjSrHMza2HlD82iZZ94g0XISXj0F3WBtTa5mkpLfdgmrBsLRYJbiF5HGrV7FZLkOAAAAAElFTkSuQmCC"),
                            PhotoSelected =  Encoding.ASCII.GetBytes("iVBORw0KGgoAAAANSUhEUgAAAEoAAABKCAYAAAAc0MJxAAAACXBIWXMAAAsSAAALEgHS3X78AAAB70lEQVR42u3cMXKDQAwFUNBQuckJcig3rjlBchb7BNQ0HMonSJOWVGQwgbCrlVaC/du5wczzl9jxsKrHcayw9ldj8aW39sr+dfpuqC3uuc6RqCVM9/bOvlb79TSBU4WagFJgYuA00cSh5unRBPoPTQNMDCpHeizBRKBu7XX0AKQJlgTlKUV7YKlYbCivKdJKFwvqaEgS6aKSkKY2wdnwUklIKVhUGhIXi0pE4mBRqUixWFQykupTr9RUEdIUhkVAQumJpoqQJiRKNFVNjjRd7o+Xz9+fH2LXSr0eEiWcKkJvQqIAZVF+hLJDokRXY30DVo/7mPLru6EmS6A1pC28IksvBMIbFqGRO+9Ra31omaLL/eGmX5EXJGwPsI8CFBagAAUoQB0Nqu+GevnuNhYSBShAGa75+56ACly/bwXj7xYkCj0qV5peoLCfQqIAlavs/kCh/JCopDStQiFVSBQ7TZtQJaZq7xzfZqJQghGlVwpWyKnQ4ntU6NHZXagzpyrmfHFQos6IFXsIO7j0zoTFOake1aPOgMU9zs+ee1BV/idoSCGxoeZgR8FKHTuSPMTGO5bUIBuxaT/eSlF6NJLooC0PYFrDtlRGt1lMJdOcRqYGlQst13y7LFBbaLF4a/u3nKMma+uBpaGzUazmb07rB5iWd7JBPn/QAAAAAElFTkSuQmCC"),
                        },
                        new Dietary
                        {
                            Name =  "Vegetarian",
                            Photo =  Encoding.ASCII.GetBytes("iVBORw0KGgoAAAANSUhEUgAAAEsAAABKCAYAAADzEqlPAAAACXBIWXMAAAsSAAALEgHS3X78AAACNklEQVR42u2cMXIDIQxFgXGV223jek+QnCU5gWs3vp3bTbWZzcYBJCQhkJhxh9fo8b9WMB7FbdtC73Fdl+Ii7rdH7L3OKAkrByUHA/u94WCdA6UMjvPZorCOgUjsvsTvkcKS3mlpaGSw9kVqSMRc0JphSdut5/qaYGlTE/daUbC0q4kLGBjWSGqiXn+yBOq49ppTAxrWDKBagSVroFqApWB4QIEli6rCAkuWQUGBJeug2EoH6+pKrqp6YK6sFhtaz1U5dbmysMryN2BeXa4sjLJcVV5nkVrRYUGV5RasGxfqBz4/3l9Wv2+fX7F2PmRu6TutVryuy3Zdl+1+e0RyG0ICzQV/nleaWzNn+gQPBcAJTB2sozK5laICFkf+OD9//0iqK0m+CY9BYAM6A+LeGLU21GxBltLhGDhHwD0hegWvQVlURasZWBxW7AnVbTgarBEsGEIIqeUvOJwgMAUn9/nwMqIdepUPqnMWVpVsx53eQZeAYIBx5cAhbLgHX1IMNaTzufnnD7h+tVyG5XWWF6UOq6sFf8HiLk5dWYZU9QeWq8uVxQfLurpy9aYrqxLUv7A8dwFzljVgNcc9tyHgXJyF5XYEKmt2YJDbliobzgoMei1VnbNmA4a5vwMl+FmAYS86wW/D0YG13AijO4aMdg1N0biDpL2KdmhUG0vS5Uiryqg3k7wllAZoXGsh78zWExr3b4u0seMGJ7VBIt0kX5UZLUFRP08VrJpgsfWe5IgamrqOMr4BpSL4YM8ej2gAAAAASUVORK5CYII="),
                            PhotoSelected =  Encoding.ASCII.GetBytes("iVBORw0KGgoAAAANSUhEUgAAAEoAAABKCAYAAAAc0MJxAAAACXBIWXMAAAsSAAALEgHS3X78AAACS0lEQVR42u2cMW7DMAxFLcFTl56gh8qS2Sdoz9KeIHOWHKon6NLVnVy4rmyTNClR1hfQJXUC6+GRogGTYRzHrvS6Dpfdm7jfHqHkPYbcoFJQbs8vu98bvj6LwjMHtQRDgUJdc3jW0MxATYA0wZSEpg4qN6AtaJrA1EB5AJQCpgVLBdR1uIyeAFnYdQiUR4us7BKD8myRhV0iUDVCOmpXbA3SlCooTwNiUGeAJIUVW4QkgRVbhcSFFVuGxIEVW4dkduq1alWETTRYMOpI6LWcm9asglFSo3DSpa2CURKjYNO6VTAKBacRKITddvjBKOLqtX/w6f3j32ffb6/k6znXUr7jNkelbnptg2ufp67bunbv/2qgPOcnDgBLWK5y1NJGa0s4CT3m2LD2b8//clkVS4WQZEMpMDkSuavQ8xh2puXBfOPamy0JDwWnR1CTEZwi8/ShZxF+JYEi9GoBVUPYZQElPfYlxaTlqdjXFgK5S4TppTPXOUoSlqaPMPfbI6RaKKw3TAHBhWWV86oIvWnzW7ZYHwq95zBCHVXJmr89/AvKOk+h4AQohN38pf0/oBB+MEofFKxK98rAKAKkVVCwCjlKZNMmKFjFMKolWHvNjruh1wIsSkdo8zmK2jZLAoV8xTDqjLA4TdjsLvUzvBQraetvrp1fOiji0ICIrqtjisZRSIdA1WSXxnwWtSE2Xu3SmvijOj/KEzDtGVImE8lKArMYsmUGagnMGlqOWXfZpiZqQlsWvjmmJ4YSczi5k3ZSTwm57zl4GFhaw/oB3NSxSo71ss0AAAAASUVORK5CYII="),
                        },
                        new Dietary
                        {
                            Name =  "Vegan",
                            Photo =  Encoding.ASCII.GetBytes("iVBORw0KGgoAAAANSUhEUgAAAEsAAABKCAYAAADzEqlPAAAACXBIWXMAAAsSAAALEgHS3X78AAACDklEQVR42u2cPW6DIQyGg8WU23XpnBMkZ0lP0LlLb5f16xQJ0QQwYGMbI2UK4oOH9zUmPw7HcZy8tbXI/cDPy8e03fn5/g2ccw8cysoBzVgkxZjLYHEuhutZ02GlE+e2CfXzp8FaDYljPlNgPScmARIltCFY0tREvaHdsCSriWrOsAuodL69uR5aWVpBzQgfKFgWQI2sB3YFRRazrILCxjDYXVEYYODWawcGJ2/NwMADOnFSuqu6wFU1oKzdQZXU5Tb0mEWjLnALurJI1AWuKleWw1ptRYeFaHF2vHrcri+vCuf7V2jtn/etjdkyhkgbYqC8g9Da53G7Hi1jbBGzOEGohFWylklYFPEiHfv54j4RYZWtepSTA+ICJtKGki1ICot71z2Dl5iUSkgHNCiRVFnWrOg21AZLsgLTezNIBIG5X5oO8Jrvi2JjlkRrQn7/4V5473uuLOEwowZbrVJY/gmy51k9NqSOWxauTlEjjBUW/GdDjerijG+mYtYsUO++GowaFl+zI9dp+fLvKDv/SKS0dk8dRmOWhUA/W1VFZe0KbJvTkFJVVVi7qKv1QKsqyzowzMnfZEOPX8iYZREY2d9+rQHrSby76jpozvBHCnd4EQwOWKO7pA3UMCwNKhNTuEeyysSWhJIETUWxsdKkOcBxbJKZAokcGxK465TWEtp0wZi+HC1IKuq6Qi2Y9gevyuWGX8/3FQAAAABJRU5ErkJggg=="),
                            PhotoSelected =  Encoding.ASCII.GetBytes("iVBORw0KGgoAAAANSUhEUgAAAEoAAABKCAYAAAAc0MJxAAAACXBIWXMAAAsSAAALEgHS3X78AAACOklEQVR42u2cMXKEMAxFQUOVJifIobbZmhMkZ0lOQE3DofYEadKSIsNMhgUj25Is2XLJsF78+F/IYKtf17Xzdt0G6T+8j7fsOzNPSy993T23ovZgpte37D7H74c4ODZQGyAKMDHguKCRg5IEFIJGDYwMVGlAR8AoYWWD0gaIS11ZoO7jbdUIiENdyaCsQKJSVzQozVbjVFcUKGsqooQFrUHa3BA7Q4DWIKXCgq7hFgMLWlRTCixoGRJJjGoJEkZV4FrBwQK3XKL1WoYUUpVbL0VRbrlzVbmiPDOnVRW47VxRDqqE/cBt54oibaRrD14+v56O/Xy8o88/O/eqX2w/qfabp6UnVdTRBR4NMnQ85vfYPqq3HgaEFCzQEsj3apRUi3iM2gbMNciSMKGEhVIGeBT/qIK2mRil3XZsoCTvtD/1WgW12YkrOTQLqjb7ufUsgLKkOpinpd+v2y792I+ZM5rNzEvP/ZqLUdpsCaUGjAFRGtb/JYzqn3oh0JIgh677W1LM+bold0AabOh5FMJ25kHFvqMnSQ+47WctHajaelRqOlqwDw4J1562eGj8ahyyIDWgs+0fg6slMUZxTpKtpQSXMaplWJ5wZqgpCKolVWH27wUV5RZ060WpCQWqZlXFbJlFKapGWLH7itHWqwlWyuZr387PBUrznJALUhYoS7AoarRUXcQmV0WkoLQCo64j5YW2SoEqAcxk6bYzYFujAicBRxTUFbgQxFCCK11istdUsHQPsUS9zbP2CyiftyqZ94CoAAAAAElFTkSuQmCC"),
                        }
                    };

                    foreach (var item in dietaries)
                    {
                        await _mealmateContext.Dietaries.AddAsync(
                            new Dietary
                            {
                                Name = item.Name,
                                Created = DateTime.Now,
                                IsActive = true,
                                Photo = item.Photo,
                                PhotoSelected = item.PhotoSelected
                            });
                    }
                }

                if (!_mealmateContext.ContactRequestStates.Any())
                {
                    var contactRequests = new List<string>()
                    {
                        "New",
                        "Acknowledged",
                        "Cancelled"
                    };

                    foreach (var item in contactRequests)
                    {
                        await _mealmateContext.ContactRequestStates.AddAsync(
                            new ContactRequestState
                            {
                                Created = DateTime.Now,
                                IsActive = true,
                                Name = item
                            });
                    }
                }

                if (!_mealmateContext.RestroomRequestStates.Any())
                {
                    var restroomRequestStates = new List<string>()
                    {
                        "New",
                        "Acknowledged",
                        "Cancelled"
                    };

                    foreach (var item in restroomRequestStates)
                    {
                        await _mealmateContext.RestroomRequestStates.AddAsync(
                            new RestroomRequestState
                            {
                                Created = DateTime.Now,
                                IsActive = true,
                                Name = item
                            });
                    }
                }

                if (!_mealmateContext.CuisineTypes.Any())
                {
                    var cuisineTypes = new List<string>()
                    {
                        "Arabian",
                        "Chineese",
                        "Russian",
                        "Korean",
                        "German",
                        "Indian"
                    };

                    foreach (var item in cuisineTypes)
                    {
                        await _mealmateContext.CuisineTypes.AddAsync(
                            new CuisineType
                            {
                                Created = DateTime.Now,
                                IsActive = true,
                                Name = item
                            });
                    }
                }

                if (!_mealmateContext.OrderStates.Any())
                {
                    var orderStates = new List<string>()
                    {
                        "New",
                        "Acknowledged",
                        "Served",
                        "Bill Generated",
                        "Bill Paid",
                        "Cancelled",
                    };

                    foreach (var item in orderStates)
                    {
                        await _mealmateContext.OrderStates.AddAsync(
                            new OrderState
                            {
                                Created = DateTime.Now,
                                IsActive = true,
                                Name = item
                            });
                    }
                }

                await _mealmateContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new InvalidOperationException("Could not create lookups in Seeding");
            }

        }
        #endregion
    }
}

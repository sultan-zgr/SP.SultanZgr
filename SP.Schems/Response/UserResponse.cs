using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Schema;

public class UserResponse
{
    public int UserId { get; set; }
    public string Email { get; set; }
    public string FullName { get; set; }
    public string? TCNo { get; set; }
    public string? VehiclePlateNumber { get; set; }


}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

public enum Algorithms{
    [Display(Name = "eth_hr", GroupName = "eth_p")]
    Ethhash,
    [Display(Name = "gro_hr", GroupName = "gro_p")]
    Groestl,
    [Display(Name = "x11g_hr", GroupName = "x11g_p")]
    X11Gost,
    [Display(Name = "cn_hr", GroupName = "cn_p")]
    CryotoNight,
    [Display(Name = "eq__hr", GroupName = "eq_p")]
    Equilhash,
    [Display(Name = "lrev2_hr", GroupName = "lrev2_p")]
    Lyra2REv2,
    [Display(Name = "ns_hr", GroupName = "ns_p")]
    NeoScrypt,
    [Display(Name = "lbry_hr", GroupName = "lbry_p")]
    LBRY,
    [Display(Name = "bk14_hr", GroupName = "bk14_p")]
    Blake14r,
    [Display(Name = "pas_hr", GroupName = "pas_p")]
    Pascal,
    [Display(Name = "skh_hr", GroupName = "skh_p")]
    Skunkhash,
    [Display(Name = "n5_hr", GroupName = "n5_hp")]
    NIST5,
    [Display(Name = "l2z_hr", GroupName = "l2z_p")]
    Lyra2z
}

public class AlgorithmParams{
    public Algorithms Algo;
    public decimal HashRate;
    public decimal PowerConsumtion;
    public KeyValuePair<string, string> GetQueryParameter()
    {
        

        var type = Algo.GetType();
        if (Enum.IsDefined(type, Algo))
        {
            var name = Enum.GetName(type, Algo);
            var attr = type
                .GetField(name)
                .GetCustomAttributes(false)
                .OfType<DisplayAttribute>()
                .SingleOrDefault();

            if (attr != null && !string.IsNullOrWhiteSpace(attr.Name))
            {
                return new KeyValuePair<string, string>(attr.Name, attr.GroupName);
            }
        }
    throw new Exception();
    }
}
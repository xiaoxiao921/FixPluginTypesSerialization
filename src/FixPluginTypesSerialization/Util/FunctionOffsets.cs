using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FixPluginTypesSerialization.Util
{
    public static class FunctionOffsets
    {
        public static bool TryGet(Version unityVersion, out Dictionary<string, long> offsets)
        {
            offsets = IntPtr.Size == 4 ? Get32(unityVersion) : Get64(unityVersion);
            return offsets != null;
        }

        private static Dictionary<string, long> Get32(Version unityVersion)
        {
            return null;
        }

        private static Dictionary<string, long> Get64(Version unityVersion)
        {
            switch (unityVersion.Major)
            {
                case 5:
                    switch (unityVersion.Minor)
                    {
                        case 0:
                            switch (unityVersion.Build)
                            {
                                case 0:
                                    return CreateOffsets(0x172FE0, 0x00, 0x2A4F00, 0x17FE60, 0x2AA8F0, 0x2E20, 0x2540, 0x00);
                                case 1:
                                    return CreateOffsets(0x1749D0, 0x00, 0x2A5D80, 0x1817E0, 0x2AB740, 0x2E20, 0x2540, 0x00);
                                case 2:
                                    return CreateOffsets(0x174AF0, 0x00, 0x2A6C30, 0x1816D0, 0x2AC650, 0x2ED0, 0x25F0, 0x00);
                                case 3:
                                    return CreateOffsets(0x176070, 0x00, 0x2A8D70, 0x182CA0, 0x2AE610, 0x2ED0, 0x25F0, 0x00);
                                case 4:
                                    return CreateOffsets(0x1761C0, 0x00, 0x2A8790, 0x183070, 0x2AE220, 0x2E30, 0x2550, 0x00);
                            }
                            break;
                        case 1:
                            switch (unityVersion.Build)
                            {
                                case 0:
                                    return CreateOffsets(0x177DD0, 0x00, 0x2C2D20, 0x1848A0, 0x2C7710, 0x2E10, 0x2530, 0x00);
                                case 1:
                                    return CreateOffsets(0x1761C0, 0x00, 0x2C1760, 0x182ED0, 0x2C6350, 0x2E00, 0x2520, 0x00);
                                case 2:
                                    return CreateOffsets(0x177260, 0x00, 0x2C3060, 0x183D30, 0x2C7A00, 0x2EC0, 0x25E0, 0x00);
                                case 3:
                                    return CreateOffsets(0x176870, 0x00, 0x2C25E0, 0x1834B0, 0x2C70D0, 0x2E10, 0x2530, 0x00);
                                case 4:
                                    return CreateOffsets(0x176A60, 0x00, 0x2C3800, 0x183710, 0x2C8550, 0x2E00, 0x2520, 0x00);
                                case 5:
                                    return CreateOffsets(0x177220, 0x00, 0x2C2D60, 0x183F10, 0x2C79B0, 0x2E00, 0x2520, 0x00);
                            }
                            break;
                        case 2:
                            switch (unityVersion.Build)
                            {
                                case 0:
                                    return CreateOffsets(0x186E00, 0x00, 0x2C8370, 0x1939D0, 0x2CD7C0, 0x2EA0, 0x25C0, 0x00);
                                case 1:
                                    return CreateOffsets(0x187CE0, 0x00, 0x2C9540, 0x194990, 0x2CE7C0, 0x2E00, 0x2520, 0x00);
                                case 2:
                                    return CreateOffsets(0x188B00, 0x00, 0x2C94A0, 0x1954B0, 0x2CE970, 0x2EB0, 0x25D0, 0x00);
                                case 3:
                                    return CreateOffsets(0x189020, 0x00, 0x2C8820, 0x195B80, 0x2CDD20, 0x2E30, 0x2550, 0x00);
                                case 4:
                                    return CreateOffsets(0x1891E0, 0x00, 0x2C9B30, 0x195A90, 0x2CF030, 0x2DF0, 0x2510, 0x00);
                                case 5:
                                    return CreateOffsets(0x1875E0, 0x00, 0x2C90A0, 0x194320, 0x2CE5A0, 0x2DF0, 0x2510, 0x00);
                            }
                            break;
                        case 3:
                            switch (unityVersion.Build)
                            {
                                case 0:
                                    return CreateOffsets(0x1F6720, 0x00, 0x300C80, 0x202750, 0x3067A0, 0x11FFA0, 0x11F380, 0x00);
                                case 1:
                                    return CreateOffsets(0x1F5780, 0x00, 0x300D90, 0x2015A0, 0x306BD0, 0x11DFC0, 0x11D3A0, 0x00);
                                case 2:
                                    return CreateOffsets(0x1F5860, 0x00, 0x300860, 0x201700, 0x3066D0, 0x11F420, 0x11E800, 0x00);
                                case 3:
                                    return CreateOffsets(0x1F5DB0, 0x00, 0x301A30, 0x201BE0, 0x3075B0, 0x11ECE0, 0x11E0C0, 0x00);
                                case 4:
                                    return CreateOffsets(0x1E7560, 0x00, 0x2F27C0, 0x1F3760, 0x2F8730, 0x114A50, 0x113BD0, 0x00);
                                case 5:
                                    return CreateOffsets(0x1E91A0, 0x00, 0x2F7D10, 0x1F51F0, 0x2FDA60, 0x115CB0, 0x114BC0, 0x00);
                                case 6:
                                    return CreateOffsets(0x1ECB10, 0x00, 0x2F8EE0, 0x1F8BD0, 0x2FEC00, 0x118320, 0x117230, 0x00);
                                case 7:
                                    return CreateOffsets(0x1EE7D0, 0x00, 0x2FC4E0, 0x1FA720, 0x302270, 0x119760, 0x118670, 0x00);
                                case 8:
                                    return CreateOffsets(0x1EEAB0, 0x00, 0x2FC7A0, 0x1FA9B0, 0x302520, 0x11ABC0, 0x119AD0, 0x00);
                            }
                            break;
                        case 4:
                            switch (unityVersion.Build)
                            {
                                case 0:
                                    return CreateOffsets(0x20D2D0, 0x00, 0x32C2F0, 0x2175A0, 0x330610, 0x13F550, 0x13E630, 0x00);
                                case 1:
                                    return CreateOffsets(0x2106E0, 0x00, 0x330E20, 0x21AAD0, 0x335090, 0x142EC0, 0x141FA0, 0x00);
                                case 2:
                                    return CreateOffsets(0x211470, 0x00, 0x330A50, 0x21BB30, 0x334D20, 0x143040, 0x142120, 0x00);
                                case 3:
                                    return CreateOffsets(0x20EB70, 0x00, 0x330720, 0x219000, 0x334CB0, 0x140E80, 0x13FF60, 0x00);
                                case 4:
                                    return CreateOffsets(0x211150, 0x00, 0x3311C0, 0x21B7A0, 0x335240, 0x143890, 0x142970, 0x00);
                                case 5:
                                    return CreateOffsets(0x2107D0, 0x00, 0x332750, 0x21ABC0, 0x336890, 0x1426B0, 0x141790, 0x00);
                                case 6:
                                    return CreateOffsets(0x211330, 0x00, 0x3332B0, 0x21B640, 0x337710, 0x143410, 0x1424F0, 0x00);
                            }
                            break;
                        case 5:
                            switch (unityVersion.Build)
                            {
                                case 0:
                                    return CreateOffsets(0x30A3A0, 0x00, 0x28AEE0, 0x316450, 0x28DFF0, 0x13BE0, 0x12F90, 0x00);
                                case 1:
                                    return CreateOffsets(0x30B970, 0x00, 0x28C0E0, 0x3177C0, 0x28F1F0, 0x13E30, 0x131D0, 0x00);
                                case 2:
                                    return CreateOffsets(0x30EA50, 0x00, 0x28F350, 0x31A940, 0x292460, 0x13D80, 0x13120, 0x00);
                                case 3:
                                    return CreateOffsets(0x30E190, 0x00, 0x28F680, 0x319F50, 0x292280, 0x13CC0, 0x13060, 0x00);
                                case 4:
                                    return CreateOffsets(0x30E510, 0x00, 0x28EF80, 0x31A7F0, 0x2920D0, 0x13DC0, 0x13160, 0x00);
                                case 5:
                                    return CreateOffsets(0x30E9B0, 0x00, 0x28E2F0, 0x31AC20, 0x291300, 0x13C00, 0x12FA0, 0x00);
                                case 6:
                                    return CreateOffsets(0x310370, 0x00, 0x290990, 0x31C580, 0x293740, 0x13D10, 0x130B0, 0x00);
                            }
                            break;
                        case 6:
                            switch (unityVersion.Build)
                            {
                                case 0:
                                    return CreateOffsets(0x835840, 0x00, 0x70E3E0, 0x62D710, 0x70E260, 0x8154A0, 0x815550, 0x00);
                                case 1:
                                    return CreateOffsets(0x836D30, 0x00, 0x70F250, 0x62D9D0, 0x70F0D0, 0x816A00, 0x816AB0, 0x00);
                                case 2:
                                    return CreateOffsets(0x83BF50, 0x00, 0x711E10, 0x630B80, 0x711C90, 0x81BCE0, 0x81BD90, 0x00);
                                case 3:
                                    return CreateOffsets(0x83DC30, 0x00, 0x713E60, 0x633490, 0x713CE0, 0x81D630, 0x81D6E0, 0x00);
                                case 4:
                                    return CreateOffsets(0x83ED50, 0x00, 0x714C00, 0x633760, 0x714A80, 0x81EDC0, 0x81EE70, 0x00);
                                case 5:
                                    return CreateOffsets(0x83FD60, 0x00, 0x715020, 0x633970, 0x714EA0, 0x81F5E0, 0x81F690, 0x00);
                                case 6:
                                    return CreateOffsets(0x83E670, 0x00, 0x713B20, 0x632420, 0x7139A0, 0x81E060, 0x81E110, 0x00);
                                case 7:
                                    return CreateOffsets(0x83E670, 0x00, 0x713B20, 0x632420, 0x7139A0, 0x81E060, 0x81E110, 0x00);
                            }
                            break;
                    }
                    break;
                case 2017:
                    switch (unityVersion.Minor)
                    {
                        case 1:
                            switch (unityVersion.Build)
                            {
                                case 0:
                                    return CreateOffsets(0x56D230, 0x00, 0x7F5240, 0x6EED80, 0x7F8630, 0x8E11C0, 0x8E0350, 0x00);
                                case 1:
                                    return CreateOffsets(0x56E0E0, 0x00, 0x7F6BE0, 0x6F00A0, 0x7FA240, 0x8E2F70, 0x8E2100, 0x00);
                                case 2:
                                    return CreateOffsets(0x56F330, 0x00, 0x7F6CB0, 0x6F0B40, 0x7FA310, 0x8E3FA0, 0x8E3130, 0x00);
                                case 3:
                                    return CreateOffsets(0x570920, 0x00, 0x7F9DE0, 0x6F2BC0, 0x7FD410, 0x8E7CC0, 0x8E6E50, 0x00);
                                case 4:
                                    return CreateOffsets(0x572ED0, 0x00, 0x7FBF70, 0x6F5500, 0x7FF550, 0x8E9A40, 0x8E8BD0, 0x00);
                                case 5:
                                    return CreateOffsets(0x572190, 0x00, 0x7FC1A0, 0x6F4690, 0x7FF800, 0x8E9850, 0x8E89E0, 0x00);
                            }
                            break;
                        case 2:
                            switch (unityVersion.Build)
                            {
                                case 0:
                                    return CreateOffsets(0x450E90, 0x00, 0x3CB310, 0x462980, 0x3CF180, 0x11BF40, 0x11B0D0, 0x00);
                                case 1:
                                    return CreateOffsets(0x4513E0, 0x00, 0x3CC000, 0x463060, 0x3CFF70, 0x11B0B0, 0x11A240, 0x00);
                                case 2:
                                    return CreateOffsets(0x4515A0, 0x00, 0x3CBA50, 0x463380, 0x3CF8C0, 0x11B740, 0x11A8D0, 0x00);
                                case 3:
                                    return CreateOffsets(0x453400, 0x00, 0x3CD740, 0x4651E0, 0x3D1680, 0x11B450, 0x11A5E0, 0x00);
                                case 4:
                                    return CreateOffsets(0x452680, 0x00, 0x3CBE00, 0x464670, 0x3CFE50, 0x11BAE0, 0x11AC70, 0x00);
                                case 5:
                                    return CreateOffsets(0x452570, 0x00, 0x3CDF30, 0x4641E0, 0x3D1E20, 0x11AFE0, 0x11A170, 0x00);
                            }
                            break;
                        case 3:
                            switch (unityVersion.Build)
                            {
                                case 0:
                                    return CreateOffsets(0x5D0CD0, 0x5D4CB0, 0x00, 0x5E9FB0, 0x54EF30, 0x2D5200, 0x2D5170, 0x00);
                                case 1:
                                    return CreateOffsets(0x5CF710, 0x5D35B0, 0x00, 0x5E88A0, 0x54DCF0, 0x2D4C20, 0x2D4B90, 0x00);
                            }
                            break;
                        case 4:
                            switch (unityVersion.Build)
                            {
                                case 0:
                                    return CreateOffsets(0x5D10D0, 0x5D4F60, 0x00, 0x5E9E50, 0x5501B0, 0x2D3590, 0x2D3500, 0x00);
                                case 1:
                                    return CreateOffsets(0x5CEDF0, 0x5D2CF0, 0x00, 0x5E7C20, 0x54E2D0, 0x2D45D0, 0x2D4540, 0x00);
                                case 2:
                                    return CreateOffsets(0x5CE9C0, 0x5D2860, 0x00, 0x5E78F0, 0x54D1B0, 0x2D42F0, 0x2D4260, 0x00);
                                case 3:
                                    return CreateOffsets(0x5D0D70, 0x5D4C70, 0x00, 0x5E9E00, 0x54FD40, 0x2D6A10, 0x2D6980, 0x00);
                                case 4:
                                    return CreateOffsets(0x5CFA80, 0x5D3980, 0x00, 0x5E8A10, 0x54DEA0, 0x2D4D70, 0x2D4CE0, 0x00);
                                case 5:
                                    return CreateOffsets(0x5D27D0, 0x5D6670, 0x00, 0x5EB7C0, 0x551470, 0x2D4F20, 0x2D4E90, 0x00);
                                case 6:
                                    return CreateOffsets(0x5CE640, 0x5D24F0, 0x00, 0x5E7460, 0x54C5B0, 0x2D52A0, 0x2D5210, 0x00);
                                case 7:
                                    return CreateOffsets(0x5D1C10, 0x5D5AB0, 0x00, 0x5EAA20, 0x550A60, 0x2D4690, 0x2D4600, 0x00);
                                case 8:
                                    return CreateOffsets(0x5D27F0, 0x5D66F0, 0x00, 0x5EB800, 0x551730, 0x2D7860, 0x2D77D0, 0x00);
                                case 9:
                                    return CreateOffsets(0x5D1230, 0x5D5130, 0x00, 0x5EA2E0, 0x54F370, 0x2D5020, 0x2D4F90, 0x00);
                                case 10:
                                    return CreateOffsets(0x5D3A40, 0x5D78F0, 0x00, 0x5ECA00, 0x550E10, 0x2D53C0, 0x2D5330, 0x00);
                                case 11:
                                    return CreateOffsets(0x5D3A90, 0x5D7930, 0x00, 0x5EC950, 0x551310, 0x2D4A80, 0x2D49F0, 0x00);
                                case 12:
                                    return CreateOffsets(0x5D5EF0, 0x5D9D80, 0x00, 0x5EEEE0, 0x5549E0, 0x2D6100, 0x2D6070, 0x00);
                                case 13:
                                    return CreateOffsets(0x5D4CF0, 0x5D8BF0, 0x00, 0x5EDD50, 0x552470, 0x2D78F0, 0x2D7860, 0x00);
                                case 14:
                                    return CreateOffsets(0x5D57C0, 0x5D96C0, 0x00, 0x5EE9E0, 0x5527D0, 0x2D8D40, 0x2D8CB0, 0x00);
                                case 15:
                                    return CreateOffsets(0x5D5A10, 0x5D98B0, 0x00, 0x5EEDE0, 0x552B00, 0x2D7D10, 0x2D7C80, 0x00);
                                case 16:
                                    return CreateOffsets(0x5D5BE0, 0x5D9A80, 0x00, 0x5EEE00, 0x554360, 0x2D5150, 0x2D50C0, 0x00);
                                case 17:
                                    return CreateOffsets(0x5D6690, 0x5DA530, 0x00, 0x5EF8A0, 0x554C20, 0x2D5E30, 0x2D5DA0, 0x00);
                                case 18:
                                    return CreateOffsets(0x5D6E10, 0x5DAD10, 0x00, 0x5F00A0, 0x5552A0, 0x2D5560, 0x2D54D0, 0x00);
                                case 19:
                                    return CreateOffsets(0x5D66F0, 0x5DA580, 0x00, 0x5EF770, 0x553B10, 0x2D61F0, 0x2D6160, 0x00);
                                case 20:
                                    return CreateOffsets(0x5D5390, 0x5D9230, 0x00, 0x5EE820, 0x5527D0, 0x2D4DE0, 0x2D4D50, 0x00);
                                case 21:
                                    return CreateOffsets(0x5D6C90, 0x5DAB40, 0x00, 0x5EFF30, 0x554BB0, 0x2D7BB0, 0x2D7B20, 0x00);
                                case 22:
                                    return CreateOffsets(0x5D6C90, 0x5DAB40, 0x00, 0x5EFF30, 0x554BB0, 0x2D7BB0, 0x2D7B20, 0x00);
                                case 23:
                                    return CreateOffsets(0x5D6FA0, 0x5DAE40, 0x00, 0x5F0310, 0x555120, 0x2D4F20, 0x2D4E90, 0x00);
                                case 24:
                                    return CreateOffsets(0x5D55B0, 0x5D9440, 0x00, 0x5EE8D0, 0x553BE0, 0x2D83D0, 0x2D8340, 0x00);
                                case 25:
                                    return CreateOffsets(0x5D5370, 0x5D92D0, 0x00, 0x5EE760, 0x552DE0, 0x2D8340, 0x2D82B0, 0x00);
                                case 26:
                                    return CreateOffsets(0x5D63A0, 0x5DA300, 0x00, 0x5EF7B0, 0x553F20, 0x2D51F0, 0x2D5160, 0x00);
                                case 27:
                                    return CreateOffsets(0x5D7450, 0x5DB3B0, 0x00, 0x5F0850, 0x555470, 0x2D79A0, 0x2D7910, 0x00);
                                case 28:
                                    return CreateOffsets(0x5D3180, 0x5D7150, 0x00, 0x5EC650, 0x550540, 0x2D6130, 0x2D60A0, 0x00);
                                case 29:
                                    return CreateOffsets(0x5D3180, 0x5D7150, 0x00, 0x5EC650, 0x550540, 0x2D6130, 0x2D60A0, 0x00);
                                case 30:
                                    return CreateOffsets(0x5D7710, 0x5DB6F0, 0x00, 0x5F0BA0, 0x553EE0, 0x2D8400, 0x2D8370, 0x00);
                                case 31:
                                    return CreateOffsets(0x5D6500, 0x5DA470, 0x00, 0x5EF870, 0x554310, 0x2D6940, 0x2D68B0, 0x00);
                                case 32:
                                    return CreateOffsets(0x5D3690, 0x5D7600, 0x00, 0x5ECA70, 0x5509C0, 0x2D49D0, 0x2D4940, 0x00);
                                case 33:
                                    return CreateOffsets(0x5D6B20, 0x5DAB00, 0x00, 0x5EFF20, 0x553A40, 0x2D89E0, 0x2D8950, 0x00);
                                case 34:
                                    return CreateOffsets(0x5D6C10, 0x5DABE0, 0x00, 0x5F0100, 0x553F40, 0x2D8710, 0x2D8680, 0x00);
                                case 35:
                                    return CreateOffsets(0x5D6D00, 0x5DAC60, 0x00, 0x5F0020, 0x554FB0, 0x2D59C0, 0x2D5930, 0x00);
                                case 36:
                                    return CreateOffsets(0x5D7AC0, 0x5DBA20, 0x00, 0x5F0E90, 0x5556F0, 0x2D9360, 0x2D92D0, 0x00);
                                case 37:
                                    return CreateOffsets(0x5D6890, 0x5DA800, 0x00, 0x5EFD50, 0x5539D0, 0x2D5640, 0x2D55B0, 0x00);
                                case 38:
                                    return CreateOffsets(0x5D67D0, 0x5DA740, 0x00, 0x5EFCA0, 0x554060, 0x2D7480, 0x2D73F0, 0x00);
                                case 39:
                                    return CreateOffsets(0x5D6CF0, 0x5DAC60, 0x00, 0x5F0170, 0x5552A0, 0x2D8DE0, 0x2D8D50, 0x00);
                                case 40:
                                    return CreateOffsets(0x5D59F0, 0x5D9950, 0x00, 0x5EEF30, 0x552F10, 0x2D8410, 0x2D8380, 0x00);
                            }
                            break;
                    }
                    break;
                case 2018:
                    switch (unityVersion.Minor)
                    {
                        case 1:
                            switch (unityVersion.Build)
                            {
                                case 0:
                                    return CreateOffsets(0x5920D0, 0x596D20, 0x00, 0x5A6200, 0x516B50, 0x2B78F0, 0x2B78C0, 0x00);
                                case 1:
                                    return CreateOffsets(0x590710, 0x595360, 0x00, 0x5A47B0, 0x515790, 0x2B7530, 0x2B4B80, 0x00);
                                case 2:
                                    return CreateOffsets(0x5919A0, 0x5965F0, 0x00, 0x5A5BB0, 0x516A90, 0x2B7FE0, 0x2B56C0, 0x00);
                                case 3:
                                    return CreateOffsets(0x590B50, 0x5957A0, 0x00, 0x5A4E30, 0x515B00, 0x2B74B0, 0x2B7480, 0x00);
                                case 4:
                                    return CreateOffsets(0x592080, 0x596CD0, 0x00, 0x5A60C0, 0x516A60, 0x2B70F0, 0x2B70C0, 0x00);
                                case 5:
                                    return CreateOffsets(0x591C30, 0x596880, 0x00, 0x5A5F60, 0x5163B0, 0x2B75F0, 0x2B4C40, 0x00);
                                case 6:
                                    return CreateOffsets(0x591900, 0x596550, 0x00, 0x5A5A80, 0x516560, 0x2B7670, 0x2B4CF0, 0x00);
                                case 7:
                                    return CreateOffsets(0x591C10, 0x596860, 0x00, 0x5A5EB0, 0x516C60, 0x2B8960, 0x2B60A0, 0x00);
                                case 8:
                                    return CreateOffsets(0x592040, 0x596CA0, 0x00, 0x5A61D0, 0x5165D0, 0x2B6B40, 0x2B6B10, 0x00);
                                case 9:
                                    return CreateOffsets(0x5917F0, 0x596440, 0x00, 0x5A5900, 0x516DF0, 0x2B8240, 0x2B58F0, 0x00);
                            }
                            break;
                        case 2:
                            switch (unityVersion.Build)
                            {
                                case 0:
                                    return CreateOffsets(0x5D9BE0, 0x5DE030, 0x00, 0x5ED300, 0x54F550, 0x2DD820, 0x2DD7F0, 0x00);
                                case 1:
                                    return CreateOffsets(0x5DA2A0, 0x5DE710, 0x00, 0x5EDAB0, 0x550AA0, 0x2DD670, 0x2DD640, 0x00);
                                case 2:
                                    return CreateOffsets(0x5DA3D0, 0x5DE820, 0x00, 0x5EDC40, 0x5508A0, 0x2DD500, 0x2DD4D0, 0x00);
                                case 3:
                                    return CreateOffsets(0x5D9990, 0x5DDDF0, 0x00, 0x5ED190, 0x54FCB0, 0x2DDDA0, 0x2DB500, 0x00);
                                case 4:
                                    return CreateOffsets(0x5DA700, 0x5DEB60, 0x00, 0x5EDF60, 0x550AD0, 0x2DC980, 0x2DA140, 0x00);
                                case 5:
                                    return CreateOffsets(0x5DA060, 0x5DE4C0, 0x00, 0x5EDD30, 0x5506F0, 0x2DD050, 0x2DD020, 0x00);
                                case 6:
                                    return CreateOffsets(0x5DB2D0, 0x5DF730, 0x00, 0x5EEA30, 0x551670, 0x2DDDC0, 0x2DDD90, 0x00);
                                case 7:
                                    return CreateOffsets(0x5DB560, 0x5DF9B0, 0x00, 0x5EEF90, 0x550FA0, 0x2DE2C0, 0x2DE290, 0x00);
                                case 8:
                                    return CreateOffsets(0x5DB490, 0x5DF8E0, 0x00, 0x5EEC90, 0x550E30, 0x2DE0A0, 0x2DE070, 0x00);
                                case 9:
                                    return CreateOffsets(0x5DBC00, 0x5E0070, 0x00, 0x5EF550, 0x551C20, 0x2DE090, 0x2DB8B0, 0x00);
                                case 10:
                                    return CreateOffsets(0x5DC2D0, 0x5E0740, 0x00, 0x5EFC20, 0x552840, 0x2DE170, 0x2DB930, 0x00);
                                case 11:
                                    return CreateOffsets(0x5DB220, 0x5DF670, 0x00, 0x5EEB10, 0x550E90, 0x2DDA60, 0x2DDA30, 0x00);
                                case 12:
                                    return CreateOffsets(0x5DC3C0, 0x5E0810, 0x00, 0x5EFAE0, 0x551E30, 0x2DE430, 0x2DBB60, 0x00);
                                case 13:
                                    return CreateOffsets(0x5DC310, 0x5E0770, 0x00, 0x5EFA90, 0x552200, 0x2DE370, 0x2DBB30, 0x00);
                                case 14:
                                    return CreateOffsets(0x5DD150, 0x5E15A0, 0x00, 0x5F08D0, 0x552F30, 0x2DE820, 0x2DBFB0, 0x00);
                                case 15:
                                    return CreateOffsets(0x5DCB50, 0x5E0FC0, 0x00, 0x5F0310, 0x552E00, 0x2DE950, 0x2DE920, 0x00);
                                case 16:
                                    return CreateOffsets(0x5DCC20, 0x5E1090, 0x00, 0x5F0370, 0x553A30, 0x2DECC0, 0x2DEC90, 0x00);
                                case 17:
                                    return CreateOffsets(0x5DCC20, 0x5E1090, 0x00, 0x5F0370, 0x553A30, 0x2DECC0, 0x2DEC90, 0x00);
                                case 18:
                                    return CreateOffsets(0x5DB6B0, 0x5DFB10, 0x00, 0x5EEDF0, 0x551F10, 0x2DE220, 0x2DE1F0, 0x00);
                                case 19:
                                    return CreateOffsets(0x5DC590, 0x5E09F0, 0x00, 0x5EFD20, 0x552720, 0x2DE900, 0x2DE8D0, 0x00);
                                case 20:
                                    return CreateOffsets(0x5DCCE0, 0x5E1140, 0x00, 0x5F04F0, 0x5530D0, 0x2DE3D0, 0x2DBBC0, 0x00);
                                case 21:
                                    return CreateOffsets(0x5DCCE0, 0x5E1140, 0x00, 0x5F04F0, 0x5530D0, 0x2DE3D0, 0x2DBBC0, 0x00);
                            }
                            break;
                        case 3:
                            switch (unityVersion.Build)
                            {
                                case 0:
                                    return CreateOffsets(0x8DDEB0, 0x8E0AD0, 0x00, 0x8E60C0, 0x86E2C0, 0x6214D0, 0x6214A0, 0x00);
                                case 1:
                                    return CreateOffsets(0x8DE400, 0x8E1020, 0x00, 0x8E6610, 0x86E810, 0x621840, 0x621810, 0x00);
                                case 2:
                                    return CreateOffsets(0x8DE3C0, 0x8E0FE0, 0x00, 0x8E65D0, 0x86E7D0, 0x621830, 0x621800, 0x00);
                                case 3:
                                    return CreateOffsets(0x8DEB80, 0x8E17A0, 0x00, 0x8E6D90, 0x86EF90, 0x622080, 0x622050, 0x00);
                                case 4:
                                    return CreateOffsets(0x8DEF30, 0x8E1B50, 0x00, 0x8E7140, 0x86F340, 0x622420, 0x6223F0, 0x00);
                                case 5:
                                    return CreateOffsets(0x8DF4C0, 0x8E20E0, 0x00, 0x8E76D0, 0x86F830, 0x622550, 0x622520, 0x00);
                                case 6:
                                    return CreateOffsets(0x8E1180, 0x8E3D90, 0x00, 0x8E9380, 0x8714D0, 0x6243B0, 0x624380, 0x00);
                                case 7:
                                    return CreateOffsets(0x8E1CA0, 0x8E48B0, 0x00, 0x8E9EA0, 0x871FF0, 0x6248D0, 0x6248A0, 0x00);
                                case 8:
                                    return CreateOffsets(0x8E28A0, 0x8E54B0, 0x00, 0x8EAAA0, 0x86EEE0, 0x625320, 0x6252F0, 0x00);
                                case 9:
                                    return CreateOffsets(0x8E29A0, 0x8E55B0, 0x00, 0x8EABA0, 0x86EFE0, 0x625410, 0x6253E0, 0x00);
                                case 10:
                                    return CreateOffsets(0x8E2F30, 0x8E5B40, 0x00, 0x8EB130, 0x86F470, 0x625730, 0x625700, 0x00);
                                case 11:
                                    return CreateOffsets(0x8E2F70, 0x8E5B80, 0x00, 0x8EB170, 0x86F4B0, 0x6256F0, 0x6256C0, 0x00);
                                case 12:
                                    return CreateOffsets(0x8E2F10, 0x8E5B20, 0x00, 0x8EB110, 0x86F430, 0x625CC0, 0x625C90, 0x00);
                                case 13:
                                    return CreateOffsets(0x8E3320, 0x8E5F30, 0x00, 0x8EB520, 0x86F770, 0x626000, 0x625FD0, 0x00);
                                case 14:
                                    return CreateOffsets(0x8E2B20, 0x8E5730, 0x00, 0x8EAD20, 0x86EFA0, 0x626190, 0x626160, 0x00);
                            }
                            break;
                        case 4:
                            switch (unityVersion.Build)
                            {
                                case 1:
                                    return CreateOffsets(0x8E2F70, 0x8E5B80, 0x00, 0x8EB170, 0x86F3F0, 0x6265C0, 0x626590, 0x00);
                                case 2:
                                    return CreateOffsets(0x8E39A0, 0x8E65B0, 0x00, 0x8EBBA0, 0x86FE20, 0x626E70, 0x626E40, 0x00);
                                case 3:
                                    return CreateOffsets(0x8E3940, 0x8E6550, 0x00, 0x8EBB40, 0x86FDC0, 0x626D80, 0x626D50, 0x00);
                                case 4:
                                    return CreateOffsets(0x8E38C0, 0x8E64D0, 0x00, 0x8EBAC0, 0x86FD30, 0x626E00, 0x626DD0, 0x00);
                                case 5:
                                    return CreateOffsets(0x8E3A50, 0x8E6660, 0x00, 0x8EBC50, 0x86FEA0, 0x627160, 0x627130, 0x00);
                                case 6:
                                    return CreateOffsets(0x8E3DE0, 0x8E69F0, 0x00, 0x8EBFE0, 0x870230, 0x627170, 0x627140, 0x00);
                                case 7:
                                    return CreateOffsets(0x8E3DE0, 0x8E69F0, 0x00, 0x8EBFE0, 0x870230, 0x627170, 0x627140, 0x00);
                                case 8:
                                    return CreateOffsets(0x8E37F0, 0x8E6400, 0x00, 0x8EB9F0, 0x86FC10, 0x626A20, 0x6269F0, 0x00);
                                case 9:
                                    return CreateOffsets(0x8E4E10, 0x8E7A20, 0x00, 0x8ED010, 0x8711B0, 0x627A30, 0x627A00, 0x00);
                                case 10:
                                    return CreateOffsets(0x8E7630, 0x8EA240, 0x00, 0x8EF830, 0x8739B0, 0x629290, 0x629260, 0x00);
                                case 11:
                                    return CreateOffsets(0x8E77F0, 0x8EA400, 0x00, 0x8EF9F0, 0x873B70, 0x629480, 0x629450, 0x00);
                                case 12:
                                    return CreateOffsets(0x8E7950, 0x8EA560, 0x00, 0x8EFB50, 0x873CD0, 0x6295E0, 0x6295B0, 0x00);
                                case 13:
                                    return CreateOffsets(0x8E81A0, 0x8EADB0, 0x00, 0x8F03A0, 0x874440, 0x629460, 0x629430, 0x00);
                                case 14:
                                    return CreateOffsets(0x8E9020, 0x8EBC30, 0x00, 0x8F1130, 0x8752C0, 0x629E00, 0x629DD0, 0x00);
                                case 15:
                                    return CreateOffsets(0x8E9D10, 0x8EC920, 0x00, 0x8F1E20, 0x875FA0, 0x62A6C0, 0x62A690, 0x00);
                                case 16:
                                    return CreateOffsets(0x8EA3A0, 0x8ECFB0, 0x00, 0x8F24B0, 0x876620, 0x62AC20, 0x62ABF0, 0x00);
                                case 17:
                                    return CreateOffsets(0x8EA930, 0x8ED540, 0x00, 0x8F2A40, 0x876BB0, 0x62B120, 0x62B0F0, 0x00);
                                case 18:
                                    return CreateOffsets(0x8EB0E0, 0x8EDCF0, 0x00, 0x8F31F0, 0x876CA0, 0x62B220, 0x62B1F0, 0x00);
                                case 19:
                                    return CreateOffsets(0x8EBAC0, 0x8EE6D0, 0x00, 0x8F3BD0, 0x877680, 0x62B4E0, 0x62B4B0, 0x00);
                                case 20:
                                    return CreateOffsets(0x8EBD90, 0x8EE9A0, 0x00, 0x8F3EA0, 0x877910, 0x62B790, 0x62B760, 0x00);
                                case 21:
                                    return CreateOffsets(0x8EBD70, 0x8EE980, 0x00, 0x8F3E80, 0x8778D0, 0x62B770, 0x62B740, 0x00);
                                case 22:
                                    return CreateOffsets(0x8EC080, 0x8EEC90, 0x00, 0x8F4190, 0x877BE0, 0x62B920, 0x62B8F0, 0x00);
                                case 23:
                                    return CreateOffsets(0x8EA790, 0x8ED3A0, 0x00, 0x8F28A0, 0x8762F0, 0x62B6B0, 0x62B680, 0x00);
                                case 24:
                                    return CreateOffsets(0x8EBEE0, 0x8EEAF0, 0x00, 0x8F3FF0, 0x877A40, 0x62CAC0, 0x62CA90, 0x00);
                                case 25:
                                    return CreateOffsets(0x8ECC70, 0x8EF880, 0x00, 0x8F4D80, 0x8787D0, 0x62D730, 0x62D700, 0x00);
                                case 26:
                                    return CreateOffsets(0x8ED330, 0x8EFF40, 0x00, 0x8F5440, 0x878E80, 0x62DA80, 0x62DA50, 0x00);
                                case 27:
                                    return CreateOffsets(0x8EDD10, 0x8F0920, 0x00, 0x8F5E20, 0x879670, 0x62DB60, 0x62DB30, 0x00);
                                case 28:
                                    return CreateOffsets(0x8ED940, 0x8F0550, 0x00, 0x8F5A50, 0x8792A0, 0x62D770, 0x62D740, 0x00);
                                case 29:
                                    return CreateOffsets(0x8EE070, 0x8F0C80, 0x00, 0x8F6180, 0x8799D0, 0x62DBF0, 0x62DBC0, 0x00);
                                case 30:
                                    return CreateOffsets(0x8EE8E0, 0x8F14F0, 0x00, 0x8F6A20, 0x87A240, 0x62E010, 0x62DFE0, 0x00);
                                case 31:
                                    return CreateOffsets(0x8EE9D0, 0x8F15E0, 0x00, 0x8F6B10, 0x87A330, 0x62E020, 0x62DFF0, 0x00);
                                case 32:
                                    return CreateOffsets(0x8EEB20, 0x8F1730, 0x00, 0x8F6C60, 0x87A480, 0x62E080, 0x62E050, 0x00);
                                case 33:
                                    return CreateOffsets(0x8EEB30, 0x8F1740, 0x00, 0x8F6C70, 0x87A490, 0x62E080, 0x62E050, 0x00);
                                case 34:
                                    return CreateOffsets(0x8EF710, 0x8F2320, 0x00, 0x8F7850, 0x87B070, 0x62E080, 0x62E050, 0x00);
                                case 35:
                                    return CreateOffsets(0x8EF870, 0x8F2480, 0x00, 0x8F79B0, 0x87B1D0, 0x62E1E0, 0x62E1B0, 0x00);
                                case 36:
                                    return CreateOffsets(0x8EFAD0, 0x8F26E0, 0x00, 0x8F7C10, 0x87B360, 0x62E330, 0x62E300, 0x00);
                            }
                            break;
                    }
                    break;
                case 2019:
                    switch (unityVersion.Minor)
                    {
                        case 1:
                            switch (unityVersion.Build)
                            {
                                case 0:
                                    return CreateOffsets(0x8B8000, 0x8BDD70, 0x00, 0x8C6490, 0x825C70, 0x5C8E30, 0x5C8E00, 0x00);
                                case 1:
                                    return CreateOffsets(0x8B8630, 0x8BE3A0, 0x00, 0x8C6AC0, 0x8262A0, 0x5C9410, 0x5C93E0, 0x00);
                                case 2:
                                    return CreateOffsets(0x8B8CD0, 0x8BEA40, 0x00, 0x8C7160, 0x826940, 0x5C9710, 0x5C96E0, 0x00);
                                case 3:
                                    return CreateOffsets(0x8B9030, 0x8BEDA0, 0x00, 0x8C74C0, 0x822560, 0x5C9B00, 0x5C9AD0, 0x00);
                                case 4:
                                    return CreateOffsets(0x8B7050, 0x8BCDC0, 0x00, 0x8C54E0, 0x820580, 0x5C7820, 0x5C77F0, 0x00);
                                case 5:
                                    return CreateOffsets(0x8B7050, 0x8BCDC0, 0x00, 0x8C54E0, 0x820580, 0x5C7820, 0x5C77F0, 0x00);
                                case 6:
                                    return CreateOffsets(0x8B6E90, 0x8BCC00, 0x00, 0x8C5320, 0x820340, 0x5C7360, 0x5C7330, 0x00);
                                case 7:
                                    return CreateOffsets(0x8B7080, 0x8BCDF0, 0x00, 0x8C5510, 0x820530, 0x5C7480, 0x5C7450, 0x00);
                                case 8:
                                    return CreateOffsets(0x8B7030, 0x8BCDA0, 0x00, 0x8C54C0, 0x8204E0, 0x5C73A0, 0x5C7370, 0x00);
                                case 9:
                                    return CreateOffsets(0x8B7450, 0x8BD1C0, 0x00, 0x8C58E0, 0x820900, 0x5C7710, 0x5C76E0, 0x00);
                                case 10:
                                    return CreateOffsets(0x8B7200, 0x8BCF80, 0x00, 0x8C56A0, 0x8206B0, 0x5C77C0, 0x5C7790, 0x00);
                                case 11:
                                    return CreateOffsets(0x8B7220, 0x8BCFA0, 0x00, 0x8C56C0, 0x8206D0, 0x5C77C0, 0x5C7790, 0x00);
                                case 12:
                                    return CreateOffsets(0x8B7220, 0x8BCFA0, 0x00, 0x8C56C0, 0x8206D0, 0x5C77C0, 0x5C7790, 0x00);
                                case 13:
                                    return CreateOffsets(0x8B7400, 0x8BD180, 0x00, 0x8C58A0, 0x8208B0, 0x5C7700, 0x5C76D0, 0x00);
                                case 14:
                                    return CreateOffsets(0x8B7800, 0x8BD580, 0x00, 0x8C5D20, 0x820CB0, 0x5C7AF0, 0x5C7AC0, 0x00);
                                case 15:
                                    return CreateOffsets(0x8B7440, 0x8BD030, 0x00, 0x8C5720, 0x820CF0, 0x5C7AF0, 0x5C7AC0, 0x00);
                            }
                            break;
                        case 2:
                            switch (unityVersion.Build)
                            {
                                case 0:
                                    return CreateOffsets(0x86BCC0, 0x871A50, 0x00, 0x87A240, 0x7D0CE0, 0x578F20, 0x578EF0, 0x00);
                                case 1:
                                    return CreateOffsets(0x86BFA0, 0x871D30, 0x00, 0x87A520, 0x7D0FD0, 0x578F70, 0x578F40, 0x00);
                                case 2:
                                    return CreateOffsets(0x86C530, 0x8722C0, 0x00, 0x87AAB0, 0x7D1560, 0x578FD0, 0x578FA0, 0x00);
                                case 3:
                                    return CreateOffsets(0x86C9C0, 0x872750, 0x00, 0x87AF40, 0x7D19F0, 0x579440, 0x579410, 0x00);
                                case 4:
                                    return CreateOffsets(0x86D3C0, 0x873150, 0x00, 0x87B940, 0x7D23F0, 0x579C00, 0x579BD0, 0x00);
                                case 5:
                                    return CreateOffsets(0x86D440, 0x8731D0, 0x00, 0x87B9C0, 0x7D2430, 0x5799B0, 0x579980, 0x00);
                                case 6:
                                    return CreateOffsets(0x86D330, 0x8730C0, 0x00, 0x87B8B0, 0x7D2320, 0x5797D0, 0x5797A0, 0x00);
                                case 7:
                                    return CreateOffsets(0x86D820, 0x8735B0, 0x00, 0x87BCB0, 0x7D2810, 0x579780, 0x579750, 0x00);
                                case 8:
                                    return CreateOffsets(0x86D970, 0x873710, 0x00, 0x87BE10, 0x7D2910, 0x579870, 0x579840, 0x00);
                                case 9:
                                    return CreateOffsets(0x86DCE0, 0x873A80, 0x00, 0x87C180, 0x7D2C80, 0x579A20, 0x5799F0, 0x00);
                                case 10:
                                    return CreateOffsets(0x86DF60, 0x873D00, 0x00, 0x87C400, 0x7D2E70, 0x579C20, 0x579BF0, 0x00);
                                case 11:
                                    return CreateOffsets(0x86E180, 0x873F20, 0x00, 0x87C620, 0x7D3090, 0x579DE0, 0x579DB0, 0x00);
                                case 12:
                                    return CreateOffsets(0x86E210, 0x873FB0, 0x00, 0x87C6B0, 0x7D3120, 0x579E00, 0x579DD0, 0x00);
                                case 13:
                                    return CreateOffsets(0x86E630, 0x8743D0, 0x00, 0x87CAD0, 0x7D3540, 0x579E50, 0x579E20, 0x00);
                                case 14:
                                    return CreateOffsets(0x86EC10, 0x8749B0, 0x00, 0x87D0B0, 0x7D3B10, 0x57A070, 0x57A040, 0x00);
                                case 15:
                                    return CreateOffsets(0x86EDF0, 0x874B90, 0x00, 0x87D290, 0x7D3CF0, 0x57A250, 0x57A220, 0x00);
                                case 16:
                                    return CreateOffsets(0x86F0B0, 0x874E50, 0x00, 0x87D550, 0x7D3F90, 0x57A470, 0x57A440, 0x00);
                                case 17:
                                    return CreateOffsets(0x86F9B0, 0x875750, 0x00, 0x87DE50, 0x7D4890, 0x57AD00, 0x57ACD0, 0x00);
                                case 18:
                                    return CreateOffsets(0x86F750, 0x8754F0, 0x00, 0x87DBF0, 0x7D4630, 0x57A960, 0x57A930, 0x00);
                                case 19:
                                    return CreateOffsets(0x86E3F0, 0x874190, 0x00, 0x87C890, 0x7D32D0, 0x579600, 0x5795D0, 0x00);
                                case 20:
                                    return CreateOffsets(0x86E4D0, 0x874270, 0x00, 0x87C970, 0x7D3390, 0x5796C0, 0x579690, 0x00);
                                case 21:
                                    return CreateOffsets(0x86E4D0, 0x874270, 0x00, 0x87C970, 0x7D3390, 0x5796C0, 0x579690, 0x00);
                                case 23:
                                    return CreateOffsets(0x86E110, 0x873D20, 0x00, 0x87C370, 0x7D33D0, 0x5796C0, 0x579690, 0x00);
                            }
                            break;
                        case 3:
                            switch (unityVersion.Build)
                            {
                                case 0:
                                    return CreateOffsets(0x895E60, 0x898D30, 0x00, 0x89DF30, 0x7F1810, 0x578190, 0x578130, 0x00);
                                case 1:
                                    return CreateOffsets(0x896780, 0x899650, 0x00, 0x89E850, 0x7F2180, 0x578380, 0x578320, 0x00);
                                case 2:
                                    return CreateOffsets(0x896D70, 0x899C40, 0x00, 0x89EE40, 0x7F2770, 0x578E70, 0x578E10, 0x00);
                                case 3:
                                    return CreateOffsets(0x896E00, 0x899CD0, 0x00, 0x89EED0, 0x7F2800, 0x578E80, 0x578E20, 0x00);
                                case 4:
                                    return CreateOffsets(0x898460, 0x89B330, 0x00, 0x8A0530, 0x7F3E60, 0x5795F0, 0x579590, 0x00);
                                case 5:
                                    return CreateOffsets(0x898E60, 0x89BD30, 0x00, 0x8A0F30, 0x7F4860, 0x579A60, 0x579A00, 0x00);
                                case 6:
                                    return CreateOffsets(0x89A790, 0x89D660, 0x00, 0x8A28A0, 0x7F5FB0, 0x57AC10, 0x57ABB0, 0x00);
                                case 7:
                                    return CreateOffsets(0x89B410, 0x89E2E0, 0x00, 0x8A3520, 0x7F6C10, 0x57B500, 0x57B4A0, 0x00);
                                case 8:
                                    return CreateOffsets(0x89D6F0, 0x8A05C0, 0x00, 0x8A5800, 0x7F8EF0, 0x57B780, 0x57B720, 0x00);
                                case 9:
                                    return CreateOffsets(0x89D910, 0x8A07E0, 0x00, 0x8A5A20, 0x7F9110, 0x57B4C0, 0x57B460, 0x00);
                                case 10:
                                    return CreateOffsets(0x89DCB0, 0x8A0B80, 0x00, 0x8A5DC0, 0x7F9350, 0x57B5A0, 0x57B540, 0x00);
                                case 11:
                                    return CreateOffsets(0x89F730, 0x8A2600, 0x00, 0x8A7840, 0x7FAD10, 0x57CB40, 0x57CAE0, 0x00);
                                case 12:
                                    return CreateOffsets(0x89FA90, 0x8A2960, 0x00, 0x8A7BA0, 0x7FAEC0, 0x57CCE0, 0x57CC80, 0x00);
                                case 13:
                                    return CreateOffsets(0x89FE40, 0x8A2D10, 0x00, 0x8A7F50, 0x7FB250, 0x57D020, 0x57CFC0, 0x00);
                                case 14:
                                    return CreateOffsets(0x8A07A0, 0x8A3670, 0x00, 0x8A88B0, 0x7FBBA0, 0x57D8E0, 0x57D880, 0x00);
                                case 15:
                                    return CreateOffsets(0x8A3300, 0x8A61D0, 0x00, 0x8AB410, 0x7FE6C0, 0x57F8B0, 0x57F850, 0x00);
                                case 17:
                                    return CreateOffsets(0x8A3600, 0x8A6390, 0x00, 0x8AB560, 0x7FEDC0, 0x57FA20, 0x57F9C0, 0x00);
                            }
                            break;
                        case 4:
                            switch (unityVersion.Build)
                            {
                                case 0:
                                    return CreateOffsets(0x8A39C0, 0x8A6890, 0x00, 0x8ABAD0, 0x7FED80, 0x57FA20, 0x57F9C0, 0x00);
                                case 1:
                                    return CreateOffsets(0x8A3A00, 0x8A68B0, 0x00, 0x8ABB10, 0x7FEDC0, 0x57FA20, 0x57F9C0, 0x00);
                                case 2:
                                    return CreateOffsets(0x8A4160, 0x8A7010, 0x00, 0x8AC270, 0x7FF520, 0x57FEA0, 0x57FE40, 0x00);
                                case 3:
                                    return CreateOffsets(0x8A5BB0, 0x8A8A60, 0x00, 0x8ADCC0, 0x800F70, 0x580FE0, 0x580F80, 0x00);
                                case 4:
                                    return CreateOffsets(0x8A5C20, 0x8A8AD0, 0x00, 0x8ADD30, 0x800FE0, 0x580FE0, 0x580F80, 0x00);
                                case 5:
                                    return CreateOffsets(0x8A5F00, 0x8A8DB0, 0x00, 0x8AE010, 0x8012B0, 0x581140, 0x5810E0, 0x00);
                                case 6:
                                    return CreateOffsets(0x8A6460, 0x8A9310, 0x00, 0x8AE570, 0x801800, 0x581580, 0x581520, 0x00);
                                case 7:
                                    return CreateOffsets(0x8A71A0, 0x8AA050, 0x00, 0x8AF2B0, 0x802520, 0x582120, 0x5820C0, 0x00);
                                case 8:
                                    return CreateOffsets(0x8A7170, 0x8AA020, 0x00, 0x8AF280, 0x8024F0, 0x581F50, 0x581EF0, 0x00);
                                case 9:
                                    return CreateOffsets(0x8A9370, 0x8AC220, 0x00, 0x8B1480, 0x8044D0, 0x5825C0, 0x582560, 0x00);
                                case 10:
                                    return CreateOffsets(0x8AAA50, 0x8AD900, 0x00, 0x8B2B60, 0x805BB0, 0x583A50, 0x5839F0, 0x00);
                                case 11:
                                    return CreateOffsets(0x8AB070, 0x8ADF20, 0x00, 0x8B3180, 0x806240, 0x583B90, 0x583B30, 0x00);
                                case 12:
                                    return CreateOffsets(0x8AB7F0, 0x8AE6A0, 0x00, 0x8B3900, 0x806870, 0x584650, 0x5845F0, 0x00);
                                case 13:
                                    return CreateOffsets(0x8ABAD0, 0x8AE980, 0x00, 0x8B3BE0, 0x806B50, 0x584760, 0x584700, 0x00);
                                case 14:
                                    return CreateOffsets(0x8ABD00, 0x8AEBB0, 0x00, 0x8B3E10, 0x806D80, 0x5848B0, 0x584850, 0x00);
                                case 15:
                                    return CreateOffsets(0x8ABEF0, 0x8AEDA0, 0x00, 0x8B4000, 0x806ED0, 0x5849D0, 0x584970, 0x00);
                                case 16:
                                    return CreateOffsets(0x8AD3B0, 0x8B0260, 0x00, 0x8B54E0, 0x808300, 0x585630, 0x5855D0, 0x00);
                                case 17:
                                    return CreateOffsets(0x8AE1A0, 0x8B1050, 0x00, 0x8B62D0, 0x808E30, 0x586050, 0x585FF0, 0x00);
                                case 18:
                                    return CreateOffsets(0x8AF710, 0x8B25C0, 0x00, 0x8B7840, 0x80A3B0, 0x587070, 0x587010, 0x00);
                                case 19:
                                    return CreateOffsets(0x8B03C0, 0x8B3270, 0x00, 0x8B84F0, 0x80AFD0, 0x587400, 0x5873A0, 0x00);
                                case 20:
                                    return CreateOffsets(0x8B16A0, 0x8B4550, 0x00, 0x8B97D0, 0x80C050, 0x588380, 0x588320, 0x00);
                                case 21:
                                    return CreateOffsets(0x8B0490, 0x8B3380, 0x00, 0x8B8600, 0x80AE20, 0x587080, 0x587020, 0x00);
                                case 22:
                                    return CreateOffsets(0x8B18E0, 0x8B47D0, 0x00, 0x8B9A50, 0x80C240, 0x5879D0, 0x587970, 0x00);
                                case 23:
                                    return CreateOffsets(0x8B7260, 0x8BA150, 0x00, 0x8BEEF0, 0x811650, 0x58C6D0, 0x58C670, 0x00);
                                case 24:
                                    return CreateOffsets(0x8B8810, 0x8BB6E0, 0x00, 0x8C0430, 0x812AD0, 0x58E460, 0x58E400, 0x00);
                                case 25:
                                    return CreateOffsets(0x8B8860, 0x8BB730, 0x00, 0x8C0480, 0x812B60, 0x58E4B0, 0x58E450, 0x00);
                                case 26:
                                    return CreateOffsets(0x8B8F30, 0x8BBE00, 0x00, 0x8C0B50, 0x812F70, 0x58E4E0, 0x58E480, 0x00);
                                case 27:
                                    return CreateOffsets(0x8B98C0, 0x8BC790, 0x00, 0x8C1470, 0x813910, 0x58ECD0, 0x58EC70, 0x00);
                                case 28:
                                    return CreateOffsets(0x8B9AF0, 0x8BC9C0, 0x00, 0x8C16A0, 0x813B20, 0x58ED90, 0x58ED30, 0x00);
                                case 29:
                                    return CreateOffsets(0x8BAF40, 0x8BDE20, 0x00, 0x8C2B00, 0x814F70, 0x58F370, 0x58F310, 0x00);
                                case 30:
                                    return CreateOffsets(0x8BBE50, 0x8BED30, 0x00, 0x8C3A10, 0x815E80, 0x590110, 0x5900B0, 0x00);
                                case 31:
                                    return CreateOffsets(0x8BEC20, 0x8C1AF0, 0x00, 0x8C67D0, 0x818C50, 0x592100, 0x5920A0, 0x00);
                                case 32:
                                    return CreateOffsets(0x8BF180, 0x8C2050, 0x00, 0x8C6D30, 0x818D50, 0x5922C0, 0x592260, 0x00);
                                case 33:
                                    return CreateOffsets(0x8BFE40, 0x8C2D10, 0x00, 0x8C79F0, 0x819A10, 0x592620, 0x5925C0, 0x00);
                                case 34:
                                    return CreateOffsets(0x8BFE30, 0x8C2D00, 0x00, 0x8C79E0, 0x819A00, 0x5925F0, 0x592590, 0x00);
                                case 35:
                                    return CreateOffsets(0x8C0A80, 0x8C3950, 0x00, 0x8C8630, 0x81A650, 0x593100, 0x5930A0, 0x00);
                                case 36:
                                    return CreateOffsets(0x8C0F70, 0x8C3E40, 0x00, 0x8C8B20, 0x81AB30, 0x593590, 0x593530, 0x00);
                                case 37:
                                    return CreateOffsets(0x8C2850, 0x8C5720, 0x00, 0x8CA3F0, 0x81B200, 0x593610, 0x5935B0, 0x00);
                                case 38:
                                    return CreateOffsets(0x8C2B70, 0x8C5A40, 0x00, 0x8CA710, 0x81B500, 0x593940, 0x5938E0, 0x00);
                                case 39:
                                    return CreateOffsets(0x8C3C40, 0x8C6B10, 0x00, 0x8CB7E0, 0x81C5D0, 0x594A00, 0x5949A0, 0x00);
                                case 40:
                                    return CreateOffsets(0x8C4000, 0x8C6ED0, 0x00, 0x8CBBA0, 0x81C980, 0x594D40, 0x594CE0, 0x00);
                                case 41:
                                    return CreateOffsets(0x8C3C40, 0x8C69D0, 0x00, 0x8CB630, 0x81C9C0, 0x594D40, 0x594CE0, 0x00);
                            }
                            break;
                    }
                    break;
                case 2020:
                    switch (unityVersion.Minor)
                    {
                        case 1:
                            switch (unityVersion.Build)
                            {
                                case 0:
                                    return CreateOffsets(0x825A80, 0x828A60, 0x00, 0x82D3E0, 0x7793A0, 0x50BCB0, 0x50BC50, 0x00);
                                case 1:
                                    return CreateOffsets(0x828940, 0x82B920, 0x00, 0x8302A0, 0x77A010, 0x50C3D0, 0x50C370, 0x00);
                                case 2:
                                    return CreateOffsets(0x828A20, 0x82BA00, 0x00, 0x830380, 0x77A0F0, 0x50C4B0, 0x50C450, 0x00);
                                case 3:
                                    return CreateOffsets(0x828A80, 0x82BA60, 0x00, 0x8303E0, 0x77A090, 0x50C360, 0x50C300, 0x00);
                                case 4:
                                    return CreateOffsets(0x829640, 0x82C620, 0x00, 0x830FA0, 0x77AAB0, 0x50C9B0, 0x50C950, 0x00);
                                case 5:
                                    return CreateOffsets(0x829B90, 0x82CB50, 0x00, 0x8314F0, 0x77AEE0, 0x50C900, 0x50C8A0, 0x00);
                                case 6:
                                    return CreateOffsets(0x829DF0, 0x82CDB0, 0x00, 0x831750, 0x77B140, 0x50CB40, 0x50CAE0, 0x00);
                                case 7:
                                    return CreateOffsets(0x82AE10, 0x82DDD0, 0x00, 0x832770, 0x77BFD0, 0x50D3E0, 0x50D380, 0x00);
                                case 8:
                                    return CreateOffsets(0x82AD60, 0x82DD20, 0x00, 0x8326C0, 0x77BF20, 0x50D350, 0x50D2F0, 0x00);
                                case 9:
                                    return CreateOffsets(0x82AF10, 0x82DED0, 0x00, 0x832870, 0x77C0D0, 0x50D250, 0x50D1F0, 0x00);
                                case 10:
                                    return CreateOffsets(0x82AFD0, 0x82DF90, 0x00, 0x832930, 0x77C190, 0x50D360, 0x50D300, 0x00);
                                case 11:
                                    return CreateOffsets(0x82B630, 0x82E5F0, 0x00, 0x832F90, 0x77C7F0, 0x50DA30, 0x50D9D0, 0x00);
                                case 12:
                                    return CreateOffsets(0x82B790, 0x82E750, 0x00, 0x8330F0, 0x77C950, 0x50DAB0, 0x50DA50, 0x00);
                                case 13:
                                    return CreateOffsets(0x82CD10, 0x82FCD0, 0x00, 0x834670, 0x77DEF0, 0x50F020, 0x50EFC0, 0x00);
                                case 14:
                                    return CreateOffsets(0x82E160, 0x831130, 0x00, 0x835A70, 0x77F320, 0x50FC30, 0x50FBD0, 0x00);
                                case 15:
                                    return CreateOffsets(0x82ED00, 0x831CD0, 0x00, 0x836630, 0x77FE20, 0x510460, 0x510400, 0x00);
                                case 16:
                                    return CreateOffsets(0x8302D0, 0x8332A0, 0x00, 0x837C00, 0x781270, 0x511170, 0x511110, 0x00);
                                case 17:
                                    return CreateOffsets(0x830440, 0x833410, 0x00, 0x837D70, 0x7813E0, 0x511230, 0x5111D0, 0x00);
                                case 18:
                                    return CreateOffsets(0x830480, 0x833260, 0x00, 0x837B80, 0x781420, 0x511230, 0x5111D0, 0x00);
                            }
                            break;
                        case 2:
                            switch (unityVersion.Build)
                            {
                                case 0:
                                    return CreateOffsets(0x8AA840, 0x8AE060, 0x00, 0x8B32D0, 0x7F7E40, 0x56ACD0, 0x56AC70, 0x196E680);
                                case 1:
                                    return CreateOffsets(0x8AA840, 0x8AE060, 0x00, 0x8B32D0, 0x7F7E40, 0x56ACD0, 0x56AC70, 0x196E680);
                                case 2:
                                    return CreateOffsets(0x8AB7A0, 0x8AEFC0, 0x00, 0x8B4230, 0x7F8B60, 0x56B6A0, 0x56B640, 0x196F6C0);
                                case 3:
                                    return CreateOffsets(0x8ADD60, 0x8B1580, 0x00, 0x8B67F0, 0x7FB040, 0x56C210, 0x56C1B0, 0x1973700);
                                case 4:
                                    return CreateOffsets(0x8AE210, 0x8B1A30, 0x00, 0x8B6CC0, 0x7FB4F0, 0x56C640, 0x56C5E0, 0x1975740);
                                case 5:
                                    return CreateOffsets(0x8AE9D0, 0x8B21F0, 0x00, 0x8B7480, 0x7FBCB0, 0x56CAD0, 0x56CA70, 0x19767C0);
                                case 6:
                                    return CreateOffsets(0x8ADAA0, 0x8B12C0, 0x00, 0x8B6550, 0x7FAD80, 0x56B870, 0x56B810, 0x1977780);
                                case 7:
                                    return CreateOffsets(0x8AE1B0, 0x8B19D0, 0x00, 0x8B6C60, 0x7FB490, 0x56BE10, 0x56BDB0, 0x1978880);
                                case 8:
                                    return CreateOffsets(0x8AE1F0, 0x8B17D0, 0x00, 0x8B6A10, 0x7FB4D0, 0x56BE10, 0x56BDB0, 0x1978880);
                            }
                            break;
                        case 3:
                            switch (unityVersion.Build)
                            {
                                case 0:
                                    return CreateOffsets(0x8AE5D0, 0x8B1DF0, 0x00, 0x8B7080, 0x7FB8B0, 0x56BE60, 0x56BE00, 0x1978880);
                                case 1:
                                    return CreateOffsets(0x8B6710, 0x8B9F30, 0x00, 0x8BF1C0, 0x8037E0, 0x574130, 0x5740D0, 0x1983880);
                                case 2:
                                    return CreateOffsets(0x8B6C80, 0x8BA4A0, 0x00, 0x8BF730, 0x803CB0, 0x574110, 0x5740B0, 0x1984880);
                                case 3:
                                    return CreateOffsets(0x8B7430, 0x8BAC50, 0x00, 0x8BFEE0, 0x804420, 0x5745D0, 0x574570, 0x1984880);
                                case 4:
                                    return CreateOffsets(0x8B7570, 0x8BAD90, 0x00, 0x8C0020, 0x804560, 0x5745E0, 0x574580, 0x1984880);
                                case 5:
                                    return CreateOffsets(0x8B9460, 0x8BCC80, 0x00, 0x8C1F10, 0x805FD0, 0x5757D0, 0x575770, 0x19888C0);
                                case 6:
                                    return CreateOffsets(0x8B9010, 0x8BC830, 0x00, 0x8C1AC0, 0x805B80, 0x575800, 0x5757A0, 0x19878C0);
                                case 7:
                                    return CreateOffsets(0x8B9370, 0x8BCB90, 0x00, 0x8C1E20, 0x805D70, 0x5759A0, 0x575940, 0x19898C0);
                                case 8:
                                    return CreateOffsets(0x8B9630, 0x8BCE50, 0x00, 0x8C20E0, 0x805C50, 0x5757B0, 0x575750, 0x193F8C0);
                                case 9:
                                    return CreateOffsets(0x8B9720, 0x8BCF40, 0x00, 0x8C21D0, 0x805CF0, 0x575870, 0x575810, 0x194F300);
                                case 10:
                                    return CreateOffsets(0x8BB8A0, 0x8BF0C0, 0x00, 0x8C4350, 0x807D20, 0x575B00, 0x575AA0, 0x1951340);
                                case 11:
                                    return CreateOffsets(0x8BC8A0, 0x8C00C0, 0x00, 0x8C5350, 0x808D40, 0x576970, 0x576910, 0x19523C0);
                                case 12:
                                    return CreateOffsets(0x8BCC50, 0x8C0470, 0x00, 0x8C5700, 0x8090E0, 0x576C30, 0x576BD0, 0x19503C0);
                                case 13:
                                    return CreateOffsets(0x8BD2C0, 0x8C0AE0, 0x00, 0x8C5D70, 0x8096D0, 0x576F60, 0x576F00, 0x19513C0);
                                case 14:
                                    return CreateOffsets(0x8BD960, 0x8C11D0, 0x00, 0x8C6460, 0x809D70, 0x5775F0, 0x577590, 0x19553C0);
                                case 15:
                                    return CreateOffsets(0x8BD940, 0x8C11B0, 0x00, 0x8C6440, 0x809D60, 0x5775F0, 0x577590, 0x19553C0);
                                case 16:
                                    return CreateOffsets(0x8BE680, 0x8C1EE0, 0x00, 0x8C70C0, 0x80AA50, 0x577F90, 0x577F30, 0x19573C0);
                                case 17:
                                    return CreateOffsets(0x8BF900, 0x8C3160, 0x00, 0x8C8340, 0x80BCC0, 0x579060, 0x579000, 0x195A4C0);
                                case 18:
                                    return CreateOffsets(0x8C3E90, 0x8C76F0, 0x00, 0x8CC8D0, 0x810160, 0x57D290, 0x57D230, 0x195E4C0);
                                case 19:
                                    return CreateOffsets(0x8C3F20, 0x8C7780, 0x00, 0x8CC960, 0x810310, 0x57D0A0, 0x57D040, 0x195E480);
                                case 20:
                                    return CreateOffsets(0x8C4100, 0x8C7960, 0x00, 0x8CCB40, 0x810480, 0x57D150, 0x57D0F0, 0x195E480);
                                case 21:
                                    return CreateOffsets(0x8C4550, 0x8C7DB0, 0x00, 0x8CCF90, 0x810530, 0x57D120, 0x57D0C0, 0x1964480);
                                case 22:
                                    return CreateOffsets(0x8C5320, 0x8C8B80, 0x00, 0x8CDD60, 0x8112D0, 0x57D670, 0x57D610, 0x1965480);
                                case 23:
                                    return CreateOffsets(0x8C4950, 0x8C81B0, 0x00, 0x8CD390, 0x810F10, 0x57D860, 0x57D800, 0x1964540);
                                case 24:
                                    return CreateOffsets(0x8C4F40, 0x8C87A0, 0x00, 0x8CD980, 0x8114A0, 0x57DDF0, 0x57DD90, 0x1975600);
                                case 25:
                                    return CreateOffsets(0x8C6A10, 0x8CA270, 0x00, 0x8CF450, 0x812FE0, 0x57F900, 0x57F8A0, 0x1977600);
                                case 26:
                                    return CreateOffsets(0x8C6CA0, 0x8CA500, 0x00, 0x8CF6E0, 0x813260, 0x57FBA0, 0x57FB40, 0x1977600);
                                case 27:
                                    return CreateOffsets(0x8C7DE0, 0x8CB640, 0x00, 0x8D0820, 0x814100, 0x5809F0, 0x580990, 0x1978640);
                                case 28:
                                    return CreateOffsets(0x8C86E0, 0x8CBF40, 0x00, 0x8D1120, 0x8149C0, 0x581040, 0x580FE0, 0x1979640);
                                case 29:
                                    return CreateOffsets(0x8C8E10, 0x8CC670, 0x00, 0x8D1850, 0x8150F0, 0x5812C0, 0x581260, 0x19776C0);
                                case 30:
                                    return CreateOffsets(0x8C9270, 0x8CCAD0, 0x00, 0x8D1CB0, 0x815550, 0x5815F0, 0x581590, 0x19776C0);
                                case 31:
                                    return CreateOffsets(0x8D0F00, 0x8D4820, 0x00, 0x8D9CE0, 0x81AA50, 0x585110, 0x5850B0, 0x1981700);
                                case 32:
                                    return CreateOffsets(0x8D1200, 0x8D4B20, 0x00, 0x8D9FE0, 0x81AD40, 0x5852A0, 0x585240, 0x1982700);
                                case 33:
                                    return CreateOffsets(0x8D0FC0, 0x8D48E0, 0x00, 0x8D9DA0, 0x81AAC0, 0x585110, 0x5850B0, 0x1982700);
                                case 34:
                                    return CreateOffsets(0x8D1120, 0x8D4A40, 0x00, 0x8D9F00, 0x81AB00, 0x585100, 0x5850A0, 0x1983700);
                                case 35:
                                    return CreateOffsets(0x8D1AC0, 0x8D53E0, 0x00, 0x8DA8A0, 0x81B420, 0x5853A0, 0x585340, 0x1983700);
                                case 36:
                                    return CreateOffsets(0x8D1C80, 0x8D55A0, 0x00, 0x8DAA60, 0x81B5E0, 0x5854A0, 0x585440, 0x1985700);
                                case 37:
                                    return CreateOffsets(0x8D2920, 0x8D6240, 0x00, 0x8DB700, 0x81C280, 0x585900, 0x5858A0, 0x1989700);
                                case 38:
                                    return CreateOffsets(0x8D4F60, 0x8D88A0, 0x00, 0x8DDD60, 0x81E870, 0x585F90, 0x585F30, 0x1989780);
                                case 39:
                                    return CreateOffsets(0x8D51C0, 0x8D8B00, 0x00, 0x8DDFC0, 0x81E560, 0x5860D0, 0x586070, 0x1993740);
                                case 40:
                                    return CreateOffsets(0x8D55E0, 0x8D8F20, 0x00, 0x8DE3E0, 0x81E980, 0x5860F0, 0x586090, 0x19B4740);
                                case 41:
                                    return CreateOffsets(0x8D66D0, 0x8DA010, 0x00, 0x8DF4D0, 0x81F830, 0x5868F0, 0x586890, 0x19B6800);
                                case 42:
                                    return CreateOffsets(0x8D8ED0, 0x8DC810, 0x00, 0x8E1CD0, 0x821F20, 0x588DE0, 0x588D80, 0x19B9800);
                                case 43:
                                    return CreateOffsets(0x8D9D50, 0x8DD690, 0x00, 0x8E2B50, 0x822DA0, 0x589080, 0x589020, 0x19BA800);
                                case 44:
                                    return CreateOffsets(0x8D8530, 0x8DBE40, 0x00, 0x8E0F00, 0x821540, 0x588390, 0x588330, 0x19B67C0);
                                case 45:
                                    return CreateOffsets(0x890800, 0x894110, 0x00, 0x8991D0, 0x7D96A0, 0x53F730, 0x53F6D0, 0x19BC7C0);
                                case 46:
                                    return CreateOffsets(0x890CD0, 0x8945E0, 0x00, 0x8996A0, 0x7D9B70, 0x53FA70, 0x53FA10, 0x19BC800);
                                case 47:
                                    return CreateOffsets(0x890E70, 0x894780, 0x00, 0x899840, 0x7D9D10, 0x53FC40, 0x53FBE0, 0x19BD800);
                                case 48:
                                    return CreateOffsets(0x891B30, 0x895440, 0x00, 0x89A500, 0x7DAA90, 0x540680, 0x540620, 0x19BD800);
                                case 49:
                                    return CreateOffsets(0x891B70, 0x895270, 0x00, 0x89A2E0, 0x7DAAD0, 0x540680, 0x540620, 0x19BD800);
                            }
                            break;
                    }
                    break;
                case 2021:
                    switch (unityVersion.Minor)
                    {
                        case 1:
                            switch (unityVersion.Build)
                            {
                                case 0:
                                    return CreateOffsets(0x8AF6D0, 0x8B29B0, 0x00, 0x8B7790, 0x7FD5A0, 0x572570, 0x572510, 0x197A280);
                                case 1:
                                    return CreateOffsets(0x8B0320, 0x8B3600, 0x00, 0x8B83E0, 0x7FE1B0, 0x572B30, 0x572AD0, 0x197C280);
                                case 2:
                                    return CreateOffsets(0x8B0F00, 0x8B41E0, 0x00, 0x8B8FC0, 0x7FED00, 0x573680, 0x573620, 0x197D280);
                                case 3:
                                    return CreateOffsets(0x8B1420, 0x8B4700, 0x00, 0x8B94E0, 0x7FF080, 0x5737B0, 0x573750, 0x198C280);
                                case 4:
                                    return CreateOffsets(0x8B17F0, 0x8B4AD0, 0x00, 0x8B98B0, 0x7FF060, 0x573670, 0x573610, 0x198D280);
                                case 5:
                                    return CreateOffsets(0x8B1480, 0x8B4760, 0x00, 0x8B9530, 0x7FED30, 0x5735F0, 0x573590, 0x1943280);
                                case 6:
                                    return CreateOffsets(0x8B1C20, 0x8B4F00, 0x00, 0x8B9CD0, 0x7FF3B0, 0x573D10, 0x573CB0, 0x1944280);
                                case 7:
                                    return CreateOffsets(0x8B1C10, 0x8B4EF0, 0x00, 0x8B9CC0, 0x7FF3A0, 0x573CB0, 0x573C50, 0x1944280);
                                case 9:
                                    return CreateOffsets(0x8B2680, 0x8B59B0, 0x00, 0x8BA780, 0x7FFD00, 0x5740E0, 0x574080, 0x19422C0);
                                case 10:
                                    return CreateOffsets(0x8B3110, 0x8B6440, 0x00, 0x8BB210, 0x800690, 0x5748F0, 0x574890, 0x19432C0);
                                case 11:
                                    return CreateOffsets(0x8B3110, 0x8B6440, 0x00, 0x8BB210, 0x800690, 0x5748F0, 0x574890, 0x19432C0);
                                case 12:
                                    return CreateOffsets(0x8B3150, 0x8B6480, 0x00, 0x8BB250, 0x8006C0, 0x574920, 0x5748C0, 0x19432C0);
                                case 13:
                                    return CreateOffsets(0x8B31E0, 0x8B6510, 0x00, 0x8BB2E0, 0x800750, 0x5749B0, 0x574950, 0x19432C0);
                                case 14:
                                    return CreateOffsets(0x8B3770, 0x8B6AA0, 0x00, 0x8BB870, 0x800CE0, 0x574F50, 0x574EF0, 0x19442C0);
                                case 15:
                                    return CreateOffsets(0x8B3A10, 0x8B6D50, 0x00, 0x8BBAD0, 0x800FC0, 0x575230, 0x5751D0, 0x19472C0);
                                case 16:
                                    return CreateOffsets(0x895A80, 0x898DC0, 0x00, 0x89DB40, 0x7E2FA0, 0x5575B0, 0x557550, 0x1949B00);
                                case 17:
                                    return CreateOffsets(0x896400, 0x899740, 0x00, 0x89E4C0, 0x7E38E0, 0x557C00, 0x557BA0, 0x194AB00);
                                case 18:
                                    return CreateOffsets(0x89A290, 0x89D5D0, 0x00, 0x8A2350, 0x7E7710, 0x55BA50, 0x55B9F0, 0x194EC40);
                                case 19:
                                    return CreateOffsets(0x89A720, 0x89DA60, 0x00, 0x8A27E0, 0x7E7BA0, 0x55BE10, 0x55BDB0, 0x194EC40);
                                case 20:
                                    return CreateOffsets(0x89AB40, 0x89DE80, 0x00, 0x8A2C00, 0x7E7FC0, 0x55C190, 0x55C130, 0x194FC40);
                                case 21:
                                    return CreateOffsets(0x89AD20, 0x89E060, 0x00, 0x8A2DE0, 0x7E8170, 0x55C270, 0x55C210, 0x194FC40);
                                case 22:
                                    return CreateOffsets(0x89B1B0, 0x89E4F0, 0x00, 0x8A3270, 0x7E86C0, 0x55C260, 0x55C200, 0x194FC40);
                                case 23:
                                    return CreateOffsets(0x89AFA0, 0x89E2E0, 0x00, 0x8A3060, 0x7E84B0, 0x55C140, 0x55C0E0, 0x194FC40);
                                case 24:
                                    return CreateOffsets(0x89AB00, 0x89DE40, 0x00, 0x8A2BC0, 0x7E8030, 0x55BD00, 0x55BCA0, 0x1954C40);
                                case 25:
                                    return CreateOffsets(0x89BD40, 0x89F080, 0x00, 0x8A3E00, 0x7E8EE0, 0x55C3E0, 0x55C380, 0x1956C40);
                                case 26:
                                    return CreateOffsets(0x89BF90, 0x89F2D0, 0x00, 0x8A4050, 0x7E9120, 0x55C5A0, 0x55C540, 0x1956C40);
                                case 27:
                                    return CreateOffsets(0x89BCD0, 0x89F010, 0x00, 0x8A3D90, 0x7E8E60, 0x55C200, 0x55C1A0, 0x1956C40);
                                case 28:
                                    return CreateOffsets(0x89BBE0, 0x89EF20, 0x00, 0x8A3CA0, 0x7E8D30, 0x55C060, 0x55C000, 0x1956C40);
                                case 29:
                                    return CreateOffsets(0x89BC20, 0x89ED00, 0x00, 0x8A3A40, 0x7E8D70, 0x55C060, 0x55C000, 0x1956C40);
                            }
                            break;
                        case 2:
                            switch (unityVersion.Build)
                            {
                                case 0:
                                    return CreateOffsets(0x658320, 0x65BC70, 0x00, 0x661E10, 0x589CF0, 0x2BD660, 0x2BD610, 0x19FEF00);
                                case 1:
                                    return CreateOffsets(0x658430, 0x65BD80, 0x00, 0x661F20, 0x589DD0, 0x2BD660, 0x2BD610, 0x19FEF00);
                                case 2:
                                    return CreateOffsets(0x658D20, 0x65C670, 0x00, 0x662810, 0x58A6C0, 0x2BDCC0, 0x2BDC70, 0x1A00F40);
                                case 3:
                                    return CreateOffsets(0x659020, 0x65C970, 0x00, 0x662B10, 0x58A9E0, 0x2BDC60, 0x2BDC10, 0x1A01F80);
                                case 4:
                                    return CreateOffsets(0x658DE0, 0x65C730, 0x00, 0x6628D0, 0x58A7A0, 0x2BDD40, 0x2BDCF0, 0x1A02040);
                                case 5:
                                    return CreateOffsets(0x6592D0, 0x65CC20, 0x00, 0x662DC0, 0x58AC90, 0x2BDFD0, 0x2BDF80, 0x1A02FC0);
                                case 6:
                                    return CreateOffsets(0x659400, 0x65CD50, 0x00, 0x662EF0, 0x58ADC0, 0x2BDFD0, 0x2BDF80, 0x1A03000);
                                case 7:
                                    return CreateOffsets(0x659540, 0x65CE90, 0x00, 0x663030, 0x58AF00, 0x2BE110, 0x2BE0C0, 0x1A04000);
                                case 8:
                                    return CreateOffsets(0x659A40, 0x65D390, 0x00, 0x663530, 0x58B3E0, 0x2BE290, 0x2BE240, 0x1A06040);
                                case 9:
                                    return CreateOffsets(0x659EA0, 0x65D7F0, 0x00, 0x663990, 0x58B7B0, 0x2BE3E0, 0x2BE390, 0x1A07080);
                                case 10:
                                    return CreateOffsets(0x65A0E0, 0x65DA30, 0x00, 0x663BD0, 0x58B730, 0x2BE3E0, 0x2BE390, 0x1A08080);
                                case 11:
                                    return CreateOffsets(0x65A090, 0x65D9E0, 0x00, 0x663B80, 0x58B6E0, 0x2BE370, 0x2BE320, 0x1A08080);
                                case 12:
                                    return CreateOffsets(0x65A4E0, 0x65DE30, 0x00, 0x663FD0, 0x58BB40, 0x2BE470, 0x2BE420, 0x1A09100);
                                case 13:
                                    return CreateOffsets(0x65A4F0, 0x65DE40, 0x00, 0x663FE0, 0x58BB40, 0x2BE470, 0x2BE420, 0x1A09100);
                                case 14:
                                    return CreateOffsets(0x65A670, 0x65DFC0, 0x00, 0x664160, 0x58BCC0, 0x2BE470, 0x2BE420, 0x1A09100);
                                case 15:
                                    return CreateOffsets(0x65A670, 0x65DFC0, 0x00, 0x664160, 0x58BCC0, 0x2BE470, 0x2BE420, 0x1A0A100);
                                case 16:
                                    return CreateOffsets(0x65A670, 0x65DFC0, 0x00, 0x664160, 0x58BCC0, 0x2BE470, 0x2BE420, 0x1A0C100);
                                case 17:
                                    return CreateOffsets(0x65AC60, 0x65E5C0, 0x00, 0x664650, 0x58BD40, 0x2BE470, 0x2BE420, 0x1A0E100);
                                case 18:
                                    return CreateOffsets(0x65BA30, 0x65F390, 0x00, 0x665420, 0x58CD10, 0x2BE800, 0x2BE7B0, 0x1A0F100);
                                case 19:
                                    return CreateOffsets(0x65BAB0, 0x65F410, 0x00, 0x6654A0, 0x58CD90, 0x2BE930, 0x2BE8E0, 0x1A0F100);
                                case 20:
                                    return CreateOffsets(0x65BAF0, 0x00, 0x586F10, 0x6652B0, 0x58CDD0, 0x2BE930, 0x2BE8E0, 0x1A0F100);
                            }
                            break;
                        case 3:
                            switch (unityVersion.Build)
                            {
                                case 0:
                                    return CreateOffsets(0x65BAB0, 0x65F410, 0x00, 0x6654A0, 0x58CD90, 0x2BE930, 0x2BE8E0, 0x1A0F100);
                                case 1:
                                    return CreateOffsets(0x65C250, 0x65FBB0, 0x00, 0x665C40, 0x58D500, 0x2BE930, 0x2BE8E0, 0x1A10100);
                                case 2:
                                    return CreateOffsets(0x65C5D0, 0x65FF30, 0x00, 0x665FC0, 0x58D6D0, 0x2BE930, 0x2BE8E0, 0x1A11100);
                                case 3:
                                    return CreateOffsets(0x65C620, 0x65FF80, 0x00, 0x666010, 0x58D720, 0x2BE990, 0x2BE940, 0x1A12140);
                                case 4:
                                    return CreateOffsets(0x65D220, 0x660B80, 0x00, 0x666C10, 0x58E320, 0x2BEC90, 0x2BEC40, 0x1A14100);
                                case 5:
                                    return CreateOffsets(0x65D8E0, 0x661240, 0x00, 0x6672D0, 0x58E970, 0x2BECB0, 0x2BEC60, 0x1A14140);
                                case 6:
                                    return CreateOffsets(0x614D30, 0x618690, 0x00, 0x61E720, 0x545DC0, 0x2760E0, 0x276090, 0x1A1A140);
                                case 7:
                                    return CreateOffsets(0x615D60, 0x6196C0, 0x00, 0x61F750, 0x546DF0, 0x276D40, 0x276CF0, 0x1A1B140);
                                case 8:
                                    return CreateOffsets(0x615DF0, 0x619750, 0x00, 0x6274C0, 0x546E80, 0x276D80, 0x276D30, 0x1A1B280);
                                case 9:
                                    return CreateOffsets(0x617350, 0x61AC80, 0x00, 0x628940, 0x548470, 0x276C50, 0x276C00, 0x1A1A280);
                                case 10:
                                    return CreateOffsets(0x618F40, 0x61C870, 0x00, 0x62A530, 0x54A050, 0x276EC0, 0x276E70, 0x1A1E2C0);
                                case 11:
                                    return CreateOffsets(0x619700, 0x61D030, 0x00, 0x62ACF0, 0x54A5C0, 0x277000, 0x276FB0, 0x1A3E2C0);
                                case 12:
                                    return CreateOffsets(0x61CFF0, 0x620920, 0x00, 0x62E5F0, 0x54D400, 0x277600, 0x2775B0, 0x1A44780);
                                case 13:
                                    return CreateOffsets(0x61D670, 0x620FA0, 0x00, 0x62EC70, 0x54D7D0, 0x2776B0, 0x277660, 0x1A46780);
                                case 14:
                                    return CreateOffsets(0x61DC90, 0x6215C0, 0x00, 0x62F290, 0x54DDD0, 0x2777A0, 0x277750, 0x1A497C0);
                                case 15:
                                    return CreateOffsets(0x61EA00, 0x622330, 0x00, 0x630000, 0x54EB20, 0x2777F0, 0x2777A0, 0x1A49800);
                                case 16:
                                    return CreateOffsets(0x625340, 0x628C70, 0x00, 0x636940, 0x555200, 0x27D9A0, 0x27D950, 0x1A73A80);
                                case 17:
                                    return CreateOffsets(0x6257D0, 0x629100, 0x00, 0x636DD0, 0x555780, 0x27E520, 0x27E4D0, 0x1A76B00);
                                case 18:
                                    return CreateOffsets(0x6262F0, 0x629C20, 0x00, 0x637900, 0x556290, 0x27E650, 0x27E600, 0x1A77B80);
                                case 19:
                                    return CreateOffsets(0x626560, 0x629E90, 0x00, 0x637B70, 0x5563E0, 0x27E730, 0x27E6E0, 0x1A7BB80);
                                case 20:
                                    return CreateOffsets(0x626A30, 0x62A360, 0x00, 0x638040, 0x5568B0, 0x27E9C0, 0x27E970, 0x1A7BBC0);
                                case 21:
                                    return CreateOffsets(0x627280, 0x62ABB0, 0x00, 0x638890, 0x556FD0, 0x27ECA0, 0x27EC50, 0x1A8CBC0);
                                case 22:
                                    return CreateOffsets(0x627330, 0x62AC60, 0x00, 0x638940, 0x557080, 0x27ECA0, 0x27EC50, 0x1A8CBC0);
                                case 23:
                                    return CreateOffsets(0x6275B0, 0x62AEE0, 0x00, 0x638BC0, 0x5573C0, 0x27F020, 0x27EFD0, 0x1A8EC00);
                                case 24:
                                    return CreateOffsets(0x6275D0, 0x62AF00, 0x00, 0x638BE0, 0x5573B0, 0x27F020, 0x27EFD0, 0x1A8EC00);
                                case 25:
                                    return CreateOffsets(0x627740, 0x62B070, 0x00, 0x638D50, 0x557860, 0x27F050, 0x27F000, 0x1A8DC00);
                                case 26:
                                    return CreateOffsets(0x627D20, 0x62B650, 0x00, 0x639330, 0x557E30, 0x27F080, 0x27F030, 0x1A8EC00);
                                case 27:
                                    return CreateOffsets(0x627E10, 0x62B740, 0x00, 0x639420, 0x557E40, 0x27F080, 0x27F030, 0x1A8EC00);
                                case 28:
                                    return CreateOffsets(0x628950, 0x62C280, 0x00, 0x639F60, 0x5588E0, 0x27F190, 0x27F140, 0x1A90C00);
                                case 29:
                                    return CreateOffsets(0x624870, 0x6281A0, 0x00, 0x635E80, 0x554820, 0x2793E0, 0x279390, 0x1A90C40);
                                case 30:
                                    return CreateOffsets(0x6255B0, 0x628EE0, 0x00, 0x636BC0, 0x555AB0, 0x279550, 0x279500, 0x1A92C40);
                                case 31:
                                    return CreateOffsets(0x625DC0, 0x6296F0, 0x00, 0x6373D0, 0x555F10, 0x279910, 0x2798C0, 0x1AA0C40);
                                case 32:
                                    return CreateOffsets(0x626640, 0x629F70, 0x00, 0x637C50, 0x556750, 0x279DB0, 0x279D60, 0x1AA1C40);
                                case 33:
                                    return CreateOffsets(0x625DC0, 0x6296F0, 0x00, 0x6373D0, 0x555CC0, 0x279390, 0x279340, 0x1A9EBC0);
                                case 34:
                                    return CreateOffsets(0x627430, 0x62AD60, 0x00, 0x638CC0, 0x556FD0, 0x279620, 0x2795D0, 0x1AA1C00);
                                case 35:
                                    return CreateOffsets(0x627700, 0x62B030, 0x00, 0x638F90, 0x5572A0, 0x2796A0, 0x279650, 0x1AA3C00);
                                case 36:
                                    return CreateOffsets(0x627660, 0x62AF90, 0x00, 0x638EF0, 0x557330, 0x2796E0, 0x279690, 0x1AA3C00);
                                case 37:
                                    return CreateOffsets(0x6277D0, 0x62B100, 0x00, 0x639060, 0x557140, 0x279300, 0x2792B0, 0x1AA4C00);
                                case 38:
                                    return CreateOffsets(0x627AA0, 0x62B3D0, 0x00, 0x639330, 0x557410, 0x279300, 0x2792B0, 0x1AA4C00);
                                case 39:
                                    return CreateOffsets(0x627BA0, 0x62B4D0, 0x00, 0x639460, 0x557510, 0x2791E0, 0x279190, 0x1AA6C00);
                                case 40:
                                    return CreateOffsets(0x627E80, 0x62B7B0, 0x00, 0x639740, 0x5577B0, 0x2792E0, 0x279290, 0x1AA8C00);
                                case 41:
                                    return CreateOffsets(0x629280, 0x62CBB0, 0x00, 0x63AB40, 0x558D90, 0x279430, 0x2793E0, 0x1AA8C00);
                                case 42:
                                    return CreateOffsets(0x629360, 0x62CC90, 0x00, 0x63AC20, 0x558E70, 0x279430, 0x2793E0, 0x1AA8C00);
                                case 43:
                                    return CreateOffsets(0x629390, 0x62CCC0, 0x00, 0x63AC50, 0x558EA0, 0x279430, 0x2793E0, 0x1A97C00);
                                case 44:
                                    return CreateOffsets(0x62D4E0, 0x630E10, 0x00, 0x63EDA0, 0x55A210, 0x27A790, 0x27A740, 0x1A9BC00);
                                case 45:
                                    switch (unityVersion.Revision)
                                    {
                                        case 43167: // f1
                                            return CreateOffsets(0x62D510, 0x630E40, 0x00, 0x63EDD0, 0x55A240, 0x27A790, 0x27A740, 0x1A9CC00);
                                        case 63631: // f2
                                            return CreateOffsets(0x62D560, 0x00, 0x554320, 0x63EC50, 0x55A290, 0x27A790, 0x27A740, 0x1A9CC00);
                                    }
                                    break;
                                case 46:
                                    return CreateOffsets(0x62D510, 0x630E40, 0x00, 0x63EDD0, 0x55A240, 0x27A790, 0x27A740, 0x1AB6C00);
                                case 47:
                                    return CreateOffsets(0x62D510, 0x630E40, 0x00, 0x63EDD0, 0x55A240, 0x27A790, 0x27A740, 0x1AB6C00);
                                case 48:
                                    return CreateOffsets(0x62D510, 0x630E40, 0x00, 0x63EDD0, 0x55A240, 0x27A790, 0x27A740, 0x1AB7C00);
                                case 49:
                                    return CreateOffsets(0x62D510, 0x630E40, 0x00, 0x63EDD0, 0x55A240, 0x27A790, 0x27A740, 0x1AB7C00);
                                case 50:
                                    return CreateOffsets(0x62D510, 0x630E40, 0x00, 0x63EDD0, 0x55A240, 0x27A790, 0x27A740, 0x1AB7C00);
                            }
                            break;
                    }
                    break;
                case 2022:
                    switch (unityVersion.Minor)
                    {
                        case 1:
                            switch (unityVersion.Build)
                            {
                                case 0:
                                    return CreateOffsets(0x676960, 0x67A200, 0x00, 0x680400, 0x5A2210, 0x2C4460, 0x2C4410, 0x1A40DC0);
                                case 1:
                                    return CreateOffsets(0x676CA0, 0x67A550, 0x00, 0x680750, 0x5A23C0, 0x2C44F0, 0x2C44A0, 0x1A40DC0);
                                case 2:
                                    return CreateOffsets(0x677830, 0x67B0E0, 0x00, 0x6812E0, 0x5A2F50, 0x2C4660, 0x2C4610, 0x1A41D80);
                                case 3:
                                    return CreateOffsets(0x677830, 0x67B0E0, 0x00, 0x6812E0, 0x5A2F50, 0x2C4660, 0x2C4610, 0x1A41D80);
                                case 4:
                                    return CreateOffsets(0x6776C0, 0x67AF70, 0x00, 0x681170, 0x5A2DE0, 0x2C4660, 0x2C4610, 0x1A41D80);
                                case 5:
                                    return CreateOffsets(0x6776C0, 0x67AF70, 0x00, 0x681170, 0x5A2DE0, 0x2C4660, 0x2C4610, 0x1A41D80);
                                case 6:
                                    return CreateOffsets(0x677670, 0x67AF20, 0x00, 0x681120, 0x5A2C60, 0x2C4660, 0x2C4610, 0x1A41D80);
                                case 7:
                                    return CreateOffsets(0x677670, 0x67AF20, 0x00, 0x681120, 0x5A2C60, 0x2C4660, 0x2C4610, 0x1A42D80);
                                case 8:
                                    return CreateOffsets(0x6777B0, 0x67B060, 0x00, 0x681260, 0x5A2DA0, 0x2C4660, 0x2C4610, 0x1A42D80);
                                case 9:
                                    return CreateOffsets(0x678510, 0x67BDC0, 0x00, 0x681FC0, 0x5A3B00, 0x2C4780, 0x2C4730, 0x1A66D80);
                                case 10:
                                    return CreateOffsets(0x678510, 0x67BDC0, 0x00, 0x689E60, 0x5A3B00, 0x2C4780, 0x2C4730, 0x1A66E80);
                                case 11:
                                    return CreateOffsets(0x678510, 0x67BDC0, 0x00, 0x689E60, 0x5A3B00, 0x2C4780, 0x2C4730, 0x1A66E80);
                                case 12:
                                    return CreateOffsets(0x678920, 0x67C1D0, 0x00, 0x68A280, 0x5A3F00, 0x2C4860, 0x2C4810, 0x1A66EC0);
                                case 13:
                                    return CreateOffsets(0x67B7E0, 0x67F090, 0x00, 0x68D140, 0x5A6DD0, 0x2C4860, 0x2C4810, 0x1A69EC0);
                                case 14:
                                    return CreateOffsets(0x67B730, 0x67EFE0, 0x00, 0x68D090, 0x5A6D20, 0x2C47C0, 0x2C4770, 0x1A71EC0);
                                case 15:
                                    return CreateOffsets(0x67B730, 0x67EFE0, 0x00, 0x68D090, 0x5A6D20, 0x2C47C0, 0x2C4770, 0x1A73EC0);
                                case 16:
                                    return CreateOffsets(0x67B830, 0x67F0E0, 0x00, 0x68D190, 0x5A6D20, 0x2C47C0, 0x2C4770, 0x1A73EC0);
                                case 17:
                                    return CreateOffsets(0x67BB80, 0x67F430, 0x00, 0x68D4E0, 0x5A7070, 0x2C47B0, 0x2C4760, 0x1A72EC0);
                                case 18:
                                    return CreateOffsets(0x67C610, 0x67FEC0, 0x00, 0x68DF70, 0x5A78E0, 0x2C4910, 0x2C48C0, 0x1A73F40);
                                case 19:
                                    return CreateOffsets(0x67C7F0, 0x6800A0, 0x00, 0x68E150, 0x5A7AC0, 0x2C4A50, 0x2C4A00, 0x1A73F40);
                                case 20:
                                    return CreateOffsets(0x67C7E0, 0x680090, 0x00, 0x68E140, 0x5A7AB0, 0x2C4A40, 0x2C49F0, 0x1A73F40);
                                case 21:
                                    return CreateOffsets(0x67DF50, 0x681800, 0x00, 0x68F8B0, 0x5A90A0, 0x2C4BD0, 0x2C4B80, 0x1A75F80);
                                case 22:
                                    return CreateOffsets(0x67E010, 0x6818C0, 0x00, 0x68F970, 0x5A9160, 0x2C4BD0, 0x2C4B80, 0x1A75F80);
                                case 23:
                                    return CreateOffsets(0x67E1A0, 0x681A50, 0x00, 0x68FB00, 0x5A9280, 0x2C4C70, 0x2C4C20, 0x1A77F80);
                                case 24:
                                    return CreateOffsets(0x67E710, 0x681FC0, 0x00, 0x690070, 0x5A9780, 0x2C4C70, 0x2C4C20, 0x1A75F80);
                                case 25:
                                    return CreateOffsets(0x67E750, 0x00, 0x5A38C0, 0x68FF00, 0x5A97C0, 0x2C4C70, 0x2C4C20, 0x1A75F80);
                            }
                            break;
                        case 2:
                            switch (unityVersion.Build)
                            {
                                case 0:
                                    return CreateOffsets(0x7481A0, 0x747CD0, 0x00, 0x75C9C0, 0x661590, 0x34C760, 0x627AA0, 0x1B93FC0);
                                case 1:
                                    return CreateOffsets(0x748BC0, 0x7486F0, 0x00, 0x75D3E0, 0x661FB0, 0x34C280, 0x6284C0, 0x1B94000);
                                case 2:
                                    return CreateOffsets(0x74A2B0, 0x749DE0, 0x00, 0x75EAD0, 0x663470, 0x34C110, 0x629980, 0x1B98000);
                                case 3:
                                    return CreateOffsets(0x74B260, 0x74AD90, 0x00, 0x75FA80, 0x6643E0, 0x34CE30, 0x629920, 0x1B97C00);
                                case 4:
                                    return CreateOffsets(0x74FBA0, 0x74F6D0, 0x00, 0x7644F0, 0x667EB0, 0x34FB60, 0x62D440, 0x1B9F140);
                                case 5:
                                    return CreateOffsets(0x750320, 0x74FE50, 0x00, 0x764C80, 0x668630, 0x34FF80, 0x62DBC0, 0x1BA0100);
                                case 6:
                                    return CreateOffsets(0x750510, 0x750040, 0x00, 0x764E70, 0x6686D0, 0x34FF40, 0x62DC60, 0x1BA0140);
                                case 7:
                                    return CreateOffsets(0x753790, 0x7532C0, 0x00, 0x7680F0, 0x66B9A0, 0x351480, 0x6311C0, 0x1BA3140);
                                case 8:
                                    return CreateOffsets(0x753F00, 0x753A30, 0x00, 0x768860, 0x66C040, 0x351B30, 0x631870, 0x1BA4140);
                                case 9:
                                    return CreateOffsets(0x753E20, 0x753950, 0x00, 0x768780, 0x66C550, 0x351EC0, 0x631D80, 0x1BA6140);
                                case 10:
                                    return CreateOffsets(0x754400, 0x753F30, 0x00, 0x768D60, 0x66CC70, 0x352240, 0x632490, 0x1BA4140);
                                case 11:
                                    return CreateOffsets(0x754CE0, 0x754810, 0x00, 0x769640, 0x66D4B0, 0x352120, 0x632A20, 0x1BA5180);
                                case 12:
                                    return CreateOffsets(0x7552E0, 0x754E10, 0x00, 0x769C40, 0x66DAB0, 0x351A30, 0x633020, 0x1BA7180);
                                case 13:
                                    return CreateOffsets(0x754810, 0x754340, 0x00, 0x769170, 0x66D060, 0x351560, 0x6325D0, 0x1BA6180);
                                case 14:
                                    return CreateOffsets(0x7559C0, 0x7554F0, 0x00, 0x76A320, 0x66E210, 0x351DF0, 0x633780, 0x1BA91C0);
                                case 15:
                                    return CreateOffsets(0x757030, 0x756B60, 0x00, 0x76B9B0, 0x66F970, 0x3524A0, 0x634EE0, 0x1BAB1C0);
                                case 16:
                                    return CreateOffsets(0x757660, 0x757190, 0x00, 0x76BFE0, 0x670090, 0x3523B0, 0x635600, 0x1BAB140);
                                case 17:
                                    return CreateOffsets(0x757940, 0x757470, 0x00, 0x76C2C0, 0x670260, 0x3523A0, 0x6357C0, 0x1BAA100);
                                case 18:
                                    return CreateOffsets(0x75AD80, 0x75A8B0, 0x00, 0x76F700, 0x673600, 0x352550, 0x638A70, 0x1BB01C0);
                                case 19:
                                    return CreateOffsets(0x75B110, 0x75AC40, 0x00, 0x76FA90, 0x673990, 0x352570, 0x638E00, 0x1BC5200);
                                case 20:
                                    return CreateOffsets(0x77AED0, 0x77AA00, 0x00, 0x78F9D0, 0x692050, 0x367A90, 0x6576A0, 0x1BF0280);
                                case 21:
                                    return CreateOffsets(0x77B8B0, 0x77B3E0, 0x00, 0x7903B0, 0x692AC0, 0x367BA0, 0x658110, 0x1BF02C0);
                                case 23:
                                    return CreateOffsets(0x77BFC0, 0x00, 0x687F20, 0x7907E0, 0x692DE0, 0x367C20, 0x658210, 0x1BF1300);
                            }
                            break;
                        case 3:
                            switch (unityVersion.Build)
                            {
                                case 0:
                                    return CreateOffsets(0x77BEA0, 0x77B9D0, 0x00, 0x790A70, 0x692DC0, 0x367C10, 0x6581F0, 0x1BF12C0);
                                case 1:
                                    return CreateOffsets(0x77B530, 0x77B060, 0x00, 0x7900E0, 0x692760, 0x367B50, 0x657BE0, 0x1BF1300);
                                case 2:
                                    return CreateOffsets(0x77B6D0, 0x77B200, 0x00, 0x790280, 0x692850, 0x367BE0, 0x657CD0, 0x1BF1300);
                                case 3:
                                    return CreateOffsets(0x77B470, 0x77AFA0, 0x00, 0x790020, 0x6927F0, 0x367C50, 0x657C90, 0x1BF2300);
                                case 4:
                                    return CreateOffsets(0x77B5F0, 0x77B120, 0x00, 0x7901A0, 0x692960, 0x367DC0, 0x657E00, 0x1BF2300);
                                case 5:
                                    return CreateOffsets(0x769270, 0x768DA0, 0x00, 0x77DE20, 0x680490, 0x3558B0, 0x645520, 0x1BF28C0);
                                case 6:
                                    return CreateOffsets(0x7690C0, 0x768BF0, 0x00, 0x77DC70, 0x6801B0, 0x355270, 0x6451A0, 0x1BF4900);
                                case 7:
                                    return CreateOffsets(0x76ACF0, 0x76A820, 0x00, 0x77FE80, 0x681D90, 0x355220, 0x646C90, 0x1BF89C0);
                                case 8:
                                    return CreateOffsets(0x76BCB0, 0x76B7E0, 0x00, 0x780F50, 0x682F80, 0x355660, 0x647E70, 0x1BFA9C0);
                                case 9:
                                    return CreateOffsets(0x76C0C0, 0x76BBF0, 0x00, 0x781360, 0x683330, 0x3556D0, 0x648220, 0x1BFBA40);
                                case 10:
                                    return CreateOffsets(0x76BE50, 0x76B980, 0x00, 0x7810F0, 0x682F70, 0x355B60, 0x647FE0, 0x1BFBA00);
                                case 11:
                                    return CreateOffsets(0x76DFA0, 0x76DAD0, 0x00, 0x783280, 0x684480, 0x356760, 0x649A10, 0x1BFB840);
                                case 12:
                                    return CreateOffsets(0x76E220, 0x76DD50, 0x00, 0x783520, 0x6846F0, 0x356DA0, 0x6474E0, 0x1BFC8C0);
                                case 13:
                                    return CreateOffsets(0x76E480, 0x76DFB0, 0x00, 0x783780, 0x684750, 0x356E00, 0x647540, 0x1C008C0);
                                case 14:
                                    return CreateOffsets(0x773F20, 0x773D40, 0x00, 0x789C10, 0x689770, 0x357170, 0x64BB80, 0x1C1D8C0);
                                case 15:
                                    return CreateOffsets(0x7742E0, 0x774100, 0x00, 0x78A080, 0x689A70, 0x357030, 0x64BE80, 0x1C1F8C0);
                                case 16:
                                    return CreateOffsets(0x776DF0, 0x776C10, 0x00, 0x78CB70, 0x68C400, 0x3573E0, 0x64E710, 0x1C25900);
                                case 17:
                                    return CreateOffsets(0x776560, 0x776380, 0x00, 0x78C2E0, 0x68BC80, 0x3573F0, 0x64DF90, 0x1C248C0);
                                case 18:
                                    return CreateOffsets(0x776540, 0x776360, 0x00, 0x78C2C0, 0x68BC60, 0x357370, 0x64DF70, 0x1C248C0);
                                case 19:
                                    return CreateOffsets(0x77AFB0, 0x77ADD0, 0x00, 0x790D30, 0x6905D0, 0x357AB0, 0x652CA0, 0x1C28940);
                                case 20:
                                    return CreateOffsets(0x782BE0, 0x782A00, 0x00, 0x798980, 0x6981E0, 0x358AB0, 0x65A850, 0x1C30940);
                                case 21:
                                    return CreateOffsets(0x77F8F0, 0x77F710, 0x00, 0x795690, 0x68D2E0, 0x358C80, 0x657840, 0x1C30940);
                                case 22:
                                    return CreateOffsets(0x780400, 0x780220, 0x00, 0x7961A0, 0x68DB30, 0x3592E0, 0x658090, 0x1C31940);
                                case 23:
                                    return CreateOffsets(0x77FE90, 0x77FCB0, 0x00, 0x795C30, 0x68D5C0, 0x359130, 0x657B20, 0x1C32940);
                                case 24:
                                    return CreateOffsets(0x77F8E0, 0x77F700, 0x00, 0x795680, 0x68CC50, 0x358780, 0x6571B0, 0x1C32940);
                                case 25:
                                    return CreateOffsets(0x77E160, 0x77DF80, 0x00, 0x7938C0, 0x6891F0, 0x357F20, 0x653830, 0x1C31940);
                                case 26:
                                    return CreateOffsets(0x77E9A0, 0x77E7C0, 0x00, 0x794100, 0x688EA0, 0x357EF0, 0x6534D0, 0x1C31A40);
                                case 27:
                                    return CreateOffsets(0x77EA70, 0x77E890, 0x00, 0x7941D0, 0x688F70, 0x3578A0, 0x6535A0, 0x1C31A80);
                                case 28:
                                    return CreateOffsets(0x77E8C0, 0x77E6E0, 0x00, 0x794020, 0x688DC0, 0x357760, 0x6533F0, 0x1C31A80);
                                case 29:
                                    return CreateOffsets(0x77E910, 0x77E730, 0x00, 0x794070, 0x688E10, 0x3577B0, 0x653440, 0x1C31A80);
                                case 30:
                                    return CreateOffsets(0x77F460, 0x77F280, 0x00, 0x794B70, 0x689960, 0x358200, 0x653F90, 0x1C32A40);
                                case 31:
                                    return CreateOffsets(0x77F850, 0x77F670, 0x00, 0x794F60, 0x689C90, 0x3582A0, 0x6542C0, 0x1C33A40);
                                case 32:
                                    return CreateOffsets(0x77F8E0, 0x77F700, 0x00, 0x794FF0, 0x689D20, 0x3582A0, 0x654350, 0x1C33A40);
                                case 33:
                                    return CreateOffsets(0x77FB80, 0x77F9A0, 0x00, 0x795290, 0x689EA0, 0x358430, 0x6542E0, 0x1C35A40);
                                case 34:
                                    return CreateOffsets(0x77FD00, 0x77FB20, 0x00, 0x795410, 0x68A030, 0x3584F0, 0x654470, 0x1C35A80);
                                case 35:
                                    return CreateOffsets(0x780110, 0x77FF30, 0x00, 0x795820, 0x68A440, 0x358500, 0x6547F0, 0x1C35A40);
                                case 36:
                                    return CreateOffsets(0x77E530, 0x77E350, 0x00, 0x7933D0, 0x689B30, 0x358D60, 0x654440, 0x1C2FA40);
                                case 37:
                                    return CreateOffsets(0x77E940, 0x77E760, 0x00, 0x7937E0, 0x689F40, 0x358D00, 0x654880, 0x1C2FA40);
                                case 38:
                                    return CreateOffsets(0x77E8B0, 0x77E6D0, 0x00, 0x793750, 0x689EB0, 0x358CD0, 0x6547F0, 0x1C30A80);
                                case 39:
                                    return CreateOffsets(0x77FC70, 0x77FA90, 0x00, 0x794950, 0x68B260, 0x358B20, 0x655630, 0x1C31A40);
                                case 40:
                                    return CreateOffsets(0x77FA00, 0x77F820, 0x00, 0x7946E0, 0x68AFF0, 0x358BE0, 0x6553D0, 0x1C32A40);
                                case 41:
                                    return CreateOffsets(0x77FC40, 0x77FA60, 0x00, 0x794920, 0x68B0F0, 0x358BE0, 0x6554D0, 0x1C31A40);
                                case 42:
                                    return CreateOffsets(0x7810D0, 0x780EF0, 0x00, 0x795DB0, 0x68C580, 0x35A020, 0x656910, 0x1C0AA80);
                                case 43:
                                    return CreateOffsets(0x7812E0, 0x781100, 0x00, 0x796000, 0x68C790, 0x35A0C0, 0x656B20, 0x1C0AA40);
                                case 44:
                                    return CreateOffsets(0x781750, 0x781570, 0x00, 0x796470, 0x68C800, 0x35A030, 0x656B90, 0x1C0BA40);
                                case 45:
                                    return CreateOffsets(0x7819A0, 0x7817C0, 0x00, 0x7966C0, 0x68CA50, 0x35A040, 0x656DE0, 0x1C0CA80);
                                case 46:
                                    return CreateOffsets(0x781920, 0x781740, 0x00, 0x796640, 0x68C9D0, 0x359E10, 0x656D60, 0x1C0CA80);
                                case 47:
                                    return CreateOffsets(0x7819E0, 0x781800, 0x00, 0x796700, 0x68CA90, 0x359EA0, 0x656E20, 0x1C0CA40);
                                case 48:
                                    return CreateOffsets(0x7819C0, 0x7817E0, 0x00, 0x7966E0, 0x68CA70, 0x359E70, 0x656E00, 0x1C0DA40);
                                case 49:
                                    return CreateOffsets(0x781C50, 0x781A70, 0x00, 0x796970, 0x68CD00, 0x35A100, 0x657090, 0x1C0EA40);
                                case 50:
                                    return CreateOffsets(0x781BD0, 0x7819F0, 0x00, 0x7968F0, 0x68CC80, 0x35A0D0, 0x656FC0, 0x1C0EA40);
                                case 51:
                                    return CreateOffsets(0x7821A0, 0x781FC0, 0x00, 0x796EC0, 0x68D250, 0x35A450, 0x657340, 0x1C0FA40);
                                case 52:
                                    return CreateOffsets(0x781550, 0x781370, 0x00, 0x796270, 0x68C600, 0x35A4C0, 0x656C20, 0x1C41B80);
                                case 53:
                                    return CreateOffsets(0x780CC0, 0x780AE0, 0x00, 0x7959E0, 0x68BFF0, 0x35A8A0, 0x656740, 0x1C3FB80);
                                case 54:
                                    return CreateOffsets(0x780C00, 0x780A20, 0x00, 0x795AE0, 0x68BF30, 0x35A7F0, 0x656650, 0x1C41BC0);
                                case 55:
                                    return CreateOffsets(0x780DD0, 0x780BF0, 0x00, 0x795CB0, 0x68C0F0, 0x35AA70, 0x656810, 0x1C40C40);
                                case 56:
                                    return CreateOffsets(0x7820E0, 0x781F00, 0x00, 0x796FC0, 0x68D3A0, 0x35ADB0, 0x6579E0, 0x1C41C40);
                                case 57:
                                    return CreateOffsets(0x7821D0, 0x781FF0, 0x00, 0x7970B0, 0x68D470, 0x35AD70, 0x6579B0, 0x1C41C40);
                                case 58:
                                    return CreateOffsets(0x7844A0, 0x7842C0, 0x00, 0x799380, 0x68F460, 0x35AA50, 0x659A10, 0x1C45C40);
                                case 59:
                                    return CreateOffsets(0x7842A0, 0x7840C0, 0x00, 0x799180, 0x68F290, 0x35A7E0, 0x659840, 0x1C45C00);
                                case 60:
                                    return CreateOffsets(0x7842B0, 0x7840D0, 0x00, 0x799190, 0x68F520, 0x35AAF0, 0x659AD0, 0x1C4AC40);
                                case 61:
                                    return CreateOffsets(0x784540, 0x784360, 0x00, 0x799420, 0x68F740, 0x35A9D0, 0x659C70, 0x1C4DC40);
                                case 62:
                                    return CreateOffsets(0x784130, 0x00, 0x6921A0, 0x798C40, 0x68ED20, 0x35ADF0, 0x658E30, 0x1C4CC80);
                            }
                            break;
                    }
                    break;
                case 2023:
                    switch (unityVersion.Minor)
                    {
                        case 1:
                            switch (unityVersion.Build)
                            {
                                case 0:
                                    return CreateOffsets(0x682270, 0x682050, 0x00, 0x698020, 0x5985D0, 0x25CB00, 0x53C2C0, 0x1B43680);
                                case 1:
                                    return CreateOffsets(0x682970, 0x682750, 0x00, 0x698720, 0x598CD0, 0x25CBB0, 0x53C9E0, 0x1B45680);
                                case 2:
                                    return CreateOffsets(0x682DC0, 0x682BA0, 0x00, 0x698B70, 0x5993A0, 0x25CF40, 0x53D0B0, 0x1B45680);
                                case 3:
                                    return CreateOffsets(0x6803C0, 0x6801A0, 0x00, 0x696170, 0x5969A0, 0x25A540, 0x53A6E0, 0x1B43680);
                                case 4:
                                    return CreateOffsets(0x6803E0, 0x6801C0, 0x00, 0x696190, 0x5969C0, 0x25A380, 0x53A6D0, 0x1B48C40);
                                case 5:
                                    return CreateOffsets(0x6800E0, 0x67FEC0, 0x00, 0x695E90, 0x596690, 0x25A5B0, 0x53A3A0, 0x1B48C40);
                                case 6:
                                    return CreateOffsets(0x680290, 0x680070, 0x00, 0x696040, 0x596840, 0x25A600, 0x53A4B0, 0x1B48C40);
                                case 7:
                                    return CreateOffsets(0x67FDF0, 0x67FBD0, 0x00, 0x695BA0, 0x596280, 0x25A050, 0x539EF0, 0x1B49C40);
                                case 8:
                                    return CreateOffsets(0x6811F0, 0x680FD0, 0x00, 0x696FA0, 0x5976B0, 0x259F80, 0x53B230, 0x1B4AC00);
                                case 9:
                                    return CreateOffsets(0x681C40, 0x681A20, 0x00, 0x697B00, 0x5983B0, 0x259FB0, 0x53BF20, 0x1B4DC40);
                                case 10:
                                    return CreateOffsets(0x681E80, 0x681C60, 0x00, 0x697D40, 0x5985C0, 0x25A1A0, 0x53C130, 0x1B4FC40);
                                case 11:
                                    return CreateOffsets(0x6829D0, 0x6827B0, 0x00, 0x698890, 0x5989D0, 0x25A210, 0x53C540, 0x1B4FC40);
                                case 12:
                                    return CreateOffsets(0x6832B0, 0x683090, 0x00, 0x699170, 0x599140, 0x25A5F0, 0x53CF00, 0x1B50CC0);
                                case 13:
                                    return CreateOffsets(0x6833C0, 0x6831A0, 0x00, 0x699280, 0x5991A0, 0x25A820, 0x53CF50, 0x1B51CC0);
                                case 14:
                                    return CreateOffsets(0x683570, 0x683350, 0x00, 0x699430, 0x599320, 0x25A830, 0x53D0D0, 0x1B51D00);
                                case 15:
                                    return CreateOffsets(0x683120, 0x682F00, 0x00, 0x698FE0, 0x598F00, 0x25A7D0, 0x53CC40, 0x1B51D00);
                                case 16:
                                    return CreateOffsets(0x6834E0, 0x6832C0, 0x00, 0x6993A0, 0x5992C0, 0x25AA60, 0x53CE80, 0x1B53D00);
                                case 17:
                                    return CreateOffsets(0x683D50, 0x683B30, 0x00, 0x699BC0, 0x599AD0, 0x25B310, 0x53DC50, 0x1B59D40);
                                case 18:
                                    return CreateOffsets(0x683EB0, 0x683C90, 0x00, 0x699D50, 0x599C20, 0x25B400, 0x53DDA0, 0x1B5AD40);
                                case 19:
                                    return CreateOffsets(0x6833E0, 0x6831C0, 0x00, 0x699280, 0x599150, 0x25B410, 0x53DB90, 0x1B59D40);
                                case 20:
                                    return CreateOffsets(0x682F30, 0x682D10, 0x00, 0x698CB0, 0x599670, 0x25C770, 0x53E0E0, 0x1B5DD40);
                                case 22:
                                    return CreateOffsets(0x682F40, 0x00, 0x58E810, 0x698950, 0x5996A0, 0x25C770, 0x53E110, 0x1B5CD40);
                            }
                            break;
                        case 2:
                            switch (unityVersion.Build)
                            {
                                case 0:
                                    return CreateOffsets(0x6B0D10, 0x6B0AD0, 0x00, 0x6C3640, 0x5C5150, 0x277890, 0x277950, 0x1BD72C0);
                                case 1:
                                    return CreateOffsets(0x6B1080, 0x6B0E40, 0x00, 0x6C39C0, 0x5C5470, 0x277730, 0x2777F0, 0x1BD72C0);
                                case 2:
                                    return CreateOffsets(0x6B1730, 0x6B14F0, 0x00, 0x6C4070, 0x5C5BE0, 0x277A40, 0x277B00, 0x1BD72C0);
                                case 3:
                                    return CreateOffsets(0x6B16F0, 0x6B14B0, 0x00, 0x6C4030, 0x5C5BA0, 0x277A50, 0x277B10, 0x1BDB2C0);
                                case 4:
                                    return CreateOffsets(0x6B34B0, 0x6B3270, 0x00, 0x6C5E00, 0x5C7660, 0x2780C0, 0x278180, 0x1BDF2C0);
                                case 5:
                                    return CreateOffsets(0x6B35E0, 0x6B33A0, 0x00, 0x6C5F30, 0x5C7770, 0x2780D0, 0x278190, 0x1BE02C0);
                                case 6:
                                    return CreateOffsets(0x6B3610, 0x6B33D0, 0x00, 0x6C5F60, 0x5C77A0, 0x278100, 0x2781C0, 0x1BDF2C0);
                                case 7:
                                    return CreateOffsets(0x6B3630, 0x6B33F0, 0x00, 0x6C5F80, 0x5C77C0, 0x278100, 0x2781C0, 0x1BDE2C0);
                                case 8:
                                    return CreateOffsets(0x6B6DD0, 0x6B6B90, 0x00, 0x6C9720, 0x5CAE50, 0x278160, 0x278220, 0x1BDF340);
                                case 9:
                                    return CreateOffsets(0x6B6DF0, 0x6B6BB0, 0x00, 0x6C9740, 0x5CAE70, 0x278170, 0x278230, 0x1BE0340);
                                case 10:
                                    return CreateOffsets(0x6B7560, 0x6B7320, 0x00, 0x6C9EB0, 0x5CB5D0, 0x278110, 0x2781D0, 0x1BE9380);
                                case 11:
                                    return CreateOffsets(0x6B7870, 0x6B7630, 0x00, 0x6CA1C0, 0x5CB8B0, 0x2783D0, 0x278490, 0x1BEA340);
                                case 12:
                                    return CreateOffsets(0x6B7A00, 0x6B77C0, 0x00, 0x6CA350, 0x5CB950, 0x278470, 0x278530, 0x1BEA340);
                                case 13:
                                    return CreateOffsets(0x6B6760, 0x6B6520, 0x00, 0x6C90B0, 0x5CA500, 0x2784D0, 0x278590, 0x1BEB340);
                                case 14:
                                    return CreateOffsets(0x6B67D0, 0x6B6590, 0x00, 0x6C9120, 0x5CA510, 0x2784D0, 0x278590, 0x1BEB340);
                                case 15:
                                    return CreateOffsets(0x6B6770, 0x6B6530, 0x00, 0x6C90C0, 0x5CA510, 0x2784D0, 0x278590, 0x1BEB340);
                                case 16:
                                    return CreateOffsets(0x6B61F0, 0x6B5FB0, 0x00, 0x6C8B40, 0x5CA570, 0x278530, 0x2785F0, 0x1BEC340);
                                case 17:
                                    return CreateOffsets(0x6BC4F0, 0x6BC2B0, 0x00, 0x6CE8B0, 0x5CDE70, 0x2793F0, 0x2794B0, 0x1C01340);
                                case 18:
                                    return CreateOffsets(0x6BB2F0, 0x6BB0B0, 0x00, 0x6CD6B0, 0x5CCD20, 0x278EB0, 0x278F70, 0x1BFE340);
                                case 19:
                                    return CreateOffsets(0x6BB2F0, 0x6BB0B0, 0x00, 0x6CD6B0, 0x5CCD20, 0x278EB0, 0x278F70, 0x1BFE340);
                                case 20:
                                    return CreateOffsets(0x6BADD0, 0x6BAB90, 0x00, 0x6CD190, 0x5CC7F0, 0x278F80, 0x279040, 0x1C00340);
                                case 22:
                                    return CreateOffsets(0x6BABE0, 0x00, 0x5C1FA0, 0x6CCD90, 0x5CC830, 0x278F80, 0x279040, 0x1BFF340);
                            }
                            break;
                    }
                    break;
            }

            return null;
        }

        private static Dictionary<string, long> CreateOffsets(
            long monoManagerAwakeFromLoadOffset,
            long monoManagerIsAssemblyCreatedOffset,
            long isFileCreatedOffset,
            long scriptingManagerDeconstructorOffset,
            long convertSeparatorsToPlatformOffset,
            long mallocInternalOffset,
            long freeAllocInternalOffset,
            long scriptingAssembliesOffset)
        {
            return new Dictionary<string, long>
            {
                [nameof(Config.MonoManagerAwakeFromLoadOffset)] = monoManagerAwakeFromLoadOffset,
                [nameof(Config.MonoManagerIsAssemblyCreatedOffset)] = monoManagerIsAssemblyCreatedOffset,
                [nameof(Config.IsFileCreatedOffset)] = isFileCreatedOffset,
                [nameof(Config.ScriptingManagerDeconstructorOffset)] = scriptingManagerDeconstructorOffset,
                [nameof(Config.ConvertSeparatorsToPlatformOffset)] = convertSeparatorsToPlatformOffset,
                [nameof(Config.MallocInternalOffset)] = mallocInternalOffset,
                [nameof(Config.FreeAllocInternalOffset)] = freeAllocInternalOffset,
                [nameof(Config.ScriptingAssembliesOffset)] = scriptingAssembliesOffset,
            };
        }
    }
}

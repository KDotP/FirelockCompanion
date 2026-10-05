using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FirelockCompanion
{
    public static class Utilities
    {
        public static string ToVerbose(string conciseStats)
        {
            if (string.IsNullOrWhiteSpace(conciseStats))
                return string.Empty;

            string[] tokens = conciseStats.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            List<string> formattedTokens = new List<string>();

            foreach (string rawToken in tokens)
            {
                string token = rawToken.Trim();
                if (string.IsNullOrEmpty(token)) continue;

                // Unit type check
                if (token.Equals("Inf (S)", StringComparison.OrdinalIgnoreCase))
                {
                    formattedTokens.Add("Infantry Squad");
                    continue;
                }
                if (token.Equals("Inf", StringComparison.OrdinalIgnoreCase))
                {
                    formattedTokens.Add("Infantry");
                    continue;
                }
                if (token.Equals("Vec (W)", StringComparison.OrdinalIgnoreCase))
                {
                    formattedTokens.Add("Vehicle (Wheeled)");
                    continue;
                }
                if (token.Equals("Vec (C)", StringComparison.OrdinalIgnoreCase))
                {
                    formattedTokens.Add("Vehicle (Carriage)");
                    continue;
                }
                if (token.Equals("Vec (H)", StringComparison.OrdinalIgnoreCase))
                {
                    formattedTokens.Add("Vehicle (Hovercraft)");
                    continue;
                }
                if (token.Equals("Vec (S)", StringComparison.OrdinalIgnoreCase))
                {
                    formattedTokens.Add("Vehicle (Strider)");
                    continue;
                }
                if (token.Equals("Vec", StringComparison.OrdinalIgnoreCase))
                {
                    formattedTokens.Add("Vehicle (Tracked)");
                    continue;
                }
                if (token.Equals("Air", StringComparison.OrdinalIgnoreCase))
                {
                    formattedTokens.Add("Helicopter");
                    continue;
                }

                // Stat type checks
                // Height: H1, HX
                if (Regex.IsMatch(token, @"^H(\d+|X)$", RegexOptions.IgnoreCase))
                {
                    string val = Regex.Match(token, @"^H(\d+|X)$", RegexOptions.IgnoreCase).Groups[1].Value;
                    formattedTokens.Add($"Height: {val.ToUpper()}");
                    continue;
                }

                // Spotting: S0", S24", S32"
                if (Regex.IsMatch(token, @"^S(\d+"")$", RegexOptions.IgnoreCase))
                {
                    string val = Regex.Match(token, @"^S(\d+"")$", RegexOptions.IgnoreCase).Groups[1].Value;
                    formattedTokens.Add($"Spotting: {val}");
                    continue;
                }

                // Movement: M6", M6", MX
                if (Regex.IsMatch(token, @"^M(\d+""|X"")$", RegexOptions.IgnoreCase))
                {
                    string val = Regex.Match(token, @"^M(\d+""|X"")$", RegexOptions.IgnoreCase).Groups[1].Value;
                    formattedTokens.Add($"Movement: {val.ToUpper()}");
                    continue;
                }

                // Quality: Q4, Q4+, QX
                if (Regex.IsMatch(token, @"^Q(\d+\+?|X)$", RegexOptions.IgnoreCase))
                {
                    string val = Regex.Match(token, @"^Q(\d+\+?|X)$", RegexOptions.IgnoreCase).Groups[1].Value;
                    formattedTokens.Add($"Quality: {val.ToUpper()}");
                    continue;
                }

                // Command: C2, CX
                if (Regex.IsMatch(token, @"^C(\d+|X)$", RegexOptions.IgnoreCase))
                {
                    string val = Regex.Match(token, @"^C(\d+|X)$", RegexOptions.IgnoreCase).Groups[1].Value;
                    formattedTokens.Add($"Command: {val.ToUpper()}");
                    continue;
                }

                // Evasion: E3, E2
                if (Regex.IsMatch(token, @"^E(\d+|X)$", RegexOptions.IgnoreCase))
                {
                    string val = Regex.Match(token, @"^E(\d+|X)$", RegexOptions.IgnoreCase).Groups[1].Value;
                    formattedTokens.Add($"Evasion: {val.ToUpper()}");
                    continue;
                }

                // 3 Value Directional Toughness: T1/1-/1-, TX/X/X
                // Probably should have done it like this before, but REGEX is too silly for me
                Match t3Match = Regex.Match(token, @"^T([^/]+)/([^/]+)/([^/]+)$", RegexOptions.IgnoreCase);
                if (t3Match.Success)
                {
                    string front = t3Match.Groups[1].Value.ToUpper();
                    string side = t3Match.Groups[2].Value.ToUpper();
                    string back = t3Match.Groups[3].Value.ToUpper();
                    formattedTokens.Add($"Toughness: {front} (Front) / {side} (Side) / {back} (Back)");
                    continue;
                }

                // Single-Value Toughness: T3, TX
                Match t1Match = Regex.Match(token, @"^T([^/]+)$", RegexOptions.IgnoreCase);
                if (t1Match.Success)
                {
                    string val = t1Match.Groups[1].Value.ToUpper();
                    formattedTokens.Add($"Toughness: {val}");
                    continue;
                }

                // Fallback: If token isn't standard, retain original string
                formattedTokens.Add(token);
            }

            return string.Join("\n", formattedTokens);
        }

        // AI programmed section. I'm expecting some errors here, but I think I've fixed the obvious issues.
        public static string ToVerboseWeaponStats(string conciseStats, IEnumerable<string> keywords = null)
        {
            if (string.IsNullOrWhiteSpace(conciseStats))
                return string.Empty;

            bool hasShapedCharge = keywords != null &&
                keywords.Any(k => !string.IsNullOrEmpty(k) &&
                    k.IndexOf("Shaped Charge", StringComparison.OrdinalIgnoreCase) >= 0);

            string[] tokens = conciseStats.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            List<string> formattedTokens = new List<string>();

            foreach (string rawToken in tokens)
            {
                string token = rawToken.Trim();
                if (string.IsNullOrEmpty(token)) continue;

                // R8" -> 8" Range. This also handles variable/split ranges such as R12-60" and R12-∞".
                Match rangeMatch = Regex.Match(token, @"^R(.+)$", RegexOptions.IgnoreCase);
                if (rangeMatch.Success && rangeMatch.Groups[1].Value.EndsWith("\""))
                {
                    formattedTokens.Add($"{rangeMatch.Groups[1].Value} Range");
                    continue;
                }

                // A4+/5+ -> 4+ Stationary Accuracy, 5+ Moving Accuracy.
                Match accuracyPairMatch = Regex.Match(token, @"^A([^/]+)/([^/]+)$", RegexOptions.IgnoreCase);
                if (accuracyPairMatch.Success)
                {
                    string stationary = accuracyPairMatch.Groups[1].Value;
                    string moving = accuracyPairMatch.Groups[2].Value;
                    formattedTokens.Add($"{stationary} Stationary Accuracy");
                    formattedTokens.Add($"{moving} Moving Accuracy");
                    continue;
                }

                // A3+ / A++ -> 3+ Accuracy / ++ Accuracy. Preserve special A* unchanged.
                Match accuracySingleMatch = Regex.Match(token, @"^A([0-9+*xX-]+)$", RegexOptions.IgnoreCase);
                if (accuracySingleMatch.Success && !token.Equals("A*", StringComparison.OrdinalIgnoreCase))
                {
                    string accuracy = accuracySingleMatch.Groups[1].Value;
                    formattedTokens.Add($"{accuracy} Accuracy");
                    continue;
                }

                // S1/1+ -> Beyond/Below Half Range, unless Shaped Charge changes the meaning of the pair.
                Match strengthPairMatch = Regex.Match(token, @"^S([^/]+)/([^/]+)$", RegexOptions.IgnoreCase);
                if (strengthPairMatch.Success)
                {
                    string first = strengthPairMatch.Groups[1].Value;
                    string second = strengthPairMatch.Groups[2].Value;

                    if (hasShapedCharge)
                    {
                        formattedTokens.Add($"{first} Strength Against Armor");
                        formattedTokens.Add($"{second} Strength Against Other");
                    }
                    else
                    {
                        formattedTokens.Add($"{first} Strength Beyond Half Range");
                        formattedTokens.Add($"{second} Strength Below Half Range");
                    }

                    continue;
                }

                // Single Strength values are unambiguous as a single stat. Preserve bracketed variable Strength.
                Match strengthSingleMatch = Regex.Match(token, @"^S(?!\[)(.+)$", RegexOptions.IgnoreCase);
                if (strengthSingleMatch.Success)
                {
                    string strength = strengthSingleMatch.Groups[1].Value;
                    formattedTokens.Add($"{strength} Strength");
                    continue;
                }

                // D1 -> 1 Die.
                Match diceMatch = Regex.Match(token, @"^D(.+)$", RegexOptions.IgnoreCase);
                if (diceMatch.Success)
                {
                    string dice = diceMatch.Groups[1].Value;
                    formattedTokens.Add($"{dice} Die");
                    continue;
                }

                // Targeting types, Ammo counts, A*, S[D3]/S[D6], and any future unknown tokens remain unchanged.
                formattedTokens.Add(token);
            }

            return string.Join(", ", formattedTokens);
        }
    }
}

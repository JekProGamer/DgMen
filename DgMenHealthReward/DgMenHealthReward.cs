using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;

namespace DgMenHealthReward;

public sealed class DgMenHealthReward : BasePlugin
{
    public override string ModuleName => "DgMen Health Reward";
    public override string ModuleVersion => "1.0.0";
    public override string ModuleAuthor => "DgMen";
    public override string ModuleDescription => "Rewards health for kills, with a hard 100 HP cap.";

    private const int HeadshotReward = 100;
    private const int BodyKillReward = 25;
    private const int MaximumHealth = 100;

    public override void Load(bool hotReload)
    {
        RegisterEventHandler<EventPlayerDeath>(OnPlayerDeath);
    }

    private HookResult OnPlayerDeath(EventPlayerDeath gameEvent, GameEventInfo info)
    {
        var attacker = gameEvent.Attacker;
        var victim = gameEvent.Userid;

        // Ignore world damage, invalid attackers and suicide/team self-kills.
        if (attacker == null || !attacker.IsValid || victim == null ||
            attacker.UserId == victim.UserId)
        {
            return HookResult.Continue;
        }

        var pawn = attacker.PlayerPawn.Value;
        if (pawn == null || !pawn.IsValid || pawn.Health <= 0)
        {
            return HookResult.Continue;
        }

        var reward = gameEvent.Headshot ? HeadshotReward : BodyKillReward;

        // The reward can never raise the player's health above 100.
        pawn.MaxHealth = MaximumHealth;
        pawn.Health = Math.Min(MaximumHealth, pawn.Health + reward);

        Utilities.SetStateChanged(pawn, "CBaseEntity", "m_iHealth");
        Utilities.SetStateChanged(pawn, "CBaseEntity", "m_iMaxHealth");

        return HookResult.Continue;
    }
}

using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Discord
{
    /// <summary>
    ///     Represents a container for a series of overwrite permissions.
    /// </summary>
    [DebuggerDisplay(@"{DebuggerDisplay,nq}")]
    public struct OverwritePermissions
    {
        /// <summary>
        ///     Gets a blank <see cref="OverwritePermissions" /> that inherits all permissions.
        /// </summary>
        public static OverwritePermissions InheritAll { get; } = new OverwritePermissions();
        /// <summary>
        ///     Gets a <see cref="OverwritePermissions" /> that grants all permissions for the given channel.
        /// </summary>
        /// <exception cref="ArgumentException">Unknown channel type.</exception>
        public static OverwritePermissions AllowAll(IChannel channel)
            => new OverwritePermissions(ChannelPermissions.All(channel).RawValue, 0);
        /// <summary>
        ///     Gets a <see cref="OverwritePermissions" /> that denies all permissions for the given channel.
        /// </summary>
        /// <exception cref="ArgumentException">Unknown channel type.</exception>
        public static OverwritePermissions DenyAll(IChannel channel)
            => new OverwritePermissions(0, ChannelPermissions.All(channel).RawValue);

        /// <summary>
        ///     Gets a packed value representing all the allowed permissions in this <see cref="OverwritePermissions"/>.
        /// </summary>
        public ulong AllowValue { get; }
        /// <summary>
        ///     Gets a packed value representing all the denied permissions in this <see cref="OverwritePermissions"/>.
        /// </summary>
        public ulong DenyValue { get; }

        /// <summary> If Allowed, a user may create invites. </summary>
        public PermValue CreateInstantInvite => Permissions.GetValue(AllowValue, DenyValue, ChannelPermission.CreateInstantInvite);
        /// <summary> If Allowed, a user may create, delete and modify this channel. </summary>
        public PermValue ManageChannel => Permissions.GetValue(AllowValue, DenyValue, ChannelPermission.ManageChannels);
        /// <summary> If Allowed, a user may add reactions. </summary>
        public PermValue AddReactions => Permissions.GetValue(AllowValue, DenyValue, ChannelPermission.AddReactions);
        /// <summary> If Allowed, a user may join channels. </summary>
        public PermValue ViewChannel => Permissions.GetValue(AllowValue, DenyValue, ChannelPermission.ViewChannel);
        /// <summary> If Allowed, a user may send messages. </summary>
        public PermValue SendMessages => Permissions.GetValue(AllowValue, DenyValue, ChannelPermission.SendMessages);
        /// <summary> If Allowed, a user may send text-to-speech messages. </summary>
        public PermValue SendTTSMessages => Permissions.GetValue(AllowValue, DenyValue, ChannelPermission.SendTTSMessages);
        /// <summary> If Allowed, a user may delete messages. </summary>
        public PermValue ManageMessages => Permissions.GetValue(AllowValue, DenyValue, ChannelPermission.ManageMessages);
        /// <summary> If Allowed, Discord will auto-embed links sent by this user. </summary>
        public PermValue EmbedLinks => Permissions.GetValue(AllowValue, DenyValue, ChannelPermission.EmbedLinks);
        /// <summary> If Allowed, a user may send files. </summary>
        public PermValue AttachFiles => Permissions.GetValue(AllowValue, DenyValue, ChannelPermission.AttachFiles);
        /// <summary> If Allowed, a user may read previous messages. </summary>
        public PermValue ReadMessageHistory => Permissions.GetValue(AllowValue, DenyValue, ChannelPermission.ReadMessageHistory);
        /// <summary> If Allowed, a user may mention @everyone. </summary>
        public PermValue MentionEveryone => Permissions.GetValue(AllowValue, DenyValue, ChannelPermission.MentionEveryone);
        /// <summary> If Allowed, a user may use custom emoji from other guilds. </summary>
        public PermValue UseExternalEmojis => Permissions.GetValue(AllowValue, DenyValue, ChannelPermission.UseExternalEmojis);

        /// <summary> If Allowed, a user may connect to a voice channel. </summary>
        public PermValue Connect => Permissions.GetValue(AllowValue, DenyValue, ChannelPermission.Connect);
        /// <summary> If Allowed, a user may speak in a voice channel. </summary>
        public PermValue Speak => Permissions.GetValue(AllowValue, DenyValue, ChannelPermission.Speak);
        /// <summary> If Allowed, a user may mute users. </summary>
        public PermValue MuteMembers => Permissions.GetValue(AllowValue, DenyValue, ChannelPermission.MuteMembers);
        /// <summary> If Allowed, a user may deafen users. </summary>
        public PermValue DeafenMembers => Permissions.GetValue(AllowValue, DenyValue, ChannelPermission.DeafenMembers);
        /// <summary> If Allowed, a user may move other users between voice channels. </summary>
        public PermValue MoveMembers => Permissions.GetValue(AllowValue, DenyValue, ChannelPermission.MoveMembers);
        /// <summary> If Allowed, a user may use voice-activity-detection rather than push-to-talk. </summary>
        public PermValue UseVAD => Permissions.GetValue(AllowValue, DenyValue, ChannelPermission.UseVAD);
        /// <summary> If Allowed, a user may use priority speaker in a voice channel. </summary>
        public PermValue PrioritySpeaker => Permissions.GetValue(AllowValue, DenyValue, ChannelPermission.PrioritySpeaker);
        /// <summary> If Allowed, a user may go live in a voice channel. </summary>
        public PermValue Stream => Permissions.GetValue(AllowValue, DenyValue, ChannelPermission.Stream);

        /// <summary> If Allowed, a user may adjust role permissions. This also implicitly grants all other permissions. </summary>
        public PermValue ManageRoles => Permissions.GetValue(AllowValue, DenyValue, ChannelPermission.ManageRoles);
        /// <summary> If True, a user may edit the webhooks for this channel. </summary>
        public PermValue ManageWebhooks => Permissions.GetValue(AllowValue, DenyValue, ChannelPermission.ManageWebhooks);
        /// <summary> If <see langword="true" />, a user may use slash commands in this guild. </summary>
        public PermValue UseApplicationCommands => Permissions.GetValue(AllowValue, DenyValue, ChannelPermission.UseApplicationCommands);
        /// <summary> If <see langword="true" />, a user may request to speak in stage channels. </summary>
        public PermValue RequestToSpeak => Permissions.GetValue(AllowValue, DenyValue, ChannelPermission.RequestToSpeak);
        /// <summary> If <see langword="true" />, a user may manage threads in this guild. </summary>
        public PermValue ManageThreads => Permissions.GetValue(AllowValue, DenyValue, ChannelPermission.ManageThreads);
        /// <summary> If <see langword="true" />, a user may create public threads in this guild. </summary>
        public PermValue CreatePublicThreads => Permissions.GetValue(AllowValue, DenyValue, ChannelPermission.CreatePublicThreads);
        /// <summary> If <see langword="true" />, a user may create private threads in this guild. </summary>
        public PermValue CreatePrivateThreads => Permissions.GetValue(AllowValue, DenyValue, ChannelPermission.CreatePrivateThreads);
        /// <summary> If <see langword="true" />, a user may use external stickers in this guild. </summary>
        public PermValue UseExternalStickers => Permissions.GetValue(AllowValue, DenyValue, ChannelPermission.UseExternalStickers);
        /// <summary> If <see langword="true" />, a user may send messages in threads in this guild. </summary>
        public PermValue SendMessagesInThreads => Permissions.GetValue(AllowValue, DenyValue, ChannelPermission.SendMessagesInThreads);
        /// <summary> If <see langword="true" />, a user launch application activities in voice channels in this guild. </summary>
        public PermValue StartEmbeddedActivities => Permissions.GetValue(AllowValue, DenyValue, ChannelPermission.StartEmbeddedActivities);
        /// <summary> If <see langword="true"/>, a user can use soundboard in a voice channel.</summary>
        public PermValue UseSoundboard => Permissions.GetValue(AllowValue, DenyValue, ChannelPermission.UseSoundboard);
        /// <summary> If <see langword="true"/>, a user can edit and cancel events in this channel.</summary>
        public PermValue CreateEvents => Permissions.GetValue(AllowValue, DenyValue, ChannelPermission.CreateEvents);
        /// <summary> If <see langword="true"/>, a user can send voice messages in this channel.</summary>
        public PermValue SendVoiceMessages => Permissions.GetValue(AllowValue, DenyValue, ChannelPermission.SendVoiceMessages);
        /// <summary> If <see langword="true"/>, a user can use the Clyde AI bot in this channel.</summary>
        public PermValue UseClydeAI => Permissions.GetValue(AllowValue, DenyValue, ChannelPermission.UseClydeAI);
        /// <summary> If <see langword="true"/>, a user can set the status of a voice channel.</summary>
        public PermValue SetVoiceChannelStatus => Permissions.GetValue(AllowValue, DenyValue, ChannelPermission.SetVoiceChannelStatus);
        /// <summary> If <see langword="true"/>, a user can send polls.</summary>
        public PermValue SendPolls => Permissions.GetValue(AllowValue, DenyValue, ChannelPermission.SendPolls);
        /// <summary> If <see langword="true"/>, a user-installed application can send public responses.</summary>
        public PermValue UserExternalApps => Permissions.GetValue(AllowValue, DenyValue, ChannelPermission.UseExternalApps);
        /// <summary> If <see langword="true"/>, a user-installed application can send public responses.</summary>
        public PermValue UseExternalSounds => Permissions.GetValue(AllowValue, DenyValue, ChannelPermission.UseExternalSounds);

        /// <summary> Creates a new OverwritePermissions with the provided allow and deny packed values. </summary>
        public OverwritePermissions(ulong allowValue, ulong denyValue)
        {
            AllowValue = allowValue;
            DenyValue = denyValue;
        }

        /// <summary> Creates a new OverwritePermissions with the provided allow and deny packed values after converting to ulong. </summary>
        public OverwritePermissions(string allowValue, string denyValue)
        {
            AllowValue = ulong.Parse(allowValue);
            DenyValue = ulong.Parse(denyValue);
        }

        private OverwritePermissions(ulong allowValue, ulong denyValue,
            PermValue? createInstantInvite = null,
            PermValue? manageChannel = null,
            PermValue? addReactions = null,
            PermValue? viewChannel = null,
            PermValue? sendMessages = null,
            PermValue? sendTTSMessages = null,
            PermValue? manageMessages = null,
            PermValue? embedLinks = null,
            PermValue? attachFiles = null,
            PermValue? readMessageHistory = null,
            PermValue? mentionEveryone = null,
            PermValue? useExternalEmojis = null,
            PermValue? connect = null,
            PermValue? speak = null,
            PermValue? muteMembers = null,
            PermValue? deafenMembers = null,
            PermValue? moveMembers = null,
            PermValue? useVoiceActivation = null,
            PermValue? manageRoles = null,
            PermValue? manageWebhooks = null,
            PermValue? prioritySpeaker = null,
            PermValue? stream = null,
            PermValue? useSlashCommands = null,
            PermValue? useApplicationCommands = null,
            PermValue? requestToSpeak = null,
            PermValue? manageThreads = null,
            PermValue? createPublicThreads = null,
            PermValue? createPrivateThreads = null,
            PermValue? usePublicThreads = null,
            PermValue? usePrivateThreads = null,
            PermValue? useExternalStickers = null,
            PermValue? sendMessagesInThreads = null,
            PermValue? startEmbeddedActivities = null,
            PermValue? useSoundboard = null,
            PermValue? createEvents = null,
            PermValue? sendVoiceMessages = null,
            PermValue? useClydeAI = null,
            PermValue? setVoiceChannelStatus = null,
            PermValue? sendPolls = null,
            PermValue? useExternalApps = null,
            PermValue? useExternalSounds = null)
        {
            Permissions.SetValue(ref allowValue, ref denyValue, createInstantInvite, ChannelPermission.CreateInstantInvite);
            Permissions.SetValue(ref allowValue, ref denyValue, manageChannel, ChannelPermission.ManageChannels);
            Permissions.SetValue(ref allowValue, ref denyValue, addReactions, ChannelPermission.AddReactions);
            Permissions.SetValue(ref allowValue, ref denyValue, viewChannel, ChannelPermission.ViewChannel);
            Permissions.SetValue(ref allowValue, ref denyValue, sendMessages, ChannelPermission.SendMessages);
            Permissions.SetValue(ref allowValue, ref denyValue, sendTTSMessages, ChannelPermission.SendTTSMessages);
            Permissions.SetValue(ref allowValue, ref denyValue, manageMessages, ChannelPermission.ManageMessages);
            Permissions.SetValue(ref allowValue, ref denyValue, embedLinks, ChannelPermission.EmbedLinks);
            Permissions.SetValue(ref allowValue, ref denyValue, attachFiles, ChannelPermission.AttachFiles);
            Permissions.SetValue(ref allowValue, ref denyValue, readMessageHistory, ChannelPermission.ReadMessageHistory);
            Permissions.SetValue(ref allowValue, ref denyValue, mentionEveryone, ChannelPermission.MentionEveryone);
            Permissions.SetValue(ref allowValue, ref denyValue, useExternalEmojis, ChannelPermission.UseExternalEmojis);
            Permissions.SetValue(ref allowValue, ref denyValue, connect, ChannelPermission.Connect);
            Permissions.SetValue(ref allowValue, ref denyValue, speak, ChannelPermission.Speak);
            Permissions.SetValue(ref allowValue, ref denyValue, muteMembers, ChannelPermission.MuteMembers);
            Permissions.SetValue(ref allowValue, ref denyValue, deafenMembers, ChannelPermission.DeafenMembers);
            Permissions.SetValue(ref allowValue, ref denyValue, moveMembers, ChannelPermission.MoveMembers);
            Permissions.SetValue(ref allowValue, ref denyValue, useVoiceActivation, ChannelPermission.UseVAD);
            Permissions.SetValue(ref allowValue, ref denyValue, prioritySpeaker, ChannelPermission.PrioritySpeaker);
            Permissions.SetValue(ref allowValue, ref denyValue, stream, ChannelPermission.Stream);
            Permissions.SetValue(ref allowValue, ref denyValue, manageRoles, ChannelPermission.ManageRoles);
            Permissions.SetValue(ref allowValue, ref denyValue, manageWebhooks, ChannelPermission.ManageWebhooks);
            Permissions.SetValue(ref allowValue, ref denyValue, useApplicationCommands, ChannelPermission.UseApplicationCommands);
            Permissions.SetValue(ref allowValue, ref denyValue, requestToSpeak, ChannelPermission.RequestToSpeak);
            Permissions.SetValue(ref allowValue, ref denyValue, manageThreads, ChannelPermission.ManageThreads);
            Permissions.SetValue(ref allowValue, ref denyValue, createPublicThreads, ChannelPermission.CreatePublicThreads);
            Permissions.SetValue(ref allowValue, ref denyValue, createPrivateThreads, ChannelPermission.CreatePrivateThreads);
            Permissions.SetValue(ref allowValue, ref denyValue, useExternalStickers, ChannelPermission.UseExternalStickers);
            Permissions.SetValue(ref allowValue, ref denyValue, sendMessagesInThreads, ChannelPermission.SendMessagesInThreads);
            Permissions.SetValue(ref allowValue, ref denyValue, startEmbeddedActivities, ChannelPermission.StartEmbeddedActivities);
            Permissions.SetValue(ref allowValue, ref denyValue, useSoundboard, ChannelPermission.UseSoundboard);
            Permissions.SetValue(ref allowValue, ref denyValue, createEvents, ChannelPermission.CreateEvents);
            Permissions.SetValue(ref allowValue, ref denyValue, sendVoiceMessages, ChannelPermission.SendVoiceMessages);
            Permissions.SetValue(ref allowValue, ref denyValue, useClydeAI, ChannelPermission.UseClydeAI);
            Permissions.SetValue(ref allowValue, ref denyValue, setVoiceChannelStatus, ChannelPermission.SetVoiceChannelStatus);
            Permissions.SetValue(ref allowValue, ref denyValue, sendPolls, ChannelPermission.SendPolls);
            Permissions.SetValue(ref allowValue, ref denyValue, useExternalApps, ChannelPermission.UseExternalApps);
            Permissions.SetValue(ref allowValue, ref denyValue, useExternalSounds, ChannelPermission.UseExternalSounds);

            AllowValue = allowValue;
            DenyValue = denyValue;
        }

        /// <summary>
        ///     Initializes a new <see cref="ChannelPermissions"/> struct with the provided permissions.
        /// </summary>
        public OverwritePermissions(
            PermValue createInstantInvite = PermValue.Inherit,
            PermValue manageChannel = PermValue.Inherit,
            PermValue addReactions = PermValue.Inherit,
            PermValue viewChannel = PermValue.Inherit,
            PermValue sendMessages = PermValue.Inherit,
            PermValue sendTTSMessages = PermValue.Inherit,
            PermValue manageMessages = PermValue.Inherit,
            PermValue embedLinks = PermValue.Inherit,
            PermValue attachFiles = PermValue.Inherit,
            PermValue readMessageHistory = PermValue.Inherit,
            PermValue mentionEveryone = PermValue.Inherit,
            PermValue useExternalEmojis = PermValue.Inherit,
            PermValue connect = PermValue.Inherit,
            PermValue speak = PermValue.Inherit,
            PermValue muteMembers = PermValue.Inherit,
            PermValue deafenMembers = PermValue.Inherit,
            PermValue moveMembers = PermValue.Inherit,
            PermValue useVoiceActivation = PermValue.Inherit,
            PermValue manageRoles = PermValue.Inherit,
            PermValue manageWebhooks = PermValue.Inherit,
            PermValue prioritySpeaker = PermValue.Inherit,
            PermValue stream = PermValue.Inherit,
            PermValue useSlashCommands = PermValue.Inherit,
            PermValue useApplicationCommands = PermValue.Inherit,
            PermValue requestToSpeak = PermValue.Inherit,
            PermValue manageThreads = PermValue.Inherit,
            PermValue createPublicThreads = PermValue.Inherit,
            PermValue createPrivateThreads = PermValue.Inherit,
            PermValue usePublicThreads = PermValue.Inherit,
            PermValue usePrivateThreads = PermValue.Inherit,
            PermValue useExternalStickers = PermValue.Inherit,
            PermValue sendMessagesInThreads = PermValue.Inherit,
            PermValue startEmbeddedActivities = PermValue.Inherit,
            PermValue useSoundboard = PermValue.Inherit,
            PermValue createEvents = PermValue.Inherit,
            PermValue sendVoiceMessages = PermValue.Inherit,
            PermValue useClydeAI = PermValue.Inherit,
            PermValue setVoiceChannelStatus = PermValue.Inherit,
            PermValue sendPolls = PermValue.Inherit,
            PermValue useExternalApps = PermValue.Inherit,
            PermValue useExternalSounds = PermValue.Inherit)
            : this(0, 0, createInstantInvite, manageChannel, addReactions, viewChannel, sendMessages, sendTTSMessages, manageMessages,
                  embedLinks, attachFiles, readMessageHistory, mentionEveryone, useExternalEmojis, connect, speak, muteMembers, deafenMembers,
                  moveMembers, useVoiceActivation, manageRoles, manageWebhooks, prioritySpeaker, stream, useSlashCommands, useApplicationCommands,
                  requestToSpeak, manageThreads, createPublicThreads, createPrivateThreads, usePublicThreads, usePrivateThreads, useExternalStickers,
                  sendMessagesInThreads, startEmbeddedActivities, useSoundboard, createEvents, sendVoiceMessages, useClydeAI, setVoiceChannelStatus,
                  sendPolls, useExternalApps, useExternalSounds)
        { }

        /// <summary>
        ///     Initializes a new <see cref="OverwritePermissions" /> from the current one, changing the provided
        ///     non-null permissions.
        /// </summary>
        public OverwritePermissions Modify(
            PermValue? createInstantInvite = null,
            PermValue? manageChannel = null,
            PermValue? addReactions = null,
            PermValue? viewChannel = null,
            PermValue? sendMessages = null,
            PermValue? sendTTSMessages = null,
            PermValue? manageMessages = null,
            PermValue? embedLinks = null,
            PermValue? attachFiles = null,
            PermValue? readMessageHistory = null,
            PermValue? mentionEveryone = null,
            PermValue? useExternalEmojis = null,
            PermValue? connect = null,
            PermValue? speak = null,
            PermValue? muteMembers = null,
            PermValue? deafenMembers = null,
            PermValue? moveMembers = null,
            PermValue? useVoiceActivation = null,
            PermValue? manageRoles = null,
            PermValue? manageWebhooks = null,
            PermValue? prioritySpeaker = null,
            PermValue? stream = null,
            PermValue? useSlashCommands = null,
            PermValue? useApplicationCommands = null,
            PermValue? requestToSpeak = null,
            PermValue? manageThreads = null,
            PermValue? createPublicThreads = null,
            PermValue? createPrivateThreads = null,
            PermValue? usePublicThreads = null,
            PermValue? usePrivateThreads = null,
            PermValue? useExternalStickers = null,
            PermValue? sendMessagesInThreads = null,
            PermValue? startEmbeddedActivities = null,
            PermValue? useSoundboard = null,
            PermValue? createEvents = null,
            PermValue? sendVoiceMessages = null,
            PermValue? useClydeAI = null,
            PermValue? setVoiceChannelStatus = null,
            PermValue? sendPolls = null,
            PermValue? useExternalApps = null,
            PermValue? useExternalSounds = null)
            => new OverwritePermissions(AllowValue, DenyValue, createInstantInvite, manageChannel, addReactions, viewChannel, sendMessages, sendTTSMessages, manageMessages,
                embedLinks, attachFiles, readMessageHistory, mentionEveryone, useExternalEmojis, connect, speak, muteMembers, deafenMembers,
                moveMembers, useVoiceActivation, manageRoles, manageWebhooks, prioritySpeaker, stream, useSlashCommands, useApplicationCommands,
                  requestToSpeak, manageThreads, createPublicThreads, createPrivateThreads, usePublicThreads, usePrivateThreads, useExternalStickers,
                  sendMessagesInThreads, startEmbeddedActivities, useSoundboard, createEvents, sendVoiceMessages, useClydeAI, setVoiceChannelStatus,
                sendPolls, useExternalApps, useExternalSounds);

        /// <summary>
        ///     Creates a <see cref="List{T}"/> of all the <see cref="ChannelPermission"/> values that are allowed.
        /// </summary>
        /// <returns>A <see cref="List{T}"/> of all allowed <see cref="ChannelPermission"/> flags. If none, the list will be empty.</returns>
        public List<ChannelPermission> ToAllowList()
        {
            var perms = new List<ChannelPermission>();
            for (byte i = 0; i < Permissions.MaxBits; i++)
            {
                // first operand must be long or ulong to shift >31 bits
                ulong flag = ((ulong)1 << i);
                if ((AllowValue & flag) != 0)
                    perms.Add((ChannelPermission)flag);
            }
            return perms;
        }

        /// <summary>
        ///     Creates a <see cref="List{T}"/> of all the <see cref="ChannelPermission"/> values that are denied.
        /// </summary>
        /// <returns>A <see cref="List{T}"/> of all denied <see cref="ChannelPermission"/> flags. If none, the list will be empty.</returns>
        public List<ChannelPermission> ToDenyList()
        {
            var perms = new List<ChannelPermission>();
            for (byte i = 0; i < Permissions.MaxBits; i++)
            {
                ulong flag = ((ulong)1 << i);
                if ((DenyValue & flag) != 0)
                    perms.Add((ChannelPermission)flag);
            }
            return perms;
        }

        public override string ToString() => $"Allow {AllowValue}, Deny {DenyValue}";
        private string DebuggerDisplay =>
            $"Allow {string.Join(", ", ToAllowList())}, " +
            $"Deny {string.Join(", ", ToDenyList())}";
    }
}

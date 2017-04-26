﻿//using Denifia.Stardew.SendLetters.Common.Domain;
//using Denifia.Stardew.SendLetters.Domain;
//using Denifia.Stardew.SendLetters.Menus;
//using Denifia.Stardew.SendLetters.Models;
//using StardewValley;
//using StardewValley.Menus;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Denifia.Stardew.SendLetters.Services
//{
//    public class MailboxService : IMailboxService
//    {
//        private readonly IPlayerService _playerService;
//        private readonly IMessageService _messageService;
//        private const string _playerMailKey = "playerMail";
//        private const string _playerMailTitle = "Player Mail";
//        private const string _leaveSelectionKeyAndValue = "(Leave)";
//        private const string _messageFormat = "Hey there!^^  I thought you might like this... Take care! ^    -{0} %item object {1} {2} %%";

//        public MailboxService(IPlayerService playerService, IMessageService messageService)
//        {
//            _playerService = playerService;
//            _messageService = messageService;

//            //ModEvents.MessageCrafted += AfterMessageCrafted;
//            ModEvents.CheckMailbox += OnCheckMailbox;
//        }

//        public void ShowLetter(Message message)
//        {
//            if (Game1.mailbox == null || !Game1.mailbox.Any()) return;
//            if (Game1.mailbox.Peek() == _playerMailKey)
//            {
//                Game1.activeClickableMenu = (IClickableMenu)new LetterViewerMenu(message.Text, _playerMailTitle);
//                if (Game1.mailbox.Any())
//                {
//                    Game1.mailbox.Dequeue();
//                }
//                ModEvents.RaiseMessageReadEvent(message);
//            }
//        }

//        public void PostLetters(int count)
//        {
//            while (Game1.mailbox.Any() && Game1.mailbox.Peek() == _playerMailKey)
//            {
//                Game1.mailbox.Dequeue();
//            }

//            for (int i = 0; i < count; i++)
//            {
//                Game1.mailbox.Enqueue(_playerMailKey);
//            }
//        }

//        public void ShowFriendSelecter()
//        {
//            if (Game1.activeClickableMenu != null) return;

//            List<Response> responseList = new List<Response>();
//            foreach (var friend in _playerService.CurrentPlayer.Friends)
//            {
//                responseList.Add(new Response(friend.Id, string.Format("{0} ({1} Farm)", friend.Name, friend.FarmName)));
//            }
//            responseList.Add(new Response(_leaveSelectionKeyAndValue, _leaveSelectionKeyAndValue));
//            Game1.currentLocation.createQuestionDialogue("Select Friend:", responseList.ToArray(), new GameLocation.afterQuestionBehavior(this.SelectItem), (NPC)null);
//            Game1.player.Halt();
//        }

//        private void SelectItem(Farmer who, string answer)
//        {
//            if (!answer.Equals(_leaveSelectionKeyAndValue))
//            {
//                var items = new List<Item>();
//                items.Add(null);
//                Game1.activeClickableMenu = (IClickableMenu)new ComposeLetter(answer, items, 1, 1, null, highlightGiftable);
//                //Game1.activeClickableMenu = (IClickableMenu)new ComposeLetter(answer, items, 1, 1, new ComposeLetter.behaviorOnItemChange(onLetterChange));
//            }
//        }

//        private bool onLetterChange(Item i, int position, Item old, ComposeLetter container, bool onRemoval)
//        {
//            if (!onRemoval)
//            {
//                if (i.Stack > 1 || i.Stack == 1 && old != null && (old.Stack == 1 && i.canStackWith(old)))
//                {
//                    if (old != null && i != null && old.canStackWith(i))
//                    {
//                        container.ItemsToGrabMenu.actualInventory[position].Stack = 1;
//                        container.heldItem = old;
//                        return false;
//                    }
//                    if (old != null)
//                    {
//                        Utility.addItemToInventory(old, position, container.ItemsToGrabMenu.actualInventory, (ItemGrabMenu.behaviorOnItemSelect)null);
//                        container.heldItem = i;
//                        return false;
//                    }
//                    //int num = i.Stack - 1;
//                    //Item one = i.getOne();
//                    //one.Stack = num;
//                    container.heldItem = i;
//                    //i.Stack = 1;
//                }
//            }
//            else if (old != null && old.Stack > 1 && !old.Equals((object)i))
//                return false;
//            //if (Game1.IsMultiplayer)
//            //{
//            //    if (onRemoval && old == null)
//            //        MultiplayerUtility.sendMessageToEveryone(3, position.ToString() + " null null", Game1.player.uniqueMultiplayerID);
//            //    else
//            //        MultiplayerUtility.sendMessageToEveryone(3, position.ToString() + " " + (object)(i as Object).ParentSheetIndex + " " + (object)(i as Object).quality, Game1.player.uniqueMultiplayerID);
//            //}
//            //else
//            //this.addItemToLetter(!onRemoval || old != null && !old.Equals((object)i) ? i : (Item)null, position, true);
//            return true;
//        }

//        public bool highlightGiftable(Item i)
//        {
//            return i.canBeGivenAsGift();
//        }

//        private void OnCheckMailbox(object sender, EventArgs e)
//        {
//            var message = _messageService.GetFirstMessage(_playerService.CurrentPlayer.Id);
//            if (message != null && !(Game1.mailbox == null || !Game1.mailbox.Any()))

//            {
//                ShowLetter(message);
//            }
//            else
//            {
//                if (!Game1.mailbox.Any())
//                {
//                    ShowFriendSelecter();
//                }
//            }
//        }

//        private void AfterMessageCrafted(string toPlayerId, Item item)
//        {
//            if (item == null) return; 

//            var currentPlayer = _playerService.CurrentPlayer;
//            var messageText = string.Format(_messageFormat, currentPlayer.Name, item.parentSheetIndex, item.getStack());
//            var newMessage = new CreateMessageModel
//            {
//                FromPlayerId = currentPlayer.Id,
//                ToPlayerId = toPlayerId,
//                Text = messageText
//            };

//            _messageService.CreateMessage(newMessage);
//        }
//    }
//}
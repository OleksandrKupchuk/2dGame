using System;

public class EventManager {
    public static event Action<Item> OnItemDressed;

    public static void OnItemDressedHandler(Item item) {
        OnItemDressed.Invoke(item);
    }

    public static void OnItemDressedHandler() {
        OnItemDressed.Invoke(null);
    }

    public static event Action<Item> TakeAwayItem;

    public static void TakeAwayItemEventHandler(Item item) {
        TakeAwayItem.Invoke(item);
    }

    public static event Action<Item> UseItem;

    public static void UseItemEventHandler(Item item) {
        UseItem.Invoke(item);
    }

    public static event Action<Item> ActionItemOver;

    public static void ActionItemOverEventHandler(Item item) {
        ActionItemOver.Invoke(item);
    }

    public static event Action<Item> BuyItem;

    public static void BuyItemEventHandler(Item item) {
        BuyItem.Invoke(item);
    }

    public static event Action<Item> OnItemDrop;
    public static void OnItemDropHandler(Item item) {
        OnItemDrop?.Invoke(item);
    }
}

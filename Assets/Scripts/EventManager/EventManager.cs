using System;

public class EventManager {
    public static event Action<Item> OnItemDressed;

    public static void OnItemDressedHandler(Item item) {
        OnItemDressed.Invoke(item);
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

    public static event Action OnHealthChanged;

    public static void OnHealthChangedHandler() {
        OnHealthChanged.Invoke();
    }

    public static event Action OnDead;
    public static void OnDeadHandler() {
        OnDead.Invoke();
    }

    public static event Action OnHit;
    public static void OnHitHandler() {
        OnHit.Invoke();
    }
<<<<<<< HEAD
=======

    public static event Action<Item> OnItemDrop;
    public static void OnItemDropHandler(Item item) {
        OnItemDrop?.Invoke(item);
    }
>>>>>>> d84f61f28eae6158f881ef62479366b326da010c
}

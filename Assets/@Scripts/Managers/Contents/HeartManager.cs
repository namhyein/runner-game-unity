using System;
using UnityEngine;

public class HeartManager
{
  private const int USE_HEART = 5;
  private const int MAX_HEART = 50;
  private const int CHARGE_SECONDS = 300;

  public Action<int> OnHeartChanged;
  public Action<float> OnChargeTimerChanged;

  private int _heart;
  public int Heart
  {
    get { return _heart; }
    set
    {
      _heart = value;
      OnHeartChanged?.Invoke(value);
    }
  }

  private float _chargeTimer;
  public float ChargeTimer
  {
    get { return _chargeTimer; }
    set
    {
      _chargeTimer = value;
      OnChargeTimerChanged?.Invoke(value);
    }
  }

  public void Init()
  {
    Heart = GetHeartCount();
    _chargeTimer = CHARGE_SECONDS;
  }

  public void Update()
  {
    if (Heart >= MAX_HEART) return;
    if (ChargeTimer - Time.deltaTime <= 0) ChargeHeart();
    else ChargeTimer -= Time.deltaTime;
  }

  public void OnDestroy()
  {
    SaveHeartCount();
  }

  public void ChargeHeart()
  {
    if (Heart >= MAX_HEART) return;

    Heart++;
    ChargeTimer = CHARGE_SECONDS;
  }

  public void ChargeHeart(int count)
  {
    Heart += count;
  }

  public void UseHeart()
  {
    if (Heart < USE_HEART) return;

    Heart -= USE_HEART;
  }

  private int GetHeartCount()
  {
    int savedHeart = PlayerPrefs.GetInt("Heart", 0);
    string savedTime = PlayerPrefs.GetString("HeartTime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

    if (savedHeart >= MAX_HEART) return savedHeart;

    DateTime.TryParse(savedTime, out DateTime heartTime);
    TimeSpan timeSpan = DateTime.Now - heartTime;
    int chargeCount = (int)(timeSpan.TotalSeconds / CHARGE_SECONDS);

    return Mathf.Min(MAX_HEART, savedHeart + chargeCount);
  }

  private void SaveHeartCount()
  {
    PlayerPrefs.SetInt("Heart", Heart);
    PlayerPrefs.SetString("HeartTime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
  }
}
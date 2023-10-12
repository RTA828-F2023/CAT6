﻿using System.Collections;
using UnityEngine;

public class CameraShaker : MonoBehaviour
{
    #region Singleton

    private static CameraShaker _cameraShakerInstance;

    public static CameraShaker Instance
    {
        get
        {
            if (_cameraShakerInstance == null) _cameraShakerInstance = FindObjectOfType<CameraShaker>();
            return _cameraShakerInstance;
        }
    }

    #endregion

    private float _shakeDuration;
    private float _shakeIntensity;
    private float _shakeDecreaseFactor;

    private Vector3 _originalPosition;

    #region Unity Event

    private void Start()
    {
        _originalPosition = new Vector3(0f, 0f, -10f);
    }

    private void Update()
    {
        Randomize();
    }

    #endregion

    private void Randomize()
    {
        // While shake duration is greater than 0, randomize position and decrease shake duration
        if (_shakeDuration > 0f)
        {
            transform.localPosition = _originalPosition + Random.insideUnitSphere * _shakeIntensity;
            _shakeDuration -= Time.unscaledDeltaTime * _shakeDecreaseFactor;
        }
        // When shake duration reaches 0, reset everything
        else if (_shakeDuration < 0f)
        {
            _shakeDuration = 0f;
            transform.localPosition = _originalPosition;
        }
    }

    #region Shake Methods

    public void Shake(CameraShakeMode cameraShakeMode, float delay = 0f)
    {
        StartCoroutine(ShakeCoroutine(cameraShakeMode, delay));
    }

    public void Shake(float duration, float intensity, float decreaseFactor, float delay = 0f)
    {
        StartCoroutine(ShakeCoroutine(duration, intensity, decreaseFactor, delay));
    }

    #endregion

    private IEnumerator ShakeCoroutine(CameraShakeMode cameraShakeMode, float delay)
    {
        yield return new WaitForSeconds(delay);

        _originalPosition = new Vector3(0f, 0f, -10f);

        switch (cameraShakeMode)
        {
            case CameraShakeMode.Nano:
                _shakeDuration = _shakeIntensity = 0.06f;
                break;

            case CameraShakeMode.Micro:
                _shakeDuration = _shakeIntensity = 0.08f;
                break;

            case CameraShakeMode.Light:
                _shakeDuration = _shakeIntensity = 0.1f;
                break;

            case CameraShakeMode.Normal:
                _shakeDuration = _shakeIntensity = 0.12f;
                break;

            case CameraShakeMode.Hard:
                _shakeDuration = _shakeIntensity = 0.14f;
                break;

            default:
                yield break;
        }

        _shakeDecreaseFactor = 2f;
    }

    private IEnumerator ShakeCoroutine(float duration, float intensity, float decreaseFactor, float delay)
    {
        yield return new WaitForSeconds(delay);

        // If screen shake disabled in menu then do nothing
        if (PlayerPrefs.GetInt("ScreenShake", 0) == 1) yield break;

        _originalPosition = new Vector3(0f, 0f, -10f);

        _shakeDuration = duration;
        _shakeIntensity = intensity;

        _shakeDecreaseFactor = decreaseFactor;
    }
}
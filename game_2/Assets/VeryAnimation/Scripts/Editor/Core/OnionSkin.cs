﻿using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Rendering;
using UnityEditor;
using UnityEditor.Animations;
using System;
using System.Linq;
using System.Collections.Generic;

namespace VeryAnimation
{
    public class OnionSkin
    {
        private VeryAnimationWindow vaw { get { return VeryAnimationWindow.instance; } }
        private VeryAnimation va { get { return VeryAnimation.instance; } }

        private class OnionSkinObject
        {
            public DummyObject dummyObject;

            public bool active { get { return dummyObject.gameObject.activeSelf; } set { dummyObject.gameObject.SetActive(value); } }

            public OnionSkinObject(GameObject go)
            {
                dummyObject = new DummyObject();
                dummyObject.Initialize(go);
                dummyObject.ChangeTransparent();
                dummyObject.gameObject.SetActive(false);
            }
            public void Release()
            {
                if (dummyObject != null)
                {
                    dummyObject.Release();
                    dummyObject = null;
                }
            }

            public void SetRenderQueue(int renderQueue)
            {
                dummyObject.SetTransparentRenderQueue(renderQueue);
            }

            public void SetColor(Color color)
            {
                dummyObject.SetColor(color);
            }
        }
        private Dictionary<int, OnionSkinObject> onionSkinObjects;

        public OnionSkin()
        {
            onionSkinObjects = new Dictionary<int, OnionSkinObject>();
        }
        public void Release()
        {
            foreach (var pair in onionSkinObjects)
            {
                pair.Value.Release();
            }
            onionSkinObjects.Clear();
        }

        public void Update()
        {
            if (!vaw.editorSettings.settingExtraOnionSkin)
            {
                Release();
                return;
            }
            var show = !va.uAw.GetPlaying() && !va.prefabMode;
            foreach (var pair in onionSkinObjects)
            {
                pair.Value.active = false;
            }
            if (!show) return;

            var lastFrame = va.GetLastFrame();

            if (vaw.editorSettings.settingExtraOnionSkinMode == EditorSettings.OnionSkinMode.Keyframes)
            {
                #region Keyframes
                float[] nextTimes = vaw.editorSettings.settingExtraOnionSkinNextCount > 0 ? new float[vaw.editorSettings.settingExtraOnionSkinNextCount] : null;
                float[] prevTimes = vaw.editorSettings.settingExtraOnionSkinPrevCount > 0 ? new float[vaw.editorSettings.settingExtraOnionSkinPrevCount] : null;
                va.uAw.GetNearKeyframeTimes(nextTimes, prevTimes);
                #region Next
                if (nextTimes != null)
                {
                    var frame = va.uAw.GetCurrentFrame();
                    for (int i = 0; i < vaw.editorSettings.settingExtraOnionSkinNextCount; i++)
                    {
                        if (Mathf.Approximately(va.currentTime, nextTimes[i])) break;
                        frame = va.uAw.TimeToFrameRound(nextTimes[i]);
                        if (frame < 0 || frame > lastFrame) break;
                        var oso = SetFrame((i + 1), va.GetFrameTime(frame));
                        var color = vaw.editorSettings.settingExtraOnionSkinNextColor;
                        var rate = vaw.editorSettings.settingExtraOnionSkinNextCount > 1 ? i / (float)(vaw.editorSettings.settingExtraOnionSkinNextCount - 1) : 0f;
                        color.a = Mathf.Lerp(color.a, vaw.editorSettings.settingExtraOnionSkinNextMinAlpha, rate);
                        oso.SetColor(color);
                    }
                }
                #endregion
                #region Prev
                if (prevTimes != null)
                {
                    var frame = va.uAw.GetCurrentFrame();
                    for (int i = 0; i < vaw.editorSettings.settingExtraOnionSkinPrevCount; i++)
                    {
                        if (Mathf.Approximately(va.currentTime, prevTimes[i])) break;
                        frame = va.uAw.TimeToFrameRound(prevTimes[i]);
                        if (frame < 0 || frame > lastFrame) break;
                        var oso = SetFrame(-(i + 1), va.GetFrameTime(frame));
                        var color = vaw.editorSettings.settingExtraOnionSkinPrevColor;
                        var rate = vaw.editorSettings.settingExtraOnionSkinPrevCount > 1 ? i / (float)(vaw.editorSettings.settingExtraOnionSkinPrevCount - 1) : 0f;
                        color.a = Mathf.Lerp(color.a, vaw.editorSettings.settingExtraOnionSkinPrevMinAlpha, rate);
                        oso.SetColor(color);
                    }
                }
                #endregion
                #endregion
            }
            else if (vaw.editorSettings.settingExtraOnionSkinMode == EditorSettings.OnionSkinMode.Frames)
            {
                #region Frames
                #region Next
                {
                    var frame = va.uAw.GetCurrentFrame();
                    for (int i = 0; i < vaw.editorSettings.settingExtraOnionSkinNextCount; i++)
                    {
                        frame += vaw.editorSettings.settingExtraOnionSkinFrameIncrement;
                        if (frame < 0 || frame > lastFrame) break;
                        var oso = SetFrame((i + 1), va.GetFrameTime(frame));
                        var color = vaw.editorSettings.settingExtraOnionSkinNextColor;
                        var rate = vaw.editorSettings.settingExtraOnionSkinNextCount > 1 ? i / (float)(vaw.editorSettings.settingExtraOnionSkinNextCount - 1) : 0f;
                        color.a = Mathf.Lerp(color.a, vaw.editorSettings.settingExtraOnionSkinNextMinAlpha, rate);
                        oso.SetColor(color);
                    }
                }
                #endregion
                #region Prev
                {
                    var frame = va.uAw.GetCurrentFrame();
                    for (int i = 0; i < vaw.editorSettings.settingExtraOnionSkinPrevCount; i++)
                    {
                        frame -= vaw.editorSettings.settingExtraOnionSkinFrameIncrement;
                        if (frame < 0 || frame > lastFrame) break;
                        var oso = SetFrame(-(i + 1), va.GetFrameTime(frame));
                        var color = vaw.editorSettings.settingExtraOnionSkinPrevColor;
                        var rate = vaw.editorSettings.settingExtraOnionSkinPrevCount > 1 ? i / (float)(vaw.editorSettings.settingExtraOnionSkinPrevCount - 1) : 0f;
                        color.a = Mathf.Lerp(color.a, vaw.editorSettings.settingExtraOnionSkinPrevMinAlpha, rate);
                        oso.SetColor(color);
                    }
                }
                #endregion
                #endregion
            }
        }

        private OnionSkinObject SetFrame(int frame, float time)
        {
            OnionSkinObject oso;
            if(!onionSkinObjects.TryGetValue(frame, out oso))
            {
                oso = new OnionSkinObject(va.editGameObject);
                {
                    const int StartOffset = 20;
                    var offset = Math.Abs(frame * 2) + (frame > 0 ? 0 : 1);
                    oso.SetRenderQueue((int)RenderQueue.GeometryLast - (StartOffset - offset));
                }
                onionSkinObjects.Add(frame, oso);
            }

            oso.active = true;
            oso.dummyObject.SetSource();
            oso.dummyObject.UpdateState();
            oso.dummyObject.SampleAnimation(va.currentClip, time);

            return oso;
        }
    }
}

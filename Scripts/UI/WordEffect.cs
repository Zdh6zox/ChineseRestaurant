using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WordEffectType
{
    TypeWriter,
    InstantDisplayAfterDuration,
    Vibration,
    Picture,
    RichText
}

public class WordEffectFactory
{
    static List<WordEffect> CreateWordEffects(string line, Word ownerWord)
    {

        List<WordEffect> createdWordEffect = new List<WordEffect>();
        //用|来分割关键字段
        List<string> phrases = new List<string>(line.Split( new char[] { '|' },StringSplitOptions.RemoveEmptyEntries));

        foreach(string phrase in phrases)
        {
            if(phrase.Contains("<"))
            {
                char[] phraseCharArr = phrase.ToCharArray();

                for(int i = 0;i<phraseCharArr.Length;++i)
                {
                    char ch = phraseCharArr[i];
                    if(i == 0 && ch != '<')
                    {
                        throw new Exception(string.Format("Wrong line {0}", phrase));
                    }

                    if(i == 1)
                    {
                        switch (ch)
                        {
                            case 'T':
                                TypeWriterEffectData data = new TypeWriterEffectData(phrase);
                                TypeWriterEffect typeWriterEffect = CreateWordEffect<TypeWriterEffect,TypeWriterEffectData>(data);

                                createdWordEffect.Add(typeWriterEffect);
                                break;
                        }

                    }
                }
                foreach (char ch in phraseCharArr)
                {

                }
                

                if (phraseCharArr[0] != '<')
                {

                }
                char typeC = phraseCharArr[1];
            }
            else
            {
                //default we use TypeWriterEffect
                TypeWriterEffectData data = new TypeWriterEffectData(phrase);
                TypeWriterEffect typeWriterEffect = CreateWordEffect<TypeWriterEffect, TypeWriterEffectData>(data);

                createdWordEffect.Add(typeWriterEffect);
            }
        }

        return createdWordEffect;
    }

    static T CreateWordEffect<T,U>(U effectData) where T:WordEffect where U : WordEffectData
    {
        if(typeof(T).Equals(typeof(TypeWriterEffect)))
        {
            TypeWriterEffect effect = new TypeWriterEffect();
            effect.ConstructWordEffect(effectData);
            return (T)(object)effect;
        }
        else if(typeof(T).Equals(typeof(InstantDisplayAfterDurationEffect)))
        {
            InstantDisplayAfterDurationEffect effect = new InstantDisplayAfterDurationEffect();
            effect.ConstructWordEffect(effectData);
            return (T)(object)effect;
        }
        else if(typeof(T).Equals(typeof(VibrationEffect)))
        {
            VibrationEffect effect = new VibrationEffect();
            effect.ConstructWordEffect(effectData);
            return (T)(object)effect;
        }
        else
        {
            throw new Exception("try to create unimplemented word effect");
        }
    }
}

public abstract class WordEffect
{
    public Word _OwnerWord;
    public string _Line;
    public abstract void ConstructWordEffect<T>(T effectData);
    public abstract string UpdateWordEffect();
}

public abstract class WordEffectData
{
    public WordEffectType _type;
    public float _totalTime;
    public string _words;

    private WordEffectData() { }

    public WordEffectData(string line) { _words = line; }
}

public class TypeWriterEffectData : WordEffectData
{
    public static float DEFAULT_INTERVAL = 0.2f;
    private float _interval;

    public TypeWriterEffectData(string line, float charInterval = 0.2f) : base(line)
    {
        _interval = charInterval;
    }
}

public class TypeWriterEffect : WordEffect
{
    public override void ConstructWordEffect<T>(T effectData)
    {
        TypeWriterEffectData data = effectData as TypeWriterEffectData;
        if(data == null)
        {
            throw new Exception(string.Format("try to create word effect with wrong data"));
        }

    }

    public override string UpdateWordEffect()
    {
        throw new System.NotImplementedException();
    }
}

public class InstantDisplayAfterDurationEffectData : WordEffectData
{

    public InstantDisplayAfterDurationEffectData(string line) : base(line)
    {

    }
}


public class InstantDisplayAfterDurationEffect : WordEffect
{
    public static float DEFAULT_DURATION = 2.0f;

    public override void ConstructWordEffect<T>(T effectData)
    {
        InstantDisplayAfterDurationEffectData data = effectData as InstantDisplayAfterDurationEffectData;
        if (data == null)
        {
            throw new Exception(string.Format("try to create word effect with wrong data"));
        }


    }

    public override string UpdateWordEffect()
    {
        throw new System.NotImplementedException();
    }
}

public class VibrationEffectData : WordEffectData
{
    public VibrationEffectData(string line) : base(line)
    {

    }
}

public class VibrationEffect : WordEffect
{
    public override void ConstructWordEffect<T>(T effectData)
    {
        VibrationEffectData data = effectData as VibrationEffectData;
        if (data == null)
        {
            throw new Exception(string.Format("try to create word effect with wrong data"));
        }

    }

    public override string UpdateWordEffect()
    {
        throw new System.NotImplementedException();
    }
}

public class PictureEffectData : WordEffectData
{
    public PictureEffectData(string line) : base(line)
    {

    }
}

public class PictureEffect : WordEffect
{
    public override void ConstructWordEffect<T>(T effectData)
    {
        throw new NotImplementedException();
    }

    public override string UpdateWordEffect()
    {
        throw new NotImplementedException();
    }
}

public class RichTextEffectData : WordEffectData
{
    public RichTextEffectData(string line) : base(line)
    {

    }
}

public class RichTextEffect : WordEffect
{
    public override void ConstructWordEffect<T>(T effectData)
    {
        throw new NotImplementedException();
    }

    public override string UpdateWordEffect()
    {
        throw new NotImplementedException();
    }
}









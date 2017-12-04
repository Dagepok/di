﻿using System.Collections.Generic;
using System.IO;
using System.Linq;
using SharpNL.POSTag;
using NHunspell;
using SharpNL.Extensions;

namespace TagsCloudVisualization.Words_Preporation.Words_Checker
{
    public class PartOfSpeechChecker : AbstractWordChecker
    {
        private readonly POSTaggerME posTagger;
        private readonly string[] suitablePos;

        public PartOfSpeechChecker(Settings.Settings settings) : base(settings)
        {
            suitablePos = settings.SuitablePos != null ? new[] {settings.SuitablePos} : new[] {"nn", "vb","rb", "jj"};
            POSModel posModel;
            using (var modelFile = new FileStream("en-pos-maxent.bin", FileMode.Open))
                posModel = new POSModel(modelFile);
            posTagger = new POSTaggerME(posModel);
        }

        public override List<string> GetSuitableWords(IEnumerable<string> words)
        {
            using (var hunspell = new Hunspell("en_us.aff", "en_us.dic"))
            {
                var result = new List<string>();
                foreach (var word in words)
                {
                    var stem = hunspell.Stem(word).FirstOrDefault() ?? word;
                    var pos = posTagger.Tag(new[] { stem })[0].ToLowerInvariant();
                    if (stem.Length > 3 && suitablePos.Contains(pos))
                        result.Add(stem);
                }
                return result;



                // .Where(x => x.Length > 3 && suitablePos.Contains(posTagger.Tag(new[] { x })[0][0].ToString()))
                //.ToList();

            }
        }
    }
}
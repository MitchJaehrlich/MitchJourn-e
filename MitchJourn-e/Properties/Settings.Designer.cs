﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MitchJourn_e.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "17.5.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute(".25")]
        public string ImagePromptWeight {
            get {
                return ((string)(this["ImagePromptWeight"]));
            }
            set {
                this["ImagePromptWeight"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("1")]
        public string Iter {
            get {
                return ((string)(this["Iter"]));
            }
            set {
                this["Iter"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("512")]
        public string Width {
            get {
                return ((string)(this["Width"]));
            }
            set {
                this["Width"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("512")]
        public string Height {
            get {
                return ((string)(this["Height"]));
            }
            set {
                this["Height"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("25")]
        public string Steps {
            get {
                return ((string)(this["Steps"]));
            }
            set {
                this["Steps"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("1336")]
        public string Seed {
            get {
                return ((string)(this["Seed"]));
            }
            set {
                this["Seed"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("7.5")]
        public string Scale {
            get {
                return ((string)(this["Scale"]));
            }
            set {
                this["Scale"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("C:\\invokeai\\")]
        public string MainPath {
            get {
                return ((string)(this["MainPath"]));
            }
            set {
                this["MainPath"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute(".venv\\scripts\\txt2img.py")]
        public string TextToImagePath {
            get {
                return ((string)(this["TextToImagePath"]));
            }
            set {
                this["TextToImagePath"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute(".venv\\scripts\\img2img.py")]
        public string ImageToImagePath {
            get {
                return ((string)(this["ImageToImagePath"]));
            }
            set {
                this["ImageToImagePath"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("configs\\stable-diffusion\\v1-inference.yaml")]
        public string ConfigPath {
            get {
                return ((string)(this["ConfigPath"]));
            }
            set {
                this["ConfigPath"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("models\\ldm\\stable-diffusion-v1\\model.ckpt")]
        public string ModelPath {
            get {
                return ((string)(this["ModelPath"]));
            }
            set {
                this["ModelPath"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("1")]
        public string SlowMode {
            get {
                return ((string)(this["SlowMode"]));
            }
            set {
                this["SlowMode"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute(".5")]
        public string gfpganScale {
            get {
                return ((string)(this["gfpganScale"]));
            }
            set {
                this["gfpganScale"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("2")]
        public string gfpganUprezScale {
            get {
                return ((string)(this["gfpganUprezScale"]));
            }
            set {
                this["gfpganUprezScale"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("512")]
        public string gfpganBgTileSize {
            get {
                return ((string)(this["gfpganBgTileSize"]));
            }
            set {
                this["gfpganBgTileSize"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("k_dpmpp_2_a")]
        public string SamplerType {
            get {
                return ((string)(this["SamplerType"]));
            }
            set {
                this["SamplerType"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("1")]
        public string UseFullPrecision {
            get {
                return ((string)(this["UseFullPrecision"]));
            }
            set {
                this["UseFullPrecision"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("512:512")]
        public string AspectRatio11 {
            get {
                return ((string)(this["AspectRatio11"]));
            }
            set {
                this["AspectRatio11"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("864:576")]
        public string AspectRatio32 {
            get {
                return ((string)(this["AspectRatio32"]));
            }
            set {
                this["AspectRatio32"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("960:544")]
        public string AspectRatio169 {
            get {
                return ((string)(this["AspectRatio169"]));
            }
            set {
                this["AspectRatio169"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("576:864")]
        public string AspectRatio23 {
            get {
                return ((string)(this["AspectRatio23"]));
            }
            set {
                this["AspectRatio23"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("544:960")]
        public string AspectRatio920 {
            get {
                return ((string)(this["AspectRatio920"]));
            }
            set {
                this["AspectRatio920"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("1")]
        public string EnableWelcomePrompt {
            get {
                return ((string)(this["EnableWelcomePrompt"]));
            }
            set {
                this["EnableWelcomePrompt"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("<?xml version=\"1.0\" encoding=\"utf-16\"?>\r\n<ArrayOfString xmlns:xsd=\"http://www.w3." +
            "org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\">\r\n  <s" +
            "tring>GPT3/Elaborate as an master painter=Using only nouns and adjectives, descr" +
            "ibe the painting as a master artist historian. An example result: incredibly det" +
            "ailed depiction high quality hyperdetailed masterpiece art atmosphere well balan" +
            "ced lighting carefully crafted composition. This is what the painting looks like" +
            ":</string>\r\n  <string>GPT3/Elaborate as a master photographer=Using only nouns a" +
            "nd adjectives, and describing technical details, elaborate on the description of" +
            " a photo from the perspective of a professional photographer. An example result:" +
            " crisp sharp photograph taken on nikon f/1.4 50mm 200iso 4k incredibly high qual" +
            "ity art atmosphere well balanced lighting beautiful bokeh. This is what the phot" +
            "o looks like:</string>\r\n  <string>GPT3/Text to prompt=Using only nouns and adjec" +
            "tives, reduce this group of words to only the most important details. Example re" +
            "sult: incredibly detailed high quality majestic graceful rich blue surreal shado" +
            "ws golden light. This is the group of words:</string>\r\n  <string>GPT3/Generate n" +
            "egative prompt=Given a set of words, return words that would never belong in the" +
            " same set. Example set of words: a cat wearing a blue hat on the sidewalk of dow" +
            "ntown vancouver Example result: troops searching the area, from police academy 2" +
            " (1985), gunma prefecture, concrete housing, nsfw, blur, wood block print, in af" +
            "rica, kodak tri - x 3 5 mm, black lace, american flag, football players, tokyoja" +
            "pan, ferrari 458, woodblock print Set of words:</string>\r\n  <string>Random/Rando" +
            "m Word=*random*</string>\r\n  <string>Artists/Greg Rutkowski (dark concept art)=by" +
            " greg rutkowski</string>\r\n  <string>Artists/Thoma Kinkades (Flowery Landscape)=b" +
            "y thomas kinkade</string>\r\n  <string>Artists/John Berkey (Detailed paintings)=ve" +
            "ry detailed painting by John Berkey</string>\r\n  <string>Artists/William-Adolphe " +
            "Bouguereau (Old style painting)=painting by William-Adolphe Bouguereau</string>\r" +
            "\n  <string>Artists/Claude Monet (Noisy painting)=painting by Claude Monet</strin" +
            "g>\r\n  <string>Artists/Ernest Zacharevic (Wall mural)=wall mural by Ernest Zachar" +
            "evic</string>\r\n  <string>Artists/Stephan Martiniere (sci-fi concept art)=by Step" +
            "han Martiniere</string>\r\n  <string>Artists/Carl Barks (Cartoonist)=cartoon by Ca" +
            "rl Barks</string>\r\n  <string>Artists/Peter Gric (Creepy drawings)=Peter Gric pri" +
            "nt</string>\r\n  <string>Artists/Sung Choi (Sci-Fi fantacy concept art)=trending o" +
            "n artstation by sung choi</string>\r\n  <string>Artists/Ilya Kuvshinov (Retro anim" +
            "e portraits)=by ilya kuvshinov</string>\r\n  <string>Artists/Andreas Rocha (Fantac" +
            "y landscapes)=by andreas rocha</string>\r\n  <string>Artists/Lois Van Baarle (Pait" +
            "ed portraits)=by lois van baarle</string>\r\n  <string>Artists/Rossdraws (Digital " +
            "art portraits)=by rossdraws</string>\r\n  <string>Artists/Rembrandt (Baroque paint" +
            "ings)=by Rembrandt</string>\r\n  <string>Artists/Marc Simonetti (Detailed dark dig" +
            "ital landscapes)=highly detailed by marc simonetti</string>\r\n  <string>Artists/L" +
            "uis Royo (Creepy detailed portraits)=by Luis Royo</string>\r\n  <string>Artists/Be" +
            "ksiński (Creepy abstract paintings)=by beksiński</string>\r\n  <string>Artists/Hie" +
            "ronymus Bosch (Renaissance paintings)=by hieronymus bosch</string>\r\n  <string>St" +
            "yles/Midjourney1=Splash art, light dust, magnificent, details, sharp focus, intr" +
            "icate, beautiful, triadic contrast colors, trending artstation, pixiv, digital a" +
            "rt</string>\r\n  <string>Styles/Midjourney2=Splash art light dust trending Profess" +
            "ional majestic oil painting of establishing shot by Ed Blinkey and Atey Ghailan " +
            "and Studio Ghibli and Jeremy Mann and Greg Manchess and Antonio Moro volumetric " +
            "lighting, dramatic lighting</string>\r\n  <string>Styles/Photo=f/1.4 50mm 200iso 4" +
            "k</string>\r\n  <string>Styles/iPhone photo=iPhone photo</string>\r\n  <string>Style" +
            "s/Portrait Photography1=facial asymmetry, striking features, tack sharp, fine-ar" +
            "t photography, 180mm f/1.8 200iso</string>\r\n  <string>Styles/Stability Portrait=" +
            "ultrarealistic uhd faces, Kodak Ultramax 800, pexels, 85mm, casual pose, 35mm fi" +
            "lm roll photo, hard light, detailed skin texture, masterpiece, sharp focus, pret" +
            "ty, lovely, adorale, attractive, hasselblad, candid street podrait</string>\r\n  <" +
            "string>Styles/Landscape=octane render 4k unreal engine cryengine 200iso dynamic " +
            "range hdr wonderous awesome greg rutkowski sung choi thomas kinkade vincent van " +
            "gogh</string>\r\n  <string>Styles/Stability Landscape1=unsplash contest winner, br" +
            "eath-taking beautiful, warm shades of blue, video still</string>\r\n  <string>Styl" +
            "es/High Quality=hyperdetailed hd 4k 8k sharp focus highly detailed uhd</string>\r" +
            "\n  <string>Styles/Elegant Portrait=portrait photo headshot by mucha, sharp focus" +
            ", elegant, render, octane, detailed, award winning photography, masterpiece, rim" +
            " lit</string>\r\n  <string>Styles/Artistic Portrait=a vibrant professional studio " +
            "portrait photography casual, delightful, intricate, piercing eyes, nouveau, cura" +
            "ted collection, annie leibovitz, nikon, award winning, breathtaking, groundbreak" +
            "ing, superb, outstanding, lensculture portrait awards, photoshopped, dramatic li" +
            "ghting, 8k</string>\r\n  <string>Styles/Rendered Portrait=soft, octane render, unr" +
            "eal engine, photograph, realistic skin texture, photorealistic, hyper realism, h" +
            "ighly detailed, 85mm portrait photography, award winning, hard rim lighting phot" +
            "ography</string>\r\n  <string>Styles/Stary Portrait=a portrait with a luminous clo" +
            "thing, eyes shut, mouth closed, wind, sky, clouds, the moon, moonlight, stars, u" +
            "niverse, fireflies, butterflies, lights, lens flares effects, swirly bokeh, brus" +
            "h effect, In style of Yoji Shinkawa, Jackson Pollock, wojtek fus, by Makoto Shin" +
            "kai, concept art, celestial, amazing, astonishing, wonderful, beautiful, highly " +
            "detailed, centered</string>\r\n  <string>Styles/Photobashing=photobashing</string>" +
            "\r\n  <string>Styles/Algorithmic Art=algorithmic art</string>\r\n  <string>Styles/St" +
            "ars and Galaxies=composed of billions of stars, digital art</string>\r\n  <string>" +
            "Styles/Cartoon dramatic style=digital art, 2.5D style</string>\r\n  <string>Styles" +
            "/Made of smoke=made of very detailed curling wispy glowing multicolored smoke, d" +
            "igital art, volumetric, 3D render, Octane render</string>\r\n  <string>Styles/Deta" +
            "iled Particles=detailed particle, digital art</string>\r\n  <string>Styles/Mixed M" +
            "edia=mixed media</string>\r\n  <string>Styles/Magical world=magical world</string>" +
            "\r\n  <string>Styles/concept art=concept art splash art</string>\r\n  <string>Styles" +
            "/Stability Architecture=A Hyperrealistic photograph of German architectural buil" +
            "ding, lens flares, cinematic, hdri, matte painting, concept art, celestial, soft" +
            " render, highly detailed, cgsociety, octane render, architectural HD, HQ, 4k, 8k" +
            "</string>\r\n  <string>Cinematography/Cinematic lighting=cinematic lighting</strin" +
            "g>\r\n  <string>Cinematography/Low angle=photograph, taken from a low angle</strin" +
            "g>\r\n  <string>Cinematography/Over the shoulder=photograph, over-the-shoulder</st" +
            "ring>\r\n  <string>Cinematography/Drone footage=photograph taken from a drone</str" +
            "ing>\r\n  <string>Cinematography/Close up=close up</string>\r\n  <string>Cinematogra" +
            "phy/Wide angle=wide angle</string>\r\n  <string>Cinematography/Movie ccene=anamorp" +
            "hic lens film scene movie still f/2 800iso 35mm light dust haze</string>\r\n  <str" +
            "ing>Cinematography/Golden hour=golden hour</string>\r\n  <string>Cinematography/be" +
            "autiful lighting=beautiful lighting</string>\r\n  <string>Sources/artstation=artst" +
            "ation</string>\r\n  <string>Sources/instagram=instagram</string>\r\n  <string>Source" +
            "s/deviantart=deviantart</string>\r\n  <string>Sources/reddit=reddit</string>\r\n  <s" +
            "tring>Sources/shutterstock=shutterstock</string>\r\n  <string>Sources/tumblr=tumbl" +
            "r</string>\r\n  <string>Sources/cgsociety=cgsociety</string>\r\n  <string>Sources/fl" +
            "ickr=flickr</string>\r\n  <string>Sources/behance=behance</string>\r\n  <string>Sour" +
            "ces/dribble=dribble</string>\r\n  <string>Sources/pexels=pexels</string>\r\n  <strin" +
            "g>Sources/pinterest=pinterest</string>\r\n  <string>Sources/pixabay=pixabay</strin" +
            "g>\r\n  <string>Sources/pixiv=pixiv</string>\r\n  <string>Sources/polycount=polycoun" +
            "t</string>\r\n  <string>Effects/post processing=post processing</string>\r\n  <strin" +
            "g>Effects/cgi=cgi</string>\r\n  <string>Effects/chromatic aberration=chromatic abe" +
            "rration</string>\r\n  <string>Effects/anaglyph=anaglyph</string>\r\n  <string>Effect" +
            "s/cropped=cropped</string>\r\n  <string>Effects/glowing edges=glowing edges</strin" +
            "g>\r\n  <string>Effects/glow effect=glow effect</string>\r\n  <string>Effects/bokeh=" +
            "bokeh</string>\r\n  <string>Effects/dramatic lighting=dramatic lighting</string>\r\n" +
            "  <string>Effects/soft lighting=soft lighting</string>\r\n  <string>Effects/hard l" +
            "ighting=hard lighting</string>\r\n  <string>Effects/glamor shot=glamor shot</strin" +
            "g>\r\n  <string>Effects/colourful=colourful</string>\r\n  <string>Effects/compliment" +
            "ary-colours=complimentary-colours</string>\r\n  <string>Effects/dark mood=dark moo" +
            "d</string>\r\n  <string>Effects/multiverse=multiverse</string>\r\n  <string>Effects/" +
            "volumetric lighting=volumetric lighting</string>\r\n  <string>Effects/lumen global" +
            " illumination=lumen global illumination</string>\r\n  <string>Effects/octane rende" +
            "r=octane render</string>\r\n  <string>Effects/atmospheric=atmospheric</string>\r\n  " +
            "<string>Effects/technicolour=technicolour</string>\r\n  <string>Mediums/photo real" +
            "istic=photo realistic</string>\r\n  <string>Mediums/anime=anime manga</string>\r\n  " +
            "<string>Mediums/graphic novel=graphic novel</string>\r\n  <string>Mediums/fountain" +
            " pen=fountain pen</string>\r\n  <string>Mediums/pastel art=pastel art</string>\r\n  " +
            "<string>Mediums/fine art=fine art</string>\r\n  <string>Mediums/acrylic paint=acry" +
            "lic paint</string>\r\n  <string>Mediums/oil paint=oil paint</string>\r\n  <string>Me" +
            "diums/watercolour=watercolour</string>\r\n  <string>Mediums/digital art=digital ar" +
            "t</string>\r\n  <string>Mediums/magazine=magazine</string>\r\n  <string>Mediums/comi" +
            "c book=comic book</string>\r\n  <string>Mediums/pokemon card=pokemon card</string>" +
            "\r\n  <string>Mediums/puzzle=puzzle</string>\r\n  <string>Mediums/logo=logo</string>" +
            "\r\n  <string>Mediums/editorial photography=editorial photography</string>\r\n  <str" +
            "ing>Mediums/wildlife photography=wildlife photography</string>\r\n  <string>Negati" +
            "ve/Photo=cartoon anime art painting ugly</string>\r\n  <string>Negative/Portrait=b" +
            "ad anatomy bad proportions blurry cloned face deformed disfigured duplicate gros" +
            "s proportions long neck mutation mutilated morbid out of frame poorly drawn face" +
            "</string>\r\n  <string>Negative/Hands=poorly drawn hands extra arms extra fingers " +
            "extra limbs extra legs too many fingers fused fingers malformed limbs missing ar" +
            "ms missmg legs mutated hands</string>\r\n  <string>Negative/Safety=nsfw nude naked" +
            "</string>\r\n  <string>Negative/Landscape=portrait person people text signature wa" +
            "termark frame framed display</string>\r\n  <string>Negative/High Quality=lowres te" +
            "xt error cropped worst quality low quality normal quality jpeg artifacts signatu" +
            "re watermark username blurry artist name deformed disfigured poorly drawn out of" +
            " focus censorship amateur drawing bad art poor art messy drawing</string>\r\n  <st" +
            "ring>Negative/Stability Portrait=fox in a lab coat, extra limb, from scene from " +
            "twin peaks, brutalist futuristic interior, retro futurism, dramatic nautical sce" +
            "ne , ornate hospital room, crumbling masonry, pale blue armor, mechanical paw, l" +
            "aser guns, pulp sci fi, two deer wearing suits</string>\r\n  <string>Negative/Stab" +
            "ility Portrait2=colourful 3d crystals and gems, vintage 1950s stamp, full color " +
            "manga cover, kewpie, two girls, anime, fairytale illustration, chinese ribbon da" +
            "nce, children illustration, flower dress, illustration, silk shoes, classic chil" +
            "dren\'s illustrations, adorable and whimsical</string>\r\n  <string>Negative/Stabil" +
            "ity Portrait3=blender, cropped, lowres, poorly drawn face, out of frame, poorly " +
            "drawn hands, double, blurred, disfigured, deformed, repetitive, black and white<" +
            "/string>\r\n  <string>Negative/Stability Landscape=wearing victorian brass goggles" +
            ", alien, alien isolation, ink on paper, flash sheet, robot barkeep, black micron" +
            " pen illustration, black bandage on arms, mcbess, grid of eye shapes, moscow met" +
            "ro, b&amp;w, childish, steelpunk, holding a cigar, dark show room, dieselpunk, n" +
            "ecromancer, mouse face</string>\r\n  <string>Negative/Stability Digital Art=autumn" +
            " rain turkel, two finnish lapphundsv 2d lasercut earrings tribal dance, risograp" +
            "h, white orchids, egyptian sumerian features, large temples, childish</string>\r\n" +
            "  <string>Negative/Stability Digital Art2=tintype photograph, moth inspired dres" +
            "s, red on black, lace dress, deformed, 1970 film photography, very sexy woman wi" +
            "th black hair, fashion model portrait, alien faces, animal photography, disfigur" +
            "ed</string>\r\n  <string>Negative/Stability Sci-Fi Art=trees in foreground, ink pe" +
            "n sketch, on a velvet tablecloth, black ink on textured paper, long black straig" +
            "ht hair, pencil skit pink lips, sophisticated hands, noir, mystic winter landsca" +
            "pe, linocut print, sailboats, watercolor strokes, grass landscape, pink rose, vi" +
            "ntage 1950s stamp</string>\r\n  <string>Negative/Stability Sci-Fi Render=1980s flo" +
            "wer power hippy, impressionist watercolor, sitting at the beach, hot pink, germa" +
            "n expressionist woodcut, wearing in stocking, pen drawing, young woman, on a bra" +
            "nch, watercolour on paper, green and pink, wearing shades, in a garden, drinking" +
            " their hearts out, old</string>\r\n  <string>Negative/Stability Comic=lofi bioshoc" +
            "k steampunk portrait, 4k digital painting, tapir, female portrait with flowers, " +
            "metamorphosis complex 3d render, white petal, skyrim screenshot, fine art fashio" +
            "n photography, side portrait ofa girl, lotus flower, lost place photo, black and" +
            " white, text, title</string>\r\n  <string>Negative/Stability Comic2=I elderly gree" +
            "k goddess, 1900s photograph, long glowing ethereal hair, gorgeous face, brown ha" +
            "ir!, calotype, sheep wool, hammershoi, eyelashes, dreamy and ethereal, single pi" +
            "ne, nsfw, white lilies, flowers on hair</string>\r\n  <string>Negative/Stability F" +
            "antacy=troops searching the area, from police academy 2 (1985), gunma prefecture" +
            ", concrete housing, nsfw, blur, wood block print, in africa, kodak tri - x 3 5 m" +
            "m, black lace, american flag, football players, tokyojapan, ferrari 458, woodblo" +
            "ck print</string>\r\n  <string>Negative/Stability Cyberpunk=purple and red flowers" +
            ", wood block print, idyllic and fruitful land, standing on a ladder, postman pat" +
            ", gnome druid, botanic watercolors, aged paper, standing with a parasol, she has" +
            " a crown of dried flowers, wearing an apron, pink door, nature journal, theodor " +
            "kittelsen, harvest fall vibrancy, crop</string>\r\n  <string>Negative/Stability Ca" +
            "rs=blue sky and white clouds, gourds, 1880 photograph, troops searching the area" +
            ", b&amp;w, rolling green hills, aboriginal engraving, arge black hat, spiral lin" +
            "es, several cottages, with anchor man and woman, ink drawing, brown hair, wide g" +
            "reenways, black ink on white paper</string>\r\n  <string>Negative/Stability Textur" +
            "e=fox in a lab coat, extra limb, from scene from twin peaks, brutalist futuristi" +
            "c interior, retro futurism, dramatic nautical scene, ornate hospital room, crumb" +
            "ling masonry, pale blue armor, mechanical paw, laser guns, pulp sci fi, two deer" +
            " wearing suits</string>\r\n  <string>Negative/Stability Food=warframe hound art, b" +
            "lack ink on white paper, art nouveau ink illustration, huge feathery wings, hold" +
            "ing a tattered magical book, anubis, lots of furniture, clockwork steampunk, low" +
            " quality, childish, mecha suit, blue moonlight, many mechflowers, fancy clouds, " +
            "crazy hacker girl, blue flames, solarpunk, undersea temple, in a lavender field " +
            "in france : -5</string>\r\n  <string>Negative/Stability Food2=fantasy dungeon, bea" +
            "utiful woman pot-trait, on stilts, tribal dance, 3d high definition, cell shaded" +
            " cartoon, dozens of jeweled necklaces, disco lights, in full military garb, long" +
            " neck, rainbow aura crystals, full moon lighting, patterned visionary art, laser" +
            " guns, mystic winter landscape, feathered arrows, mirror selfie</string>\r\n  <str" +
            "ing>Negative/Stability Character=dramatic space battle, engraving from 1700s, sp" +
            "inosaurus, illegible rosicrucian symbols, medieval woodcut, elegant evening gown" +
            "s!, battle of 1453, black ink on white paper, ornamental arrows, croatian coastl" +
            "ine, lush vegetation, arabian beauty, 1990s 1992 sega genesis box att : -5</stri" +
            "ng>\r\n  <string>Negative/Stability Character2=pen and ink doodles, blowing hair, " +
            "in the foreground a small town, white frame, soft blues and greens, capybara, lo" +
            "ng flowing hair, polish poster an, books and flowers, fairytale illustration, cu" +
            "te and funny, white stockings, cropped, out of frame : -5</string>\r\n</ArrayOfStr" +
            "ing>")]
        public global::System.Collections.Specialized.StringCollection PromptHelperPresets {
            get {
                return ((global::System.Collections.Specialized.StringCollection)(this["PromptHelperPresets"]));
            }
            set {
                this["PromptHelperPresets"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Welcome,Test")]
        public string SortList {
            get {
                return ((string)(this["SortList"]));
            }
            set {
                this["SortList"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool EnableSortList {
            get {
                return ((bool)(this["EnableSortList"]));
            }
            set {
                this["EnableSortList"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0")]
        public string SaveProgress {
            get {
                return ((string)(this["SaveProgress"]));
            }
            set {
                this["SaveProgress"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0")]
        public string SafetyChecker {
            get {
                return ((string)(this["SafetyChecker"]));
            }
            set {
                this["SafetyChecker"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("C:\\invokeai\\outputs")]
        public string OutputPath {
            get {
                return ((string)(this["OutputPath"]));
            }
            set {
                this["OutputPath"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string OpenAIAPIKey {
            get {
                return ((string)(this["OpenAIAPIKey"]));
            }
            set {
                this["OpenAIAPIKey"] = value;
            }
        }
    }
}

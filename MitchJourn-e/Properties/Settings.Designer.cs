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
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "17.3.0.0")]
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
        [global::System.Configuration.DefaultSettingValueAttribute("50")]
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
        [global::System.Configuration.DefaultSettingValueAttribute("1337")]
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
        [global::System.Configuration.DefaultSettingValueAttribute("C:\\InvokeAI-main")]
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
        [global::System.Configuration.DefaultSettingValueAttribute("scripts\\txt2img.py")]
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
        [global::System.Configuration.DefaultSettingValueAttribute("scripts\\img2img.py")]
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
        [global::System.Configuration.DefaultSettingValueAttribute("k_euler_a")]
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
        [global::System.Configuration.DefaultSettingValueAttribute("576:384")]
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
        [global::System.Configuration.DefaultSettingValueAttribute("640:384")]
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
        [global::System.Configuration.DefaultSettingValueAttribute("384:576")]
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
        [global::System.Configuration.DefaultSettingValueAttribute("256:576")]
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
            "tring>Artists/Greg Rutkowski (dark concept art)=by greg rutkowski</string>\r\n  <s" +
            "tring>Artists/Thoma Kinkades (Flowery Landscape)=by thomas kinkade</string>\r\n  <" +
            "string>Artists/John Berkey (Detailed paintings)=very detailed painting by John B" +
            "erkey</string>\r\n  <string>Artists/William-Adolphe Bouguereau (Old style painting" +
            ")=painting by William-Adolphe Bouguereau</string>\r\n  <string>Artists/Claude Mone" +
            "t (Noisy painting)=painting by Claude Monet</string>\r\n  <string>Artists/Ernest Z" +
            "acharevic (Wall mural)=wall mural by Ernest Zacharevic</string>\r\n  <string>Artis" +
            "ts/Stephan Martiniere (sci-fi concept art)=by Stephan Martiniere</string>\r\n  <st" +
            "ring>Artists/Carl Barks (Cartoonist)=cartoon by Carl Barks</string>\r\n  <string>A" +
            "rtists/Peter Gric (Creepy drawings)=Peter Gric print</string>\r\n  <string>Artists" +
            "/Sung Choi (Sci-Fi fantacy concept art)=trending on artstation by sung choi</str" +
            "ing>\r\n  <string>Artists/Ilya Kuvshinov (Retro anime portraits)=by ilya kuvshinov" +
            "</string>\r\n  <string>Artists/Andreas Rocha (Fantacy landscapes)=by andreas rocha" +
            "</string>\r\n  <string>Artists/Lois Van Baarle (Paited portraits)=by lois van baar" +
            "le</string>\r\n  <string>Artists/Rossdraws (Digital art portraits)=by rossdraws</s" +
            "tring>\r\n  <string>Artists/Rembrandt (Baroque paintings)=by Rembrandt</string>\r\n " +
            " <string>Artists/Marc Simonetti (Detailed dark digital landscapes)=highly detail" +
            "ed by marc simonetti</string>\r\n  <string>Artists/Luis Royo (Creepy detailed port" +
            "raits)=by Luis Royo</string>\r\n  <string>Artists/Beksiński (Creepy abstract paint" +
            "ings)=by beksiński</string>\r\n  <string>Artists/Hieronymus Bosch (Renaissance pai" +
            "ntings)=by hieronymus bosch</string>\r\n  <string>Styles/Midjourney1=Splash art, l" +
            "ight dust, magnificent, details, sharp focus, intricate, beautiful, triadic cont" +
            "rast colors, trending artstation, pixiv, digital art</string>\r\n  <string>Styles/" +
            "Midjourney2=Splash art light dust trending Professional majestic oil painting of" +
            " establishing shot by Ed Blinkey and Atey Ghailan and Studio Ghibli and Jeremy M" +
            "ann and Greg Manchess and Antonio Moro volumetric lighting, dramatic lighting</s" +
            "tring>\r\n  <string>Styles/Photo=f/1.4 50mm 200iso 4k</string>\r\n  <string>Styles/i" +
            "Phone photo=iPhone photo</string>\r\n  <string>Styles/Portrait Photography1=facial" +
            " asymmetry, striking features, tack sharp, fine-art photography, 180mm f/1.8 200" +
            "iso</string>\r\n  <string>Styles/Landscape=octane render 4k unreal engine 200iso d" +
            "ynamic range hdr wonderous awesome greg rutkowski sung choi thomas kinkade vince" +
            "nt van gogh</string>\r\n  <string>Styles/High Quality=hyperdetailed hd 4k 8k sharp" +
            " focus highly detailed</string>\r\n  <string>Styles/Elegant Portrait=portrait phot" +
            "o headshot by mucha, sharp focus, elegant, render, octane, detailed, award winni" +
            "ng photography, masterpiece, rim lit</string>\r\n  <string>Styles/Artistic Portrai" +
            "t=a vibrant professional studio portrait photography casual, delightful, intrica" +
            "te, piercing eyes, nouveau, curated collection, annie leibovitz, nikon, award wi" +
            "nning, breathtaking, groundbreaking, superb, outstanding, lensculture portrait a" +
            "wards, photoshopped, dramatic lighting, 8k</string>\r\n  <string>Styles/Rendered P" +
            "ortrait=soft, octane render, unreal engine, photograph, realistic skin texture, " +
            "photorealistic, hyper realism, highly detailed, 85mm portrait photography, award" +
            " winning, hard rim lighting photography</string>\r\n  <string>Styles/Stary Portrai" +
            "t=a portrait with a luminous clothing, eyes shut, mouth closed, wind, sky, cloud" +
            "s, the moon, moonlight, stars, universe, fireflies, butterflies, lights, lens fl" +
            "ares effects, swirly bokeh, brush effect, In style of Yoji Shinkawa, Jackson Pol" +
            "lock, wojtek fus, by Makoto Shinkai, concept art, celestial, amazing, astonishin" +
            "g, wonderful, beautiful, highly detailed, centered</string>\r\n  <string>Styles/Ph" +
            "otobashing=photobashing</string>\r\n  <string>Styles/Algorithmic Art=algorithmic a" +
            "rt</string>\r\n  <string>Styles/Stars and Galaxies=composed of billions of stars, " +
            "digital art</string>\r\n  <string>Styles/Cartoon dramatic style=digital art, 2.5D " +
            "style</string>\r\n  <string>Styles/Made of smoke=made of very detailed curling wis" +
            "py glowing multicolored smoke, digital art, volumetric, 3D render, Octane render" +
            "</string>\r\n  <string>Styles/Detailed Particles=detailed particle, digital art</s" +
            "tring>\r\n  <string>Styles/Mixed Media=mixed media</string>\r\n  <string>Styles/Magi" +
            "cal world=magical world</string>\r\n  <string>Styles/concept art=concept art splas" +
            "h art</string>\r\n  <string>Cinematography/Cinematic lighting=cinematic lighting</" +
            "string>\r\n  <string>Cinematography/Low angle=photograph, taken from a low angle</" +
            "string>\r\n  <string>Cinematography/Over the shoulder=photograph, over-the-shoulde" +
            "r</string>\r\n  <string>Cinematography/Drone footage=photograph taken from a drone" +
            "</string>\r\n  <string>Cinematography/Close up=close up</string>\r\n  <string>Cinema" +
            "tography/Wide angle=wide angle</string>\r\n  <string>Cinematography/Movie ccene=an" +
            "amorphic lens film scene movie still f/2 800iso 35mm light dust haze</string>\r\n " +
            " <string>Cinematography/Golden hour=golden hour</string>\r\n  <string>Cinematograp" +
            "hy/beautiful lighting=beautiful lighting</string>\r\n  <string>Sources/artstation=" +
            "artstation</string>\r\n  <string>Sources/instagram=instagram</string>\r\n  <string>S" +
            "ources/deviantart=deviantart</string>\r\n  <string>Sources/reddit=reddit</string>\r" +
            "\n  <string>Sources/shutterstock=shutterstock</string>\r\n  <string>Sources/tumblr=" +
            "tumblr</string>\r\n  <string>Sources/cgsociety=cgsociety</string>\r\n  <string>Sourc" +
            "es/flickr=flickr</string>\r\n  <string>Sources/behance=behance</string>\r\n  <string" +
            ">Sources/dribble=dribble</string>\r\n  <string>Sources/pexels=pexels</string>\r\n  <" +
            "string>Sources/pinterest=pinterest</string>\r\n  <string>Sources/pixabay=pixabay</" +
            "string>\r\n  <string>Sources/pixiv=pixiv</string>\r\n  <string>Sources/polycount=pol" +
            "ycount</string>\r\n  <string>Effects/post processing=post processing</string>\r\n  <" +
            "string>Effects/cgi=cgi</string>\r\n  <string>Effects/chromatic aberration=chromati" +
            "c aberration</string>\r\n  <string>Effects/anaglyph=anaglyph</string>\r\n  <string>E" +
            "ffects/cropped=cropped</string>\r\n  <string>Effects/glowing edges=glowing edges</" +
            "string>\r\n  <string>Effects/glow effect=glow effect</string>\r\n  <string>Effects/b" +
            "okeh=bokeh</string>\r\n  <string>Effects/dramatic lighting=dramatic lighting</stri" +
            "ng>\r\n  <string>Effects/soft lighting=soft lighting</string>\r\n  <string>Effects/h" +
            "ard lighting=hard lighting</string>\r\n  <string>Effects/glamor shot=glamor shot</" +
            "string>\r\n  <string>Effects/colourful=colourful</string>\r\n  <string>Effects/compl" +
            "imentary-colours=complimentary-colours</string>\r\n  <string>Effects/dark mood=dar" +
            "k mood</string>\r\n  <string>Effects/multiverse=multiverse</string>\r\n  <string>Eff" +
            "ects/volumetric lighting=volumetric lighting</string>\r\n  <string>Effects/lumen g" +
            "lobal illumination=lumen global illumination</string>\r\n  <string>Effects/octane " +
            "render=octane render</string>\r\n  <string>Effects/atmospheric=atmospheric</string" +
            ">\r\n  <string>Effects/technicolour=technicolour</string>\r\n  <string>Mediums/photo" +
            " realistic=photo realistic</string>\r\n  <string>Mediums/anime=anime manga</string" +
            ">\r\n  <string>Mediums/graphic novel=graphic novel</string>\r\n  <string>Mediums/fou" +
            "ntain pen=fountain pen</string>\r\n  <string>Mediums/pastel art=pastel art</string" +
            ">\r\n  <string>Mediums/fine art=fine art</string>\r\n  <string>Mediums/acrylic paint" +
            "=acrylic paint</string>\r\n  <string>Mediums/oil paint=oil paint</string>\r\n  <stri" +
            "ng>Mediums/watercolour=watercolour</string>\r\n  <string>Mediums/digital art=digit" +
            "al art</string>\r\n  <string>Mediums/magazine=magazine</string>\r\n  <string>Mediums" +
            "/comic book=comic book</string>\r\n  <string>Mediums/pokemon card=pokemon card</st" +
            "ring>\r\n  <string>Mediums/puzzle=puzzle</string>\r\n  <string>Mediums/logo=logo</st" +
            "ring>\r\n  <string>Mediums/editorial photography=editorial photography</string>\r\n " +
            " <string>Mediums/wildlife photography=wildlife photography</string>\r\n  <string>N" +
            "egative/Photo=cartoon anime art painting ugly</string>\r\n  <string>Negative/Portr" +
            "ait=bad anatomy bad proportions blurry cloned face deformed disfigured duplicate" +
            " gross proportions long neck mutation mutilated morbid out of frame poorly drawn" +
            " face</string>\r\n  <string>Negative/Hands=poorly drawn hands extra arms extra fin" +
            "gers extra limbs extra legs too many fingers fused fingers malformed limbs missi" +
            "ng arms missmg legs mutated hands</string>\r\n  <string>Negative/Safety=nsfw nude " +
            "naked</string>\r\n  <string>Negative/Landscape=portrait person people text signatu" +
            "re watermark frame framed display</string>\r\n  <string>Negative/High Quality=lowr" +
            "es text error cropped worst quality low quality normal quality jpeg artifacts si" +
            "gnature watermark username blurry artist name deformed disfigured poorly drawn o" +
            "ut of focus censorship amateur drawing bad art poor art messy drawing</string>\r\n" +
            "</ArrayOfString>")]
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
        [global::System.Configuration.DefaultSettingValueAttribute("0")]
        public string DeleteOnExit {
            get {
                return ((string)(this["DeleteOnExit"]));
            }
            set {
                this["DeleteOnExit"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("C:\\InvokeAI-main\\outputs")]
        public string OutputPath {
            get {
                return ((string)(this["OutputPath"]));
            }
            set {
                this["OutputPath"] = value;
            }
        }
    }
}

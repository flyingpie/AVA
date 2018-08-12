// Extensions.cs
namespace FontAwesomeCS
{
    using System;
    using System.Reflection;

    public static class Extensions
    {
        /// <summary>
        /// Returns the key of the icon, eg "github-alt"
        /// </summary>
        public static string Key(this FAIcon icon) => icon.GetAttributeOfType<FAIconAttribute>().Key;

        /// <summary>
        /// Returns the label of the icon, eg "Alternate GitHub"
        /// </summary>
        public static string Label(this FAIcon icon) => icon.GetAttributeOfType<FAIconAttribute>().Label;

        /// <summary>
        /// Returns the unicode character of the icon, eg "f113"
        /// </summary>
        public static string Unicode(this FAIcon icon) => icon.GetAttributeOfType<FAIconAttribute>().Unicode;

        /// <summary>
        /// Returns the style enum of the icon, eg <see cref="FAStyle.Brands" />
        /// </summary>
        public static FAStyle FAStyle(this FAIcon icon) => icon.GetAttributeOfType<FAIconAttribute>().Style;

        /// <summary>
        /// Returns the style of the icon as a string, eg "brands"
        /// </summary>
        public static string Style(this FAIcon icon) => icon.FAStyle().Style();

        /// <summary>
        /// Returns the name of the style, eg "brands"
        /// </summary>
        public static string Style(this FAStyle icon) => icon.GetAttributeOfType<FAStyleAttribute>().Style;

        /// <summary>
        /// Returns the <see cref="FAIconAttribute" /> associated with the icon, containing its metadata
        /// </summary>
        public static FAIconAttribute GetFAIconAttribute(this FAIcon icon) => icon.GetAttributeOfType<FAIconAttribute>();

        /// <summary>
        /// Returns the <see cref="FAStyleAttribute" /> associated with the style, containing its metadata
        /// </summary>
        public static FAStyleAttribute GetFAStyleAttribute(this FAStyle style) => style.GetAttributeOfType<FAStyleAttribute>();

        private static T GetAttributeOfType<T>(this Enum enumVal) where T : Attribute
        {
            return enumVal
                .GetType()
                .GetMember(enumVal.ToString())[0]
                .GetCustomAttribute<T>()
            ;
        }
    }
}


// FAIconAttribute.cs
namespace FontAwesomeCS
{
    using System;

    public class FAIconAttribute : Attribute
    {
        public FAIconAttribute(string key, string label, FAStyle style, string unicode)
        {
            Key = key;
            Label = label;
            Style = style;
            Unicode = unicode;
        }

        public string Key { get; set; }

        public string Label { get; set; }

        public FAStyle Style { get; set; }

        public string Unicode { get; set; }
    }
}


// FAStyleAttribute.cs
namespace FontAwesomeCS
{
    using System;

    public class FAStyleAttribute : Attribute
    {
        public FAStyleAttribute(string style)
        {
            Style = style;
        }

        public string Style { get; set; }
    }
}


// FontAwesomeCS.cs
namespace FontAwesomeCS
{
    public enum FAStyle
    {
        [FAStyle("brands")] Brands,
        [FAStyle("regular")] Regular,
        [FAStyle("solid")] Solid,
    }
    public enum FAIcon
    {
        None = 0,
        /// <summary>500px - 500px - f26e</summary>
        [FAIcon("500px", "500px", FAStyle.Brands, "\uf26e")] _500pxBrands,
        /// <summary>Accessible Icon - accessible-icon - f368</summary>
        [FAIcon("accessible-icon", "Accessible Icon", FAStyle.Brands, "\uf368")] AccessibleIconBrands,
        /// <summary>Accusoft - accusoft - f369</summary>
        [FAIcon("accusoft", "Accusoft", FAStyle.Brands, "\uf369")] AccusoftBrands,
        /// <summary>Address Book - address-book - f2b9</summary>
        [FAIcon("address-book", "Address Book", FAStyle.Solid, "\uf2b9")] AddressBookSolid,
        /// <summary>Address Book - address-book - f2b9</summary>
        [FAIcon("address-book", "Address Book", FAStyle.Regular, "\uf2b9")] AddressBookRegular,
        /// <summary>Address Card - address-card - f2bb</summary>
        [FAIcon("address-card", "Address Card", FAStyle.Solid, "\uf2bb")] AddressCardSolid,
        /// <summary>Address Card - address-card - f2bb</summary>
        [FAIcon("address-card", "Address Card", FAStyle.Regular, "\uf2bb")] AddressCardRegular,
        /// <summary>adjust - adjust - f042</summary>
        [FAIcon("adjust", "adjust", FAStyle.Solid, "\uf042")] AdjustSolid,
        /// <summary>App.net - adn - f170</summary>
        [FAIcon("adn", "App.net", FAStyle.Brands, "\uf170")] AdnBrands,
        /// <summary>Adversal - adversal - f36a</summary>
        [FAIcon("adversal", "Adversal", FAStyle.Brands, "\uf36a")] AdversalBrands,
        /// <summary>affiliatetheme - affiliatetheme - f36b</summary>
        [FAIcon("affiliatetheme", "affiliatetheme", FAStyle.Brands, "\uf36b")] AffiliatethemeBrands,
        /// <summary>Air Freshener - air-freshener - f5d0</summary>
        [FAIcon("air-freshener", "Air Freshener", FAStyle.Solid, "\uf5d0")] AirFreshenerSolid,
        /// <summary>Algolia - algolia - f36c</summary>
        [FAIcon("algolia", "Algolia", FAStyle.Brands, "\uf36c")] AlgoliaBrands,
        /// <summary>align-center - align-center - f037</summary>
        [FAIcon("align-center", "align-center", FAStyle.Solid, "\uf037")] AlignCenterSolid,
        /// <summary>align-justify - align-justify - f039</summary>
        [FAIcon("align-justify", "align-justify", FAStyle.Solid, "\uf039")] AlignJustifySolid,
        /// <summary>align-left - align-left - f036</summary>
        [FAIcon("align-left", "align-left", FAStyle.Solid, "\uf036")] AlignLeftSolid,
        /// <summary>align-right - align-right - f038</summary>
        [FAIcon("align-right", "align-right", FAStyle.Solid, "\uf038")] AlignRightSolid,
        /// <summary>Allergies - allergies - f461</summary>
        [FAIcon("allergies", "Allergies", FAStyle.Solid, "\uf461")] AllergiesSolid,
        /// <summary>Amazon - amazon - f270</summary>
        [FAIcon("amazon", "Amazon", FAStyle.Brands, "\uf270")] AmazonBrands,
        /// <summary>Amazon Pay - amazon-pay - f42c</summary>
        [FAIcon("amazon-pay", "Amazon Pay", FAStyle.Brands, "\uf42c")] AmazonPayBrands,
        /// <summary>ambulance - ambulance - f0f9</summary>
        [FAIcon("ambulance", "ambulance", FAStyle.Solid, "\uf0f9")] AmbulanceSolid,
        /// <summary>American Sign Language Interpreting - american-sign-language-interpreting - f2a3</summary>
        [FAIcon("american-sign-language-interpreting", "American Sign Language Interpreting", FAStyle.Solid, "\uf2a3")] AmericanSignLanguageInterpretingSolid,
        /// <summary>Amilia - amilia - f36d</summary>
        [FAIcon("amilia", "Amilia", FAStyle.Brands, "\uf36d")] AmiliaBrands,
        /// <summary>Anchor - anchor - f13d</summary>
        [FAIcon("anchor", "Anchor", FAStyle.Solid, "\uf13d")] AnchorSolid,
        /// <summary>Android - android - f17b</summary>
        [FAIcon("android", "Android", FAStyle.Brands, "\uf17b")] AndroidBrands,
        /// <summary>AngelList - angellist - f209</summary>
        [FAIcon("angellist", "AngelList", FAStyle.Brands, "\uf209")] AngellistBrands,
        /// <summary>Angle Double Down - angle-double-down - f103</summary>
        [FAIcon("angle-double-down", "Angle Double Down", FAStyle.Solid, "\uf103")] AngleDoubleDownSolid,
        /// <summary>Angle Double Left - angle-double-left - f100</summary>
        [FAIcon("angle-double-left", "Angle Double Left", FAStyle.Solid, "\uf100")] AngleDoubleLeftSolid,
        /// <summary>Angle Double Right - angle-double-right - f101</summary>
        [FAIcon("angle-double-right", "Angle Double Right", FAStyle.Solid, "\uf101")] AngleDoubleRightSolid,
        /// <summary>Angle Double Up - angle-double-up - f102</summary>
        [FAIcon("angle-double-up", "Angle Double Up", FAStyle.Solid, "\uf102")] AngleDoubleUpSolid,
        /// <summary>angle-down - angle-down - f107</summary>
        [FAIcon("angle-down", "angle-down", FAStyle.Solid, "\uf107")] AngleDownSolid,
        /// <summary>angle-left - angle-left - f104</summary>
        [FAIcon("angle-left", "angle-left", FAStyle.Solid, "\uf104")] AngleLeftSolid,
        /// <summary>angle-right - angle-right - f105</summary>
        [FAIcon("angle-right", "angle-right", FAStyle.Solid, "\uf105")] AngleRightSolid,
        /// <summary>angle-up - angle-up - f106</summary>
        [FAIcon("angle-up", "angle-up", FAStyle.Solid, "\uf106")] AngleUpSolid,
        /// <summary>Angry Face - angry - f556</summary>
        [FAIcon("angry", "Angry Face", FAStyle.Solid, "\uf556")] AngrySolid,
        /// <summary>Angry Face - angry - f556</summary>
        [FAIcon("angry", "Angry Face", FAStyle.Regular, "\uf556")] AngryRegular,
        /// <summary>Angry Creative - angrycreative - f36e</summary>
        [FAIcon("angrycreative", "Angry Creative", FAStyle.Brands, "\uf36e")] AngrycreativeBrands,
        /// <summary>Angular - angular - f420</summary>
        [FAIcon("angular", "Angular", FAStyle.Brands, "\uf420")] AngularBrands,
        /// <summary>App Store - app-store - f36f</summary>
        [FAIcon("app-store", "App Store", FAStyle.Brands, "\uf36f")] AppStoreBrands,
        /// <summary>iOS App Store - app-store-ios - f370</summary>
        [FAIcon("app-store-ios", "iOS App Store", FAStyle.Brands, "\uf370")] AppStoreIosBrands,
        /// <summary>Apper Systems AB - apper - f371</summary>
        [FAIcon("apper", "Apper Systems AB", FAStyle.Brands, "\uf371")] ApperBrands,
        /// <summary>Apple - apple - f179</summary>
        [FAIcon("apple", "Apple", FAStyle.Brands, "\uf179")] AppleBrands,
        /// <summary>Fruit Apple - apple-alt - f5d1</summary>
        [FAIcon("apple-alt", "Fruit Apple", FAStyle.Solid, "\uf5d1")] AppleAltSolid,
        /// <summary>Apple Pay - apple-pay - f415</summary>
        [FAIcon("apple-pay", "Apple Pay", FAStyle.Brands, "\uf415")] ApplePayBrands,
        /// <summary>Archive - archive - f187</summary>
        [FAIcon("archive", "Archive", FAStyle.Solid, "\uf187")] ArchiveSolid,
        /// <summary>Archway - archway - f557</summary>
        [FAIcon("archway", "Archway", FAStyle.Solid, "\uf557")] ArchwaySolid,
        /// <summary>Alternate Arrow Circle Down - arrow-alt-circle-down - f358</summary>
        [FAIcon("arrow-alt-circle-down", "Alternate Arrow Circle Down", FAStyle.Solid, "\uf358")] ArrowAltCircleDownSolid,
        /// <summary>Alternate Arrow Circle Down - arrow-alt-circle-down - f358</summary>
        [FAIcon("arrow-alt-circle-down", "Alternate Arrow Circle Down", FAStyle.Regular, "\uf358")] ArrowAltCircleDownRegular,
        /// <summary>Alternate Arrow Circle Left - arrow-alt-circle-left - f359</summary>
        [FAIcon("arrow-alt-circle-left", "Alternate Arrow Circle Left", FAStyle.Solid, "\uf359")] ArrowAltCircleLeftSolid,
        /// <summary>Alternate Arrow Circle Left - arrow-alt-circle-left - f359</summary>
        [FAIcon("arrow-alt-circle-left", "Alternate Arrow Circle Left", FAStyle.Regular, "\uf359")] ArrowAltCircleLeftRegular,
        /// <summary>Alternate Arrow Circle Right - arrow-alt-circle-right - f35a</summary>
        [FAIcon("arrow-alt-circle-right", "Alternate Arrow Circle Right", FAStyle.Solid, "\uf35a")] ArrowAltCircleRightSolid,
        /// <summary>Alternate Arrow Circle Right - arrow-alt-circle-right - f35a</summary>
        [FAIcon("arrow-alt-circle-right", "Alternate Arrow Circle Right", FAStyle.Regular, "\uf35a")] ArrowAltCircleRightRegular,
        /// <summary>Alternate Arrow Circle Up - arrow-alt-circle-up - f35b</summary>
        [FAIcon("arrow-alt-circle-up", "Alternate Arrow Circle Up", FAStyle.Solid, "\uf35b")] ArrowAltCircleUpSolid,
        /// <summary>Alternate Arrow Circle Up - arrow-alt-circle-up - f35b</summary>
        [FAIcon("arrow-alt-circle-up", "Alternate Arrow Circle Up", FAStyle.Regular, "\uf35b")] ArrowAltCircleUpRegular,
        /// <summary>Arrow Circle Down - arrow-circle-down - f0ab</summary>
        [FAIcon("arrow-circle-down", "Arrow Circle Down", FAStyle.Solid, "\uf0ab")] ArrowCircleDownSolid,
        /// <summary>Arrow Circle Left - arrow-circle-left - f0a8</summary>
        [FAIcon("arrow-circle-left", "Arrow Circle Left", FAStyle.Solid, "\uf0a8")] ArrowCircleLeftSolid,
        /// <summary>Arrow Circle Right - arrow-circle-right - f0a9</summary>
        [FAIcon("arrow-circle-right", "Arrow Circle Right", FAStyle.Solid, "\uf0a9")] ArrowCircleRightSolid,
        /// <summary>Arrow Circle Up - arrow-circle-up - f0aa</summary>
        [FAIcon("arrow-circle-up", "Arrow Circle Up", FAStyle.Solid, "\uf0aa")] ArrowCircleUpSolid,
        /// <summary>arrow-down - arrow-down - f063</summary>
        [FAIcon("arrow-down", "arrow-down", FAStyle.Solid, "\uf063")] ArrowDownSolid,
        /// <summary>arrow-left - arrow-left - f060</summary>
        [FAIcon("arrow-left", "arrow-left", FAStyle.Solid, "\uf060")] ArrowLeftSolid,
        /// <summary>arrow-right - arrow-right - f061</summary>
        [FAIcon("arrow-right", "arrow-right", FAStyle.Solid, "\uf061")] ArrowRightSolid,
        /// <summary>arrow-up - arrow-up - f062</summary>
        [FAIcon("arrow-up", "arrow-up", FAStyle.Solid, "\uf062")] ArrowUpSolid,
        /// <summary>Alternate Arrows - arrows-alt - f0b2</summary>
        [FAIcon("arrows-alt", "Alternate Arrows", FAStyle.Solid, "\uf0b2")] ArrowsAltSolid,
        /// <summary>Alternate Arrows Horizontal - arrows-alt-h - f337</summary>
        [FAIcon("arrows-alt-h", "Alternate Arrows Horizontal", FAStyle.Solid, "\uf337")] ArrowsAltHSolid,
        /// <summary>Alternate Arrows Vertical - arrows-alt-v - f338</summary>
        [FAIcon("arrows-alt-v", "Alternate Arrows Vertical", FAStyle.Solid, "\uf338")] ArrowsAltVSolid,
        /// <summary>Assistive Listening Systems - assistive-listening-systems - f2a2</summary>
        [FAIcon("assistive-listening-systems", "Assistive Listening Systems", FAStyle.Solid, "\uf2a2")] AssistiveListeningSystemsSolid,
        /// <summary>asterisk - asterisk - f069</summary>
        [FAIcon("asterisk", "asterisk", FAStyle.Solid, "\uf069")] AsteriskSolid,
        /// <summary>Asymmetrik, Ltd. - asymmetrik - f372</summary>
        [FAIcon("asymmetrik", "Asymmetrik, Ltd.", FAStyle.Brands, "\uf372")] AsymmetrikBrands,
        /// <summary>At - at - f1fa</summary>
        [FAIcon("at", "At", FAStyle.Solid, "\uf1fa")] AtSolid,
        /// <summary>Atlas - atlas - f558</summary>
        [FAIcon("atlas", "Atlas", FAStyle.Solid, "\uf558")] AtlasSolid,
        /// <summary>Atom - atom - f5d2</summary>
        [FAIcon("atom", "Atom", FAStyle.Solid, "\uf5d2")] AtomSolid,
        /// <summary>Audible - audible - f373</summary>
        [FAIcon("audible", "Audible", FAStyle.Brands, "\uf373")] AudibleBrands,
        /// <summary>Audio Description - audio-description - f29e</summary>
        [FAIcon("audio-description", "Audio Description", FAStyle.Solid, "\uf29e")] AudioDescriptionSolid,
        /// <summary>Autoprefixer - autoprefixer - f41c</summary>
        [FAIcon("autoprefixer", "Autoprefixer", FAStyle.Brands, "\uf41c")] AutoprefixerBrands,
        /// <summary>avianex - avianex - f374</summary>
        [FAIcon("avianex", "avianex", FAStyle.Brands, "\uf374")] AvianexBrands,
        /// <summary>Aviato - aviato - f421</summary>
        [FAIcon("aviato", "Aviato", FAStyle.Brands, "\uf421")] AviatoBrands,
        /// <summary>Award - award - f559</summary>
        [FAIcon("award", "Award", FAStyle.Solid, "\uf559")] AwardSolid,
        /// <summary>Amazon Web Services (AWS) - aws - f375</summary>
        [FAIcon("aws", "Amazon Web Services (AWS)", FAStyle.Brands, "\uf375")] AwsBrands,
        /// <summary>Backspace - backspace - f55a</summary>
        [FAIcon("backspace", "Backspace", FAStyle.Solid, "\uf55a")] BackspaceSolid,
        /// <summary>backward - backward - f04a</summary>
        [FAIcon("backward", "backward", FAStyle.Solid, "\uf04a")] BackwardSolid,
        /// <summary>Balance Scale - balance-scale - f24e</summary>
        [FAIcon("balance-scale", "Balance Scale", FAStyle.Solid, "\uf24e")] BalanceScaleSolid,
        /// <summary>ban - ban - f05e</summary>
        [FAIcon("ban", "ban", FAStyle.Solid, "\uf05e")] BanSolid,
        /// <summary>Band-Aid - band-aid - f462</summary>
        [FAIcon("band-aid", "Band-Aid", FAStyle.Solid, "\uf462")] BandAidSolid,
        /// <summary>Bandcamp - bandcamp - f2d5</summary>
        [FAIcon("bandcamp", "Bandcamp", FAStyle.Brands, "\uf2d5")] BandcampBrands,
        /// <summary>barcode - barcode - f02a</summary>
        [FAIcon("barcode", "barcode", FAStyle.Solid, "\uf02a")] BarcodeSolid,
        /// <summary>Bars - bars - f0c9</summary>
        [FAIcon("bars", "Bars", FAStyle.Solid, "\uf0c9")] BarsSolid,
        /// <summary>Baseball Ball - baseball-ball - f433</summary>
        [FAIcon("baseball-ball", "Baseball Ball", FAStyle.Solid, "\uf433")] BaseballBallSolid,
        /// <summary>Basketball Ball - basketball-ball - f434</summary>
        [FAIcon("basketball-ball", "Basketball Ball", FAStyle.Solid, "\uf434")] BasketballBallSolid,
        /// <summary>Bath - bath - f2cd</summary>
        [FAIcon("bath", "Bath", FAStyle.Solid, "\uf2cd")] BathSolid,
        /// <summary>Battery Empty - battery-empty - f244</summary>
        [FAIcon("battery-empty", "Battery Empty", FAStyle.Solid, "\uf244")] BatteryEmptySolid,
        /// <summary>Battery Full - battery-full - f240</summary>
        [FAIcon("battery-full", "Battery Full", FAStyle.Solid, "\uf240")] BatteryFullSolid,
        /// <summary>Battery 1/2 Full - battery-half - f242</summary>
        [FAIcon("battery-half", "Battery 1/2 Full", FAStyle.Solid, "\uf242")] BatteryHalfSolid,
        /// <summary>Battery 1/4 Full - battery-quarter - f243</summary>
        [FAIcon("battery-quarter", "Battery 1/4 Full", FAStyle.Solid, "\uf243")] BatteryQuarterSolid,
        /// <summary>Battery 3/4 Full - battery-three-quarters - f241</summary>
        [FAIcon("battery-three-quarters", "Battery 3/4 Full", FAStyle.Solid, "\uf241")] BatteryThreeQuartersSolid,
        /// <summary>Bed - bed - f236</summary>
        [FAIcon("bed", "Bed", FAStyle.Solid, "\uf236")] BedSolid,
        /// <summary>beer - beer - f0fc</summary>
        [FAIcon("beer", "beer", FAStyle.Solid, "\uf0fc")] BeerSolid,
        /// <summary>Behance - behance - f1b4</summary>
        [FAIcon("behance", "Behance", FAStyle.Brands, "\uf1b4")] BehanceBrands,
        /// <summary>Behance Square - behance-square - f1b5</summary>
        [FAIcon("behance-square", "Behance Square", FAStyle.Brands, "\uf1b5")] BehanceSquareBrands,
        /// <summary>bell - bell - f0f3</summary>
        [FAIcon("bell", "bell", FAStyle.Solid, "\uf0f3")] BellSolid,
        /// <summary>bell - bell - f0f3</summary>
        [FAIcon("bell", "bell", FAStyle.Regular, "\uf0f3")] BellRegular,
        /// <summary>Bell Slash - bell-slash - f1f6</summary>
        [FAIcon("bell-slash", "Bell Slash", FAStyle.Solid, "\uf1f6")] BellSlashSolid,
        /// <summary>Bell Slash - bell-slash - f1f6</summary>
        [FAIcon("bell-slash", "Bell Slash", FAStyle.Regular, "\uf1f6")] BellSlashRegular,
        /// <summary>Bezier Curve - bezier-curve - f55b</summary>
        [FAIcon("bezier-curve", "Bezier Curve", FAStyle.Solid, "\uf55b")] BezierCurveSolid,
        /// <summary>Bicycle - bicycle - f206</summary>
        [FAIcon("bicycle", "Bicycle", FAStyle.Solid, "\uf206")] BicycleSolid,
        /// <summary>BIMobject - bimobject - f378</summary>
        [FAIcon("bimobject", "BIMobject", FAStyle.Brands, "\uf378")] BimobjectBrands,
        /// <summary>Binoculars - binoculars - f1e5</summary>
        [FAIcon("binoculars", "Binoculars", FAStyle.Solid, "\uf1e5")] BinocularsSolid,
        /// <summary>Birthday Cake - birthday-cake - f1fd</summary>
        [FAIcon("birthday-cake", "Birthday Cake", FAStyle.Solid, "\uf1fd")] BirthdayCakeSolid,
        /// <summary>Bitbucket - bitbucket - f171</summary>
        [FAIcon("bitbucket", "Bitbucket", FAStyle.Brands, "\uf171")] BitbucketBrands,
        /// <summary>Bitcoin - bitcoin - f379</summary>
        [FAIcon("bitcoin", "Bitcoin", FAStyle.Brands, "\uf379")] BitcoinBrands,
        /// <summary>Bity - bity - f37a</summary>
        [FAIcon("bity", "Bity", FAStyle.Brands, "\uf37a")] BityBrands,
        /// <summary>Font Awesome Black Tie - black-tie - f27e</summary>
        [FAIcon("black-tie", "Font Awesome Black Tie", FAStyle.Brands, "\uf27e")] BlackTieBrands,
        /// <summary>BlackBerry - blackberry - f37b</summary>
        [FAIcon("blackberry", "BlackBerry", FAStyle.Brands, "\uf37b")] BlackberryBrands,
        /// <summary>Blender - blender - f517</summary>
        [FAIcon("blender", "Blender", FAStyle.Solid, "\uf517")] BlenderSolid,
        /// <summary>Blind - blind - f29d</summary>
        [FAIcon("blind", "Blind", FAStyle.Solid, "\uf29d")] BlindSolid,
        /// <summary>Blogger - blogger - f37c</summary>
        [FAIcon("blogger", "Blogger", FAStyle.Brands, "\uf37c")] BloggerBrands,
        /// <summary>Blogger B - blogger-b - f37d</summary>
        [FAIcon("blogger-b", "Blogger B", FAStyle.Brands, "\uf37d")] BloggerBBrands,
        /// <summary>Bluetooth - bluetooth - f293</summary>
        [FAIcon("bluetooth", "Bluetooth", FAStyle.Brands, "\uf293")] BluetoothBrands,
        /// <summary>Bluetooth - bluetooth-b - f294</summary>
        [FAIcon("bluetooth-b", "Bluetooth", FAStyle.Brands, "\uf294")] BluetoothBBrands,
        /// <summary>bold - bold - f032</summary>
        [FAIcon("bold", "bold", FAStyle.Solid, "\uf032")] BoldSolid,
        /// <summary>Lightning Bolt - bolt - f0e7</summary>
        [FAIcon("bolt", "Lightning Bolt", FAStyle.Solid, "\uf0e7")] BoltSolid,
        /// <summary>Bomb - bomb - f1e2</summary>
        [FAIcon("bomb", "Bomb", FAStyle.Solid, "\uf1e2")] BombSolid,
        /// <summary>Bone - bone - f5d7</summary>
        [FAIcon("bone", "Bone", FAStyle.Solid, "\uf5d7")] BoneSolid,
        /// <summary>Bong - bong - f55c</summary>
        [FAIcon("bong", "Bong", FAStyle.Solid, "\uf55c")] BongSolid,
        /// <summary>book - book - f02d</summary>
        [FAIcon("book", "book", FAStyle.Solid, "\uf02d")] BookSolid,
        /// <summary>Book Open - book-open - f518</summary>
        [FAIcon("book-open", "Book Open", FAStyle.Solid, "\uf518")] BookOpenSolid,
        /// <summary>Book Reader - book-reader - f5da</summary>
        [FAIcon("book-reader", "Book Reader", FAStyle.Solid, "\uf5da")] BookReaderSolid,
        /// <summary>bookmark - bookmark - f02e</summary>
        [FAIcon("bookmark", "bookmark", FAStyle.Solid, "\uf02e")] BookmarkSolid,
        /// <summary>bookmark - bookmark - f02e</summary>
        [FAIcon("bookmark", "bookmark", FAStyle.Regular, "\uf02e")] BookmarkRegular,
        /// <summary>Bowling Ball - bowling-ball - f436</summary>
        [FAIcon("bowling-ball", "Bowling Ball", FAStyle.Solid, "\uf436")] BowlingBallSolid,
        /// <summary>Box - box - f466</summary>
        [FAIcon("box", "Box", FAStyle.Solid, "\uf466")] BoxSolid,
        /// <summary>Box Open - box-open - f49e</summary>
        [FAIcon("box-open", "Box Open", FAStyle.Solid, "\uf49e")] BoxOpenSolid,
        /// <summary>Boxes - boxes - f468</summary>
        [FAIcon("boxes", "Boxes", FAStyle.Solid, "\uf468")] BoxesSolid,
        /// <summary>Braille - braille - f2a1</summary>
        [FAIcon("braille", "Braille", FAStyle.Solid, "\uf2a1")] BrailleSolid,
        /// <summary>Brain - brain - f5dc</summary>
        [FAIcon("brain", "Brain", FAStyle.Solid, "\uf5dc")] BrainSolid,
        /// <summary>Briefcase - briefcase - f0b1</summary>
        [FAIcon("briefcase", "Briefcase", FAStyle.Solid, "\uf0b1")] BriefcaseSolid,
        /// <summary>Medical Briefcase - briefcase-medical - f469</summary>
        [FAIcon("briefcase-medical", "Medical Briefcase", FAStyle.Solid, "\uf469")] BriefcaseMedicalSolid,
        /// <summary>Broadcast Tower - broadcast-tower - f519</summary>
        [FAIcon("broadcast-tower", "Broadcast Tower", FAStyle.Solid, "\uf519")] BroadcastTowerSolid,
        /// <summary>Broom - broom - f51a</summary>
        [FAIcon("broom", "Broom", FAStyle.Solid, "\uf51a")] BroomSolid,
        /// <summary>Brush - brush - f55d</summary>
        [FAIcon("brush", "Brush", FAStyle.Solid, "\uf55d")] BrushSolid,
        /// <summary>BTC - btc - f15a</summary>
        [FAIcon("btc", "BTC", FAStyle.Brands, "\uf15a")] BtcBrands,
        /// <summary>Bug - bug - f188</summary>
        [FAIcon("bug", "Bug", FAStyle.Solid, "\uf188")] BugSolid,
        /// <summary>Building - building - f1ad</summary>
        [FAIcon("building", "Building", FAStyle.Solid, "\uf1ad")] BuildingSolid,
        /// <summary>Building - building - f1ad</summary>
        [FAIcon("building", "Building", FAStyle.Regular, "\uf1ad")] BuildingRegular,
        /// <summary>bullhorn - bullhorn - f0a1</summary>
        [FAIcon("bullhorn", "bullhorn", FAStyle.Solid, "\uf0a1")] BullhornSolid,
        /// <summary>Bullseye - bullseye - f140</summary>
        [FAIcon("bullseye", "Bullseye", FAStyle.Solid, "\uf140")] BullseyeSolid,
        /// <summary>Burn - burn - f46a</summary>
        [FAIcon("burn", "Burn", FAStyle.Solid, "\uf46a")] BurnSolid,
        /// <summary>Büromöbel-Experte GmbH & Co. KG. - buromobelexperte - f37f</summary>
        [FAIcon("buromobelexperte", "Büromöbel-Experte GmbH & Co. KG.", FAStyle.Brands, "\uf37f")] BuromobelexperteBrands,
        /// <summary>Bus - bus - f207</summary>
        [FAIcon("bus", "Bus", FAStyle.Solid, "\uf207")] BusSolid,
        /// <summary>Bus Alt - bus-alt - f55e</summary>
        [FAIcon("bus-alt", "Bus Alt", FAStyle.Solid, "\uf55e")] BusAltSolid,
        /// <summary>BuySellAds - buysellads - f20d</summary>
        [FAIcon("buysellads", "BuySellAds", FAStyle.Brands, "\uf20d")] BuyselladsBrands,
        /// <summary>Calculator - calculator - f1ec</summary>
        [FAIcon("calculator", "Calculator", FAStyle.Solid, "\uf1ec")] CalculatorSolid,
        /// <summary>Calendar - calendar - f133</summary>
        [FAIcon("calendar", "Calendar", FAStyle.Solid, "\uf133")] CalendarSolid,
        /// <summary>Calendar - calendar - f133</summary>
        [FAIcon("calendar", "Calendar", FAStyle.Regular, "\uf133")] CalendarRegular,
        /// <summary>Alternate Calendar - calendar-alt - f073</summary>
        [FAIcon("calendar-alt", "Alternate Calendar", FAStyle.Solid, "\uf073")] CalendarAltSolid,
        /// <summary>Alternate Calendar - calendar-alt - f073</summary>
        [FAIcon("calendar-alt", "Alternate Calendar", FAStyle.Regular, "\uf073")] CalendarAltRegular,
        /// <summary>Calendar Check - calendar-check - f274</summary>
        [FAIcon("calendar-check", "Calendar Check", FAStyle.Solid, "\uf274")] CalendarCheckSolid,
        /// <summary>Calendar Check - calendar-check - f274</summary>
        [FAIcon("calendar-check", "Calendar Check", FAStyle.Regular, "\uf274")] CalendarCheckRegular,
        /// <summary>Calendar Minus - calendar-minus - f272</summary>
        [FAIcon("calendar-minus", "Calendar Minus", FAStyle.Solid, "\uf272")] CalendarMinusSolid,
        /// <summary>Calendar Minus - calendar-minus - f272</summary>
        [FAIcon("calendar-minus", "Calendar Minus", FAStyle.Regular, "\uf272")] CalendarMinusRegular,
        /// <summary>Calendar Plus - calendar-plus - f271</summary>
        [FAIcon("calendar-plus", "Calendar Plus", FAStyle.Solid, "\uf271")] CalendarPlusSolid,
        /// <summary>Calendar Plus - calendar-plus - f271</summary>
        [FAIcon("calendar-plus", "Calendar Plus", FAStyle.Regular, "\uf271")] CalendarPlusRegular,
        /// <summary>Calendar Times - calendar-times - f273</summary>
        [FAIcon("calendar-times", "Calendar Times", FAStyle.Solid, "\uf273")] CalendarTimesSolid,
        /// <summary>Calendar Times - calendar-times - f273</summary>
        [FAIcon("calendar-times", "Calendar Times", FAStyle.Regular, "\uf273")] CalendarTimesRegular,
        /// <summary>camera - camera - f030</summary>
        [FAIcon("camera", "camera", FAStyle.Solid, "\uf030")] CameraSolid,
        /// <summary>Retro Camera - camera-retro - f083</summary>
        [FAIcon("camera-retro", "Retro Camera", FAStyle.Solid, "\uf083")] CameraRetroSolid,
        /// <summary>Cannabis - cannabis - f55f</summary>
        [FAIcon("cannabis", "Cannabis", FAStyle.Solid, "\uf55f")] CannabisSolid,
        /// <summary>Capsules - capsules - f46b</summary>
        [FAIcon("capsules", "Capsules", FAStyle.Solid, "\uf46b")] CapsulesSolid,
        /// <summary>Car - car - f1b9</summary>
        [FAIcon("car", "Car", FAStyle.Solid, "\uf1b9")] CarSolid,
        /// <summary>Car Alt - car-alt - f5de</summary>
        [FAIcon("car-alt", "Car Alt", FAStyle.Solid, "\uf5de")] CarAltSolid,
        /// <summary>Car Battery - car-battery - f5df</summary>
        [FAIcon("car-battery", "Car Battery", FAStyle.Solid, "\uf5df")] CarBatterySolid,
        /// <summary>Car Crash - car-crash - f5e1</summary>
        [FAIcon("car-crash", "Car Crash", FAStyle.Solid, "\uf5e1")] CarCrashSolid,
        /// <summary>Car Side - car-side - f5e4</summary>
        [FAIcon("car-side", "Car Side", FAStyle.Solid, "\uf5e4")] CarSideSolid,
        /// <summary>Caret Down - caret-down - f0d7</summary>
        [FAIcon("caret-down", "Caret Down", FAStyle.Solid, "\uf0d7")] CaretDownSolid,
        /// <summary>Caret Left - caret-left - f0d9</summary>
        [FAIcon("caret-left", "Caret Left", FAStyle.Solid, "\uf0d9")] CaretLeftSolid,
        /// <summary>Caret Right - caret-right - f0da</summary>
        [FAIcon("caret-right", "Caret Right", FAStyle.Solid, "\uf0da")] CaretRightSolid,
        /// <summary>Caret Square Down - caret-square-down - f150</summary>
        [FAIcon("caret-square-down", "Caret Square Down", FAStyle.Solid, "\uf150")] CaretSquareDownSolid,
        /// <summary>Caret Square Down - caret-square-down - f150</summary>
        [FAIcon("caret-square-down", "Caret Square Down", FAStyle.Regular, "\uf150")] CaretSquareDownRegular,
        /// <summary>Caret Square Left - caret-square-left - f191</summary>
        [FAIcon("caret-square-left", "Caret Square Left", FAStyle.Solid, "\uf191")] CaretSquareLeftSolid,
        /// <summary>Caret Square Left - caret-square-left - f191</summary>
        [FAIcon("caret-square-left", "Caret Square Left", FAStyle.Regular, "\uf191")] CaretSquareLeftRegular,
        /// <summary>Caret Square Right - caret-square-right - f152</summary>
        [FAIcon("caret-square-right", "Caret Square Right", FAStyle.Solid, "\uf152")] CaretSquareRightSolid,
        /// <summary>Caret Square Right - caret-square-right - f152</summary>
        [FAIcon("caret-square-right", "Caret Square Right", FAStyle.Regular, "\uf152")] CaretSquareRightRegular,
        /// <summary>Caret Square Up - caret-square-up - f151</summary>
        [FAIcon("caret-square-up", "Caret Square Up", FAStyle.Solid, "\uf151")] CaretSquareUpSolid,
        /// <summary>Caret Square Up - caret-square-up - f151</summary>
        [FAIcon("caret-square-up", "Caret Square Up", FAStyle.Regular, "\uf151")] CaretSquareUpRegular,
        /// <summary>Caret Up - caret-up - f0d8</summary>
        [FAIcon("caret-up", "Caret Up", FAStyle.Solid, "\uf0d8")] CaretUpSolid,
        /// <summary>Shopping Cart Arrow Down - cart-arrow-down - f218</summary>
        [FAIcon("cart-arrow-down", "Shopping Cart Arrow Down", FAStyle.Solid, "\uf218")] CartArrowDownSolid,
        /// <summary>Add to Shopping Cart - cart-plus - f217</summary>
        [FAIcon("cart-plus", "Add to Shopping Cart", FAStyle.Solid, "\uf217")] CartPlusSolid,
        /// <summary>Amazon Pay Credit Card - cc-amazon-pay - f42d</summary>
        [FAIcon("cc-amazon-pay", "Amazon Pay Credit Card", FAStyle.Brands, "\uf42d")] CcAmazonPayBrands,
        /// <summary>American Express Credit Card - cc-amex - f1f3</summary>
        [FAIcon("cc-amex", "American Express Credit Card", FAStyle.Brands, "\uf1f3")] CcAmexBrands,
        /// <summary>Apple Pay Credit Card - cc-apple-pay - f416</summary>
        [FAIcon("cc-apple-pay", "Apple Pay Credit Card", FAStyle.Brands, "\uf416")] CcApplePayBrands,
        /// <summary>Diner's Club Credit Card - cc-diners-club - f24c</summary>
        [FAIcon("cc-diners-club", "Diner's Club Credit Card", FAStyle.Brands, "\uf24c")] CcDinersClubBrands,
        /// <summary>Discover Credit Card - cc-discover - f1f2</summary>
        [FAIcon("cc-discover", "Discover Credit Card", FAStyle.Brands, "\uf1f2")] CcDiscoverBrands,
        /// <summary>JCB Credit Card - cc-jcb - f24b</summary>
        [FAIcon("cc-jcb", "JCB Credit Card", FAStyle.Brands, "\uf24b")] CcJcbBrands,
        /// <summary>MasterCard Credit Card - cc-mastercard - f1f1</summary>
        [FAIcon("cc-mastercard", "MasterCard Credit Card", FAStyle.Brands, "\uf1f1")] CcMastercardBrands,
        /// <summary>Paypal Credit Card - cc-paypal - f1f4</summary>
        [FAIcon("cc-paypal", "Paypal Credit Card", FAStyle.Brands, "\uf1f4")] CcPaypalBrands,
        /// <summary>Stripe Credit Card - cc-stripe - f1f5</summary>
        [FAIcon("cc-stripe", "Stripe Credit Card", FAStyle.Brands, "\uf1f5")] CcStripeBrands,
        /// <summary>Visa Credit Card - cc-visa - f1f0</summary>
        [FAIcon("cc-visa", "Visa Credit Card", FAStyle.Brands, "\uf1f0")] CcVisaBrands,
        /// <summary>Centercode - centercode - f380</summary>
        [FAIcon("centercode", "Centercode", FAStyle.Brands, "\uf380")] CentercodeBrands,
        /// <summary>certificate - certificate - f0a3</summary>
        [FAIcon("certificate", "certificate", FAStyle.Solid, "\uf0a3")] CertificateSolid,
        /// <summary>Chalkboard - chalkboard - f51b</summary>
        [FAIcon("chalkboard", "Chalkboard", FAStyle.Solid, "\uf51b")] ChalkboardSolid,
        /// <summary>Chalkboard Teacher - chalkboard-teacher - f51c</summary>
        [FAIcon("chalkboard-teacher", "Chalkboard Teacher", FAStyle.Solid, "\uf51c")] ChalkboardTeacherSolid,
        /// <summary>Charging Station - charging-station - f5e7</summary>
        [FAIcon("charging-station", "Charging Station", FAStyle.Solid, "\uf5e7")] ChargingStationSolid,
        /// <summary>Area Chart - chart-area - f1fe</summary>
        [FAIcon("chart-area", "Area Chart", FAStyle.Solid, "\uf1fe")] ChartAreaSolid,
        /// <summary>Bar Chart - chart-bar - f080</summary>
        [FAIcon("chart-bar", "Bar Chart", FAStyle.Solid, "\uf080")] ChartBarSolid,
        /// <summary>Bar Chart - chart-bar - f080</summary>
        [FAIcon("chart-bar", "Bar Chart", FAStyle.Regular, "\uf080")] ChartBarRegular,
        /// <summary>Line Chart - chart-line - f201</summary>
        [FAIcon("chart-line", "Line Chart", FAStyle.Solid, "\uf201")] ChartLineSolid,
        /// <summary>Pie Chart - chart-pie - f200</summary>
        [FAIcon("chart-pie", "Pie Chart", FAStyle.Solid, "\uf200")] ChartPieSolid,
        /// <summary>Check - check - f00c</summary>
        [FAIcon("check", "Check", FAStyle.Solid, "\uf00c")] CheckSolid,
        /// <summary>Check Circle - check-circle - f058</summary>
        [FAIcon("check-circle", "Check Circle", FAStyle.Solid, "\uf058")] CheckCircleSolid,
        /// <summary>Check Circle - check-circle - f058</summary>
        [FAIcon("check-circle", "Check Circle", FAStyle.Regular, "\uf058")] CheckCircleRegular,
        /// <summary>Check Double - check-double - f560</summary>
        [FAIcon("check-double", "Check Double", FAStyle.Solid, "\uf560")] CheckDoubleSolid,
        /// <summary>Check Square - check-square - f14a</summary>
        [FAIcon("check-square", "Check Square", FAStyle.Solid, "\uf14a")] CheckSquareSolid,
        /// <summary>Check Square - check-square - f14a</summary>
        [FAIcon("check-square", "Check Square", FAStyle.Regular, "\uf14a")] CheckSquareRegular,
        /// <summary>Chess - chess - f439</summary>
        [FAIcon("chess", "Chess", FAStyle.Solid, "\uf439")] ChessSolid,
        /// <summary>Chess Bishop - chess-bishop - f43a</summary>
        [FAIcon("chess-bishop", "Chess Bishop", FAStyle.Solid, "\uf43a")] ChessBishopSolid,
        /// <summary>Chess Board - chess-board - f43c</summary>
        [FAIcon("chess-board", "Chess Board", FAStyle.Solid, "\uf43c")] ChessBoardSolid,
        /// <summary>Chess King - chess-king - f43f</summary>
        [FAIcon("chess-king", "Chess King", FAStyle.Solid, "\uf43f")] ChessKingSolid,
        /// <summary>Chess Knight - chess-knight - f441</summary>
        [FAIcon("chess-knight", "Chess Knight", FAStyle.Solid, "\uf441")] ChessKnightSolid,
        /// <summary>Chess Pawn - chess-pawn - f443</summary>
        [FAIcon("chess-pawn", "Chess Pawn", FAStyle.Solid, "\uf443")] ChessPawnSolid,
        /// <summary>Chess Queen - chess-queen - f445</summary>
        [FAIcon("chess-queen", "Chess Queen", FAStyle.Solid, "\uf445")] ChessQueenSolid,
        /// <summary>Chess Rook - chess-rook - f447</summary>
        [FAIcon("chess-rook", "Chess Rook", FAStyle.Solid, "\uf447")] ChessRookSolid,
        /// <summary>Chevron Circle Down - chevron-circle-down - f13a</summary>
        [FAIcon("chevron-circle-down", "Chevron Circle Down", FAStyle.Solid, "\uf13a")] ChevronCircleDownSolid,
        /// <summary>Chevron Circle Left - chevron-circle-left - f137</summary>
        [FAIcon("chevron-circle-left", "Chevron Circle Left", FAStyle.Solid, "\uf137")] ChevronCircleLeftSolid,
        /// <summary>Chevron Circle Right - chevron-circle-right - f138</summary>
        [FAIcon("chevron-circle-right", "Chevron Circle Right", FAStyle.Solid, "\uf138")] ChevronCircleRightSolid,
        /// <summary>Chevron Circle Up - chevron-circle-up - f139</summary>
        [FAIcon("chevron-circle-up", "Chevron Circle Up", FAStyle.Solid, "\uf139")] ChevronCircleUpSolid,
        /// <summary>chevron-down - chevron-down - f078</summary>
        [FAIcon("chevron-down", "chevron-down", FAStyle.Solid, "\uf078")] ChevronDownSolid,
        /// <summary>chevron-left - chevron-left - f053</summary>
        [FAIcon("chevron-left", "chevron-left", FAStyle.Solid, "\uf053")] ChevronLeftSolid,
        /// <summary>chevron-right - chevron-right - f054</summary>
        [FAIcon("chevron-right", "chevron-right", FAStyle.Solid, "\uf054")] ChevronRightSolid,
        /// <summary>chevron-up - chevron-up - f077</summary>
        [FAIcon("chevron-up", "chevron-up", FAStyle.Solid, "\uf077")] ChevronUpSolid,
        /// <summary>Child - child - f1ae</summary>
        [FAIcon("child", "Child", FAStyle.Solid, "\uf1ae")] ChildSolid,
        /// <summary>Chrome - chrome - f268</summary>
        [FAIcon("chrome", "Chrome", FAStyle.Brands, "\uf268")] ChromeBrands,
        /// <summary>Church - church - f51d</summary>
        [FAIcon("church", "Church", FAStyle.Solid, "\uf51d")] ChurchSolid,
        /// <summary>Circle - circle - f111</summary>
        [FAIcon("circle", "Circle", FAStyle.Solid, "\uf111")] CircleSolid,
        /// <summary>Circle - circle - f111</summary>
        [FAIcon("circle", "Circle", FAStyle.Regular, "\uf111")] CircleRegular,
        /// <summary>Circle Notched - circle-notch - f1ce</summary>
        [FAIcon("circle-notch", "Circle Notched", FAStyle.Solid, "\uf1ce")] CircleNotchSolid,
        /// <summary>Clipboard - clipboard - f328</summary>
        [FAIcon("clipboard", "Clipboard", FAStyle.Solid, "\uf328")] ClipboardSolid,
        /// <summary>Clipboard - clipboard - f328</summary>
        [FAIcon("clipboard", "Clipboard", FAStyle.Regular, "\uf328")] ClipboardRegular,
        /// <summary>Clipboard Check - clipboard-check - f46c</summary>
        [FAIcon("clipboard-check", "Clipboard Check", FAStyle.Solid, "\uf46c")] ClipboardCheckSolid,
        /// <summary>Clipboard List - clipboard-list - f46d</summary>
        [FAIcon("clipboard-list", "Clipboard List", FAStyle.Solid, "\uf46d")] ClipboardListSolid,
        /// <summary>Clock - clock - f017</summary>
        [FAIcon("clock", "Clock", FAStyle.Solid, "\uf017")] ClockSolid,
        /// <summary>Clock - clock - f017</summary>
        [FAIcon("clock", "Clock", FAStyle.Regular, "\uf017")] ClockRegular,
        /// <summary>Clone - clone - f24d</summary>
        [FAIcon("clone", "Clone", FAStyle.Solid, "\uf24d")] CloneSolid,
        /// <summary>Clone - clone - f24d</summary>
        [FAIcon("clone", "Clone", FAStyle.Regular, "\uf24d")] CloneRegular,
        /// <summary>Closed Captioning - closed-captioning - f20a</summary>
        [FAIcon("closed-captioning", "Closed Captioning", FAStyle.Solid, "\uf20a")] ClosedCaptioningSolid,
        /// <summary>Closed Captioning - closed-captioning - f20a</summary>
        [FAIcon("closed-captioning", "Closed Captioning", FAStyle.Regular, "\uf20a")] ClosedCaptioningRegular,
        /// <summary>Cloud - cloud - f0c2</summary>
        [FAIcon("cloud", "Cloud", FAStyle.Solid, "\uf0c2")] CloudSolid,
        /// <summary>Alternate Cloud Download - cloud-download-alt - f381</summary>
        [FAIcon("cloud-download-alt", "Alternate Cloud Download", FAStyle.Solid, "\uf381")] CloudDownloadAltSolid,
        /// <summary>Alternate Cloud Upload - cloud-upload-alt - f382</summary>
        [FAIcon("cloud-upload-alt", "Alternate Cloud Upload", FAStyle.Solid, "\uf382")] CloudUploadAltSolid,
        /// <summary>cloudscale.ch - cloudscale - f383</summary>
        [FAIcon("cloudscale", "cloudscale.ch", FAStyle.Brands, "\uf383")] CloudscaleBrands,
        /// <summary>Cloudsmith - cloudsmith - f384</summary>
        [FAIcon("cloudsmith", "Cloudsmith", FAStyle.Brands, "\uf384")] CloudsmithBrands,
        /// <summary>cloudversify - cloudversify - f385</summary>
        [FAIcon("cloudversify", "cloudversify", FAStyle.Brands, "\uf385")] CloudversifyBrands,
        /// <summary>Cocktail - cocktail - f561</summary>
        [FAIcon("cocktail", "Cocktail", FAStyle.Solid, "\uf561")] CocktailSolid,
        /// <summary>Code - code - f121</summary>
        [FAIcon("code", "Code", FAStyle.Solid, "\uf121")] CodeSolid,
        /// <summary>Code Branch - code-branch - f126</summary>
        [FAIcon("code-branch", "Code Branch", FAStyle.Solid, "\uf126")] CodeBranchSolid,
        /// <summary>Codepen - codepen - f1cb</summary>
        [FAIcon("codepen", "Codepen", FAStyle.Brands, "\uf1cb")] CodepenBrands,
        /// <summary>Codie Pie - codiepie - f284</summary>
        [FAIcon("codiepie", "Codie Pie", FAStyle.Brands, "\uf284")] CodiepieBrands,
        /// <summary>Coffee - coffee - f0f4</summary>
        [FAIcon("coffee", "Coffee", FAStyle.Solid, "\uf0f4")] CoffeeSolid,
        /// <summary>cog - cog - f013</summary>
        [FAIcon("cog", "cog", FAStyle.Solid, "\uf013")] CogSolid,
        /// <summary>cogs - cogs - f085</summary>
        [FAIcon("cogs", "cogs", FAStyle.Solid, "\uf085")] CogsSolid,
        /// <summary>Coins - coins - f51e</summary>
        [FAIcon("coins", "Coins", FAStyle.Solid, "\uf51e")] CoinsSolid,
        /// <summary>Columns - columns - f0db</summary>
        [FAIcon("columns", "Columns", FAStyle.Solid, "\uf0db")] ColumnsSolid,
        /// <summary>comment - comment - f075</summary>
        [FAIcon("comment", "comment", FAStyle.Solid, "\uf075")] CommentSolid,
        /// <summary>comment - comment - f075</summary>
        [FAIcon("comment", "comment", FAStyle.Regular, "\uf075")] CommentRegular,
        /// <summary>Alternate Comment - comment-alt - f27a</summary>
        [FAIcon("comment-alt", "Alternate Comment", FAStyle.Solid, "\uf27a")] CommentAltSolid,
        /// <summary>Alternate Comment - comment-alt - f27a</summary>
        [FAIcon("comment-alt", "Alternate Comment", FAStyle.Regular, "\uf27a")] CommentAltRegular,
        /// <summary>Comment Dots - comment-dots - f4ad</summary>
        [FAIcon("comment-dots", "Comment Dots", FAStyle.Solid, "\uf4ad")] CommentDotsSolid,
        /// <summary>Comment Dots - comment-dots - f4ad</summary>
        [FAIcon("comment-dots", "Comment Dots", FAStyle.Regular, "\uf4ad")] CommentDotsRegular,
        /// <summary>Comment Slash - comment-slash - f4b3</summary>
        [FAIcon("comment-slash", "Comment Slash", FAStyle.Solid, "\uf4b3")] CommentSlashSolid,
        /// <summary>comments - comments - f086</summary>
        [FAIcon("comments", "comments", FAStyle.Solid, "\uf086")] CommentsSolid,
        /// <summary>comments - comments - f086</summary>
        [FAIcon("comments", "comments", FAStyle.Regular, "\uf086")] CommentsRegular,
        /// <summary>Compact Disc - compact-disc - f51f</summary>
        [FAIcon("compact-disc", "Compact Disc", FAStyle.Solid, "\uf51f")] CompactDiscSolid,
        /// <summary>Compass - compass - f14e</summary>
        [FAIcon("compass", "Compass", FAStyle.Solid, "\uf14e")] CompassSolid,
        /// <summary>Compass - compass - f14e</summary>
        [FAIcon("compass", "Compass", FAStyle.Regular, "\uf14e")] CompassRegular,
        /// <summary>Compress - compress - f066</summary>
        [FAIcon("compress", "Compress", FAStyle.Solid, "\uf066")] CompressSolid,
        /// <summary>Concierge Bell - concierge-bell - f562</summary>
        [FAIcon("concierge-bell", "Concierge Bell", FAStyle.Solid, "\uf562")] ConciergeBellSolid,
        /// <summary>Connect Develop - connectdevelop - f20e</summary>
        [FAIcon("connectdevelop", "Connect Develop", FAStyle.Brands, "\uf20e")] ConnectdevelopBrands,
        /// <summary>Contao - contao - f26d</summary>
        [FAIcon("contao", "Contao", FAStyle.Brands, "\uf26d")] ContaoBrands,
        /// <summary>Cookie - cookie - f563</summary>
        [FAIcon("cookie", "Cookie", FAStyle.Solid, "\uf563")] CookieSolid,
        /// <summary>Cookie Bite - cookie-bite - f564</summary>
        [FAIcon("cookie-bite", "Cookie Bite", FAStyle.Solid, "\uf564")] CookieBiteSolid,
        /// <summary>Copy - copy - f0c5</summary>
        [FAIcon("copy", "Copy", FAStyle.Solid, "\uf0c5")] CopySolid,
        /// <summary>Copy - copy - f0c5</summary>
        [FAIcon("copy", "Copy", FAStyle.Regular, "\uf0c5")] CopyRegular,
        /// <summary>Copyright - copyright - f1f9</summary>
        [FAIcon("copyright", "Copyright", FAStyle.Solid, "\uf1f9")] CopyrightSolid,
        /// <summary>Copyright - copyright - f1f9</summary>
        [FAIcon("copyright", "Copyright", FAStyle.Regular, "\uf1f9")] CopyrightRegular,
        /// <summary>Couch - couch - f4b8</summary>
        [FAIcon("couch", "Couch", FAStyle.Solid, "\uf4b8")] CouchSolid,
        /// <summary>cPanel - cpanel - f388</summary>
        [FAIcon("cpanel", "cPanel", FAStyle.Brands, "\uf388")] CpanelBrands,
        /// <summary>Creative Commons - creative-commons - f25e</summary>
        [FAIcon("creative-commons", "Creative Commons", FAStyle.Brands, "\uf25e")] CreativeCommonsBrands,
        /// <summary>Creative Commons Attribution - creative-commons-by - f4e7</summary>
        [FAIcon("creative-commons-by", "Creative Commons Attribution", FAStyle.Brands, "\uf4e7")] CreativeCommonsByBrands,
        /// <summary>Creative Commons Noncommercial - creative-commons-nc - f4e8</summary>
        [FAIcon("creative-commons-nc", "Creative Commons Noncommercial", FAStyle.Brands, "\uf4e8")] CreativeCommonsNcBrands,
        /// <summary>Creative Commons Noncommercial (Euro Sign) - creative-commons-nc-eu - f4e9</summary>
        [FAIcon("creative-commons-nc-eu", "Creative Commons Noncommercial (Euro Sign)", FAStyle.Brands, "\uf4e9")] CreativeCommonsNcEuBrands,
        /// <summary>Creative Commons Noncommercial (Yen Sign) - creative-commons-nc-jp - f4ea</summary>
        [FAIcon("creative-commons-nc-jp", "Creative Commons Noncommercial (Yen Sign)", FAStyle.Brands, "\uf4ea")] CreativeCommonsNcJpBrands,
        /// <summary>Creative Commons No Derivative Works - creative-commons-nd - f4eb</summary>
        [FAIcon("creative-commons-nd", "Creative Commons No Derivative Works", FAStyle.Brands, "\uf4eb")] CreativeCommonsNdBrands,
        /// <summary>Creative Commons Public Domain - creative-commons-pd - f4ec</summary>
        [FAIcon("creative-commons-pd", "Creative Commons Public Domain", FAStyle.Brands, "\uf4ec")] CreativeCommonsPdBrands,
        /// <summary>Creative Commons Public Domain Alternate - creative-commons-pd-alt - f4ed</summary>
        [FAIcon("creative-commons-pd-alt", "Creative Commons Public Domain Alternate", FAStyle.Brands, "\uf4ed")] CreativeCommonsPdAltBrands,
        /// <summary>Creative Commons Remix - creative-commons-remix - f4ee</summary>
        [FAIcon("creative-commons-remix", "Creative Commons Remix", FAStyle.Brands, "\uf4ee")] CreativeCommonsRemixBrands,
        /// <summary>Creative Commons Share Alike - creative-commons-sa - f4ef</summary>
        [FAIcon("creative-commons-sa", "Creative Commons Share Alike", FAStyle.Brands, "\uf4ef")] CreativeCommonsSaBrands,
        /// <summary>Creative Commons Sampling - creative-commons-sampling - f4f0</summary>
        [FAIcon("creative-commons-sampling", "Creative Commons Sampling", FAStyle.Brands, "\uf4f0")] CreativeCommonsSamplingBrands,
        /// <summary>Creative Commons Sampling + - creative-commons-sampling-plus - f4f1</summary>
        [FAIcon("creative-commons-sampling-plus", "Creative Commons Sampling +", FAStyle.Brands, "\uf4f1")] CreativeCommonsSamplingPlusBrands,
        /// <summary>Creative Commons Share - creative-commons-share - f4f2</summary>
        [FAIcon("creative-commons-share", "Creative Commons Share", FAStyle.Brands, "\uf4f2")] CreativeCommonsShareBrands,
        /// <summary>Credit Card - credit-card - f09d</summary>
        [FAIcon("credit-card", "Credit Card", FAStyle.Solid, "\uf09d")] CreditCardSolid,
        /// <summary>Credit Card - credit-card - f09d</summary>
        [FAIcon("credit-card", "Credit Card", FAStyle.Regular, "\uf09d")] CreditCardRegular,
        /// <summary>crop - crop - f125</summary>
        [FAIcon("crop", "crop", FAStyle.Solid, "\uf125")] CropSolid,
        /// <summary>Alternate Crop - crop-alt - f565</summary>
        [FAIcon("crop-alt", "Alternate Crop", FAStyle.Solid, "\uf565")] CropAltSolid,
        /// <summary>Crosshairs - crosshairs - f05b</summary>
        [FAIcon("crosshairs", "Crosshairs", FAStyle.Solid, "\uf05b")] CrosshairsSolid,
        /// <summary>Crow - crow - f520</summary>
        [FAIcon("crow", "Crow", FAStyle.Solid, "\uf520")] CrowSolid,
        /// <summary>Crown - crown - f521</summary>
        [FAIcon("crown", "Crown", FAStyle.Solid, "\uf521")] CrownSolid,
        /// <summary>CSS 3 Logo - css3 - f13c</summary>
        [FAIcon("css3", "CSS 3 Logo", FAStyle.Brands, "\uf13c")] Css3Brands,
        /// <summary>Alternate CSS3 Logo - css3-alt - f38b</summary>
        [FAIcon("css3-alt", "Alternate CSS3 Logo", FAStyle.Brands, "\uf38b")] Css3AltBrands,
        /// <summary>Cube - cube - f1b2</summary>
        [FAIcon("cube", "Cube", FAStyle.Solid, "\uf1b2")] CubeSolid,
        /// <summary>Cubes - cubes - f1b3</summary>
        [FAIcon("cubes", "Cubes", FAStyle.Solid, "\uf1b3")] CubesSolid,
        /// <summary>Cut - cut - f0c4</summary>
        [FAIcon("cut", "Cut", FAStyle.Solid, "\uf0c4")] CutSolid,
        /// <summary>Cuttlefish - cuttlefish - f38c</summary>
        [FAIcon("cuttlefish", "Cuttlefish", FAStyle.Brands, "\uf38c")] CuttlefishBrands,
        /// <summary>Dungeons & Dragons - d-and-d - f38d</summary>
        [FAIcon("d-and-d", "Dungeons & Dragons", FAStyle.Brands, "\uf38d")] DAndDBrands,
        /// <summary>DashCube - dashcube - f210</summary>
        [FAIcon("dashcube", "DashCube", FAStyle.Brands, "\uf210")] DashcubeBrands,
        /// <summary>Database - database - f1c0</summary>
        [FAIcon("database", "Database", FAStyle.Solid, "\uf1c0")] DatabaseSolid,
        /// <summary>Deaf - deaf - f2a4</summary>
        [FAIcon("deaf", "Deaf", FAStyle.Solid, "\uf2a4")] DeafSolid,
        /// <summary>Delicious Logo - delicious - f1a5</summary>
        [FAIcon("delicious", "Delicious Logo", FAStyle.Brands, "\uf1a5")] DeliciousBrands,
        /// <summary>deploy.dog - deploydog - f38e</summary>
        [FAIcon("deploydog", "deploy.dog", FAStyle.Brands, "\uf38e")] DeploydogBrands,
        /// <summary>Deskpro - deskpro - f38f</summary>
        [FAIcon("deskpro", "Deskpro", FAStyle.Brands, "\uf38f")] DeskproBrands,
        /// <summary>Desktop - desktop - f108</summary>
        [FAIcon("desktop", "Desktop", FAStyle.Solid, "\uf108")] DesktopSolid,
        /// <summary>deviantART - deviantart - f1bd</summary>
        [FAIcon("deviantart", "deviantART", FAStyle.Brands, "\uf1bd")] DeviantartBrands,
        /// <summary>Diagnoses - diagnoses - f470</summary>
        [FAIcon("diagnoses", "Diagnoses", FAStyle.Solid, "\uf470")] DiagnosesSolid,
        /// <summary>Dice - dice - f522</summary>
        [FAIcon("dice", "Dice", FAStyle.Solid, "\uf522")] DiceSolid,
        /// <summary>Dice Five - dice-five - f523</summary>
        [FAIcon("dice-five", "Dice Five", FAStyle.Solid, "\uf523")] DiceFiveSolid,
        /// <summary>Dice Four - dice-four - f524</summary>
        [FAIcon("dice-four", "Dice Four", FAStyle.Solid, "\uf524")] DiceFourSolid,
        /// <summary>Dice One - dice-one - f525</summary>
        [FAIcon("dice-one", "Dice One", FAStyle.Solid, "\uf525")] DiceOneSolid,
        /// <summary>Dice Six - dice-six - f526</summary>
        [FAIcon("dice-six", "Dice Six", FAStyle.Solid, "\uf526")] DiceSixSolid,
        /// <summary>Dice Three - dice-three - f527</summary>
        [FAIcon("dice-three", "Dice Three", FAStyle.Solid, "\uf527")] DiceThreeSolid,
        /// <summary>Dice Two - dice-two - f528</summary>
        [FAIcon("dice-two", "Dice Two", FAStyle.Solid, "\uf528")] DiceTwoSolid,
        /// <summary>Digg Logo - digg - f1a6</summary>
        [FAIcon("digg", "Digg Logo", FAStyle.Brands, "\uf1a6")] DiggBrands,
        /// <summary>Digital Ocean - digital-ocean - f391</summary>
        [FAIcon("digital-ocean", "Digital Ocean", FAStyle.Brands, "\uf391")] DigitalOceanBrands,
        /// <summary>Digital Tachograph - digital-tachograph - f566</summary>
        [FAIcon("digital-tachograph", "Digital Tachograph", FAStyle.Solid, "\uf566")] DigitalTachographSolid,
        /// <summary>Directions - directions - f5eb</summary>
        [FAIcon("directions", "Directions", FAStyle.Solid, "\uf5eb")] DirectionsSolid,
        /// <summary>Discord - discord - f392</summary>
        [FAIcon("discord", "Discord", FAStyle.Brands, "\uf392")] DiscordBrands,
        /// <summary>Discourse - discourse - f393</summary>
        [FAIcon("discourse", "Discourse", FAStyle.Brands, "\uf393")] DiscourseBrands,
        /// <summary>Divide - divide - f529</summary>
        [FAIcon("divide", "Divide", FAStyle.Solid, "\uf529")] DivideSolid,
        /// <summary>Dizzy Face - dizzy - f567</summary>
        [FAIcon("dizzy", "Dizzy Face", FAStyle.Solid, "\uf567")] DizzySolid,
        /// <summary>Dizzy Face - dizzy - f567</summary>
        [FAIcon("dizzy", "Dizzy Face", FAStyle.Regular, "\uf567")] DizzyRegular,
        /// <summary>DNA - dna - f471</summary>
        [FAIcon("dna", "DNA", FAStyle.Solid, "\uf471")] DnaSolid,
        /// <summary>DocHub - dochub - f394</summary>
        [FAIcon("dochub", "DocHub", FAStyle.Brands, "\uf394")] DochubBrands,
        /// <summary>Docker - docker - f395</summary>
        [FAIcon("docker", "Docker", FAStyle.Brands, "\uf395")] DockerBrands,
        /// <summary>Dollar Sign - dollar-sign - f155</summary>
        [FAIcon("dollar-sign", "Dollar Sign", FAStyle.Solid, "\uf155")] DollarSignSolid,
        /// <summary>Dolly - dolly - f472</summary>
        [FAIcon("dolly", "Dolly", FAStyle.Solid, "\uf472")] DollySolid,
        /// <summary>Dolly Flatbed - dolly-flatbed - f474</summary>
        [FAIcon("dolly-flatbed", "Dolly Flatbed", FAStyle.Solid, "\uf474")] DollyFlatbedSolid,
        /// <summary>Donate - donate - f4b9</summary>
        [FAIcon("donate", "Donate", FAStyle.Solid, "\uf4b9")] DonateSolid,
        /// <summary>Door Closed - door-closed - f52a</summary>
        [FAIcon("door-closed", "Door Closed", FAStyle.Solid, "\uf52a")] DoorClosedSolid,
        /// <summary>Door Open - door-open - f52b</summary>
        [FAIcon("door-open", "Door Open", FAStyle.Solid, "\uf52b")] DoorOpenSolid,
        /// <summary>Dot Circle - dot-circle - f192</summary>
        [FAIcon("dot-circle", "Dot Circle", FAStyle.Solid, "\uf192")] DotCircleSolid,
        /// <summary>Dot Circle - dot-circle - f192</summary>
        [FAIcon("dot-circle", "Dot Circle", FAStyle.Regular, "\uf192")] DotCircleRegular,
        /// <summary>Dove - dove - f4ba</summary>
        [FAIcon("dove", "Dove", FAStyle.Solid, "\uf4ba")] DoveSolid,
        /// <summary>Download - download - f019</summary>
        [FAIcon("download", "Download", FAStyle.Solid, "\uf019")] DownloadSolid,
        /// <summary>Draft2digital - draft2digital - f396</summary>
        [FAIcon("draft2digital", "Draft2digital", FAStyle.Brands, "\uf396")] Draft2digitalBrands,
        /// <summary>Drafting Compass - drafting-compass - f568</summary>
        [FAIcon("drafting-compass", "Drafting Compass", FAStyle.Solid, "\uf568")] DraftingCompassSolid,
        /// <summary>Draw Polygon - draw-polygon - f5ee</summary>
        [FAIcon("draw-polygon", "Draw Polygon", FAStyle.Solid, "\uf5ee")] DrawPolygonSolid,
        /// <summary>Dribbble - dribbble - f17d</summary>
        [FAIcon("dribbble", "Dribbble", FAStyle.Brands, "\uf17d")] DribbbleBrands,
        /// <summary>Dribbble Square - dribbble-square - f397</summary>
        [FAIcon("dribbble-square", "Dribbble Square", FAStyle.Brands, "\uf397")] DribbbleSquareBrands,
        /// <summary>Dropbox - dropbox - f16b</summary>
        [FAIcon("dropbox", "Dropbox", FAStyle.Brands, "\uf16b")] DropboxBrands,
        /// <summary>Drum - drum - f569</summary>
        [FAIcon("drum", "Drum", FAStyle.Solid, "\uf569")] DrumSolid,
        /// <summary>Drum Steelpan - drum-steelpan - f56a</summary>
        [FAIcon("drum-steelpan", "Drum Steelpan", FAStyle.Solid, "\uf56a")] DrumSteelpanSolid,
        /// <summary>Drupal Logo - drupal - f1a9</summary>
        [FAIcon("drupal", "Drupal Logo", FAStyle.Brands, "\uf1a9")] DrupalBrands,
        /// <summary>Dumbbell - dumbbell - f44b</summary>
        [FAIcon("dumbbell", "Dumbbell", FAStyle.Solid, "\uf44b")] DumbbellSolid,
        /// <summary>Dyalog - dyalog - f399</summary>
        [FAIcon("dyalog", "Dyalog", FAStyle.Brands, "\uf399")] DyalogBrands,
        /// <summary>Earlybirds - earlybirds - f39a</summary>
        [FAIcon("earlybirds", "Earlybirds", FAStyle.Brands, "\uf39a")] EarlybirdsBrands,
        /// <summary>eBay - ebay - f4f4</summary>
        [FAIcon("ebay", "eBay", FAStyle.Brands, "\uf4f4")] EbayBrands,
        /// <summary>Edge Browser - edge - f282</summary>
        [FAIcon("edge", "Edge Browser", FAStyle.Brands, "\uf282")] EdgeBrands,
        /// <summary>Edit - edit - f044</summary>
        [FAIcon("edit", "Edit", FAStyle.Solid, "\uf044")] EditSolid,
        /// <summary>Edit - edit - f044</summary>
        [FAIcon("edit", "Edit", FAStyle.Regular, "\uf044")] EditRegular,
        /// <summary>eject - eject - f052</summary>
        [FAIcon("eject", "eject", FAStyle.Solid, "\uf052")] EjectSolid,
        /// <summary>Elementor - elementor - f430</summary>
        [FAIcon("elementor", "Elementor", FAStyle.Brands, "\uf430")] ElementorBrands,
        /// <summary>Horizontal Ellipsis - ellipsis-h - f141</summary>
        [FAIcon("ellipsis-h", "Horizontal Ellipsis", FAStyle.Solid, "\uf141")] EllipsisHSolid,
        /// <summary>Vertical Ellipsis - ellipsis-v - f142</summary>
        [FAIcon("ellipsis-v", "Vertical Ellipsis", FAStyle.Solid, "\uf142")] EllipsisVSolid,
        /// <summary>Ello - ello - f5f1</summary>
        [FAIcon("ello", "Ello", FAStyle.Brands, "\uf5f1")] ElloBrands,
        /// <summary>Ember - ember - f423</summary>
        [FAIcon("ember", "Ember", FAStyle.Brands, "\uf423")] EmberBrands,
        /// <summary>Galactic Empire - empire - f1d1</summary>
        [FAIcon("empire", "Galactic Empire", FAStyle.Brands, "\uf1d1")] EmpireBrands,
        /// <summary>Envelope - envelope - f0e0</summary>
        [FAIcon("envelope", "Envelope", FAStyle.Solid, "\uf0e0")] EnvelopeSolid,
        /// <summary>Envelope - envelope - f0e0</summary>
        [FAIcon("envelope", "Envelope", FAStyle.Regular, "\uf0e0")] EnvelopeRegular,
        /// <summary>Envelope Open - envelope-open - f2b6</summary>
        [FAIcon("envelope-open", "Envelope Open", FAStyle.Solid, "\uf2b6")] EnvelopeOpenSolid,
        /// <summary>Envelope Open - envelope-open - f2b6</summary>
        [FAIcon("envelope-open", "Envelope Open", FAStyle.Regular, "\uf2b6")] EnvelopeOpenRegular,
        /// <summary>Envelope Square - envelope-square - f199</summary>
        [FAIcon("envelope-square", "Envelope Square", FAStyle.Solid, "\uf199")] EnvelopeSquareSolid,
        /// <summary>Envira Gallery - envira - f299</summary>
        [FAIcon("envira", "Envira Gallery", FAStyle.Brands, "\uf299")] EnviraBrands,
        /// <summary>Equals - equals - f52c</summary>
        [FAIcon("equals", "Equals", FAStyle.Solid, "\uf52c")] EqualsSolid,
        /// <summary>eraser - eraser - f12d</summary>
        [FAIcon("eraser", "eraser", FAStyle.Solid, "\uf12d")] EraserSolid,
        /// <summary>Erlang - erlang - f39d</summary>
        [FAIcon("erlang", "Erlang", FAStyle.Brands, "\uf39d")] ErlangBrands,
        /// <summary>Ethereum - ethereum - f42e</summary>
        [FAIcon("ethereum", "Ethereum", FAStyle.Brands, "\uf42e")] EthereumBrands,
        /// <summary>Etsy - etsy - f2d7</summary>
        [FAIcon("etsy", "Etsy", FAStyle.Brands, "\uf2d7")] EtsyBrands,
        /// <summary>Euro Sign - euro-sign - f153</summary>
        [FAIcon("euro-sign", "Euro Sign", FAStyle.Solid, "\uf153")] EuroSignSolid,
        /// <summary>Alternate Exchange - exchange-alt - f362</summary>
        [FAIcon("exchange-alt", "Alternate Exchange", FAStyle.Solid, "\uf362")] ExchangeAltSolid,
        /// <summary>exclamation - exclamation - f12a</summary>
        [FAIcon("exclamation", "exclamation", FAStyle.Solid, "\uf12a")] ExclamationSolid,
        /// <summary>Exclamation Circle - exclamation-circle - f06a</summary>
        [FAIcon("exclamation-circle", "Exclamation Circle", FAStyle.Solid, "\uf06a")] ExclamationCircleSolid,
        /// <summary>Exclamation Triangle - exclamation-triangle - f071</summary>
        [FAIcon("exclamation-triangle", "Exclamation Triangle", FAStyle.Solid, "\uf071")] ExclamationTriangleSolid,
        /// <summary>Expand - expand - f065</summary>
        [FAIcon("expand", "Expand", FAStyle.Solid, "\uf065")] ExpandSolid,
        /// <summary>Alternate Expand Arrows - expand-arrows-alt - f31e</summary>
        [FAIcon("expand-arrows-alt", "Alternate Expand Arrows", FAStyle.Solid, "\uf31e")] ExpandArrowsAltSolid,
        /// <summary>ExpeditedSSL - expeditedssl - f23e</summary>
        [FAIcon("expeditedssl", "ExpeditedSSL", FAStyle.Brands, "\uf23e")] ExpeditedsslBrands,
        /// <summary>Alternate External Link - external-link-alt - f35d</summary>
        [FAIcon("external-link-alt", "Alternate External Link", FAStyle.Solid, "\uf35d")] ExternalLinkAltSolid,
        /// <summary>Alternate External Link Square - external-link-square-alt - f360</summary>
        [FAIcon("external-link-square-alt", "Alternate External Link Square", FAStyle.Solid, "\uf360")] ExternalLinkSquareAltSolid,
        /// <summary>Eye - eye - f06e</summary>
        [FAIcon("eye", "Eye", FAStyle.Solid, "\uf06e")] EyeSolid,
        /// <summary>Eye - eye - f06e</summary>
        [FAIcon("eye", "Eye", FAStyle.Regular, "\uf06e")] EyeRegular,
        /// <summary>Eye Dropper - eye-dropper - f1fb</summary>
        [FAIcon("eye-dropper", "Eye Dropper", FAStyle.Solid, "\uf1fb")] EyeDropperSolid,
        /// <summary>Eye Slash - eye-slash - f070</summary>
        [FAIcon("eye-slash", "Eye Slash", FAStyle.Solid, "\uf070")] EyeSlashSolid,
        /// <summary>Eye Slash - eye-slash - f070</summary>
        [FAIcon("eye-slash", "Eye Slash", FAStyle.Regular, "\uf070")] EyeSlashRegular,
        /// <summary>Facebook - facebook - f09a</summary>
        [FAIcon("facebook", "Facebook", FAStyle.Brands, "\uf09a")] FacebookBrands,
        /// <summary>Facebook F - facebook-f - f39e</summary>
        [FAIcon("facebook-f", "Facebook F", FAStyle.Brands, "\uf39e")] FacebookFBrands,
        /// <summary>Facebook Messenger - facebook-messenger - f39f</summary>
        [FAIcon("facebook-messenger", "Facebook Messenger", FAStyle.Brands, "\uf39f")] FacebookMessengerBrands,
        /// <summary>Facebook Square - facebook-square - f082</summary>
        [FAIcon("facebook-square", "Facebook Square", FAStyle.Brands, "\uf082")] FacebookSquareBrands,
        /// <summary>fast-backward - fast-backward - f049</summary>
        [FAIcon("fast-backward", "fast-backward", FAStyle.Solid, "\uf049")] FastBackwardSolid,
        /// <summary>fast-forward - fast-forward - f050</summary>
        [FAIcon("fast-forward", "fast-forward", FAStyle.Solid, "\uf050")] FastForwardSolid,
        /// <summary>Fax - fax - f1ac</summary>
        [FAIcon("fax", "Fax", FAStyle.Solid, "\uf1ac")] FaxSolid,
        /// <summary>Feather - feather - f52d</summary>
        [FAIcon("feather", "Feather", FAStyle.Solid, "\uf52d")] FeatherSolid,
        /// <summary>Feather Alt - feather-alt - f56b</summary>
        [FAIcon("feather-alt", "Feather Alt", FAStyle.Solid, "\uf56b")] FeatherAltSolid,
        /// <summary>Female - female - f182</summary>
        [FAIcon("female", "Female", FAStyle.Solid, "\uf182")] FemaleSolid,
        /// <summary>fighter-jet - fighter-jet - f0fb</summary>
        [FAIcon("fighter-jet", "fighter-jet", FAStyle.Solid, "\uf0fb")] FighterJetSolid,
        /// <summary>File - file - f15b</summary>
        [FAIcon("file", "File", FAStyle.Solid, "\uf15b")] FileSolid,
        /// <summary>File - file - f15b</summary>
        [FAIcon("file", "File", FAStyle.Regular, "\uf15b")] FileRegular,
        /// <summary>Alternate File - file-alt - f15c</summary>
        [FAIcon("file-alt", "Alternate File", FAStyle.Solid, "\uf15c")] FileAltSolid,
        /// <summary>Alternate File - file-alt - f15c</summary>
        [FAIcon("file-alt", "Alternate File", FAStyle.Regular, "\uf15c")] FileAltRegular,
        /// <summary>Archive File - file-archive - f1c6</summary>
        [FAIcon("file-archive", "Archive File", FAStyle.Solid, "\uf1c6")] FileArchiveSolid,
        /// <summary>Archive File - file-archive - f1c6</summary>
        [FAIcon("file-archive", "Archive File", FAStyle.Regular, "\uf1c6")] FileArchiveRegular,
        /// <summary>Audio File - file-audio - f1c7</summary>
        [FAIcon("file-audio", "Audio File", FAStyle.Solid, "\uf1c7")] FileAudioSolid,
        /// <summary>Audio File - file-audio - f1c7</summary>
        [FAIcon("file-audio", "Audio File", FAStyle.Regular, "\uf1c7")] FileAudioRegular,
        /// <summary>Code File - file-code - f1c9</summary>
        [FAIcon("file-code", "Code File", FAStyle.Solid, "\uf1c9")] FileCodeSolid,
        /// <summary>Code File - file-code - f1c9</summary>
        [FAIcon("file-code", "Code File", FAStyle.Regular, "\uf1c9")] FileCodeRegular,
        /// <summary>File Contract - file-contract - f56c</summary>
        [FAIcon("file-contract", "File Contract", FAStyle.Solid, "\uf56c")] FileContractSolid,
        /// <summary>File Download - file-download - f56d</summary>
        [FAIcon("file-download", "File Download", FAStyle.Solid, "\uf56d")] FileDownloadSolid,
        /// <summary>Excel File - file-excel - f1c3</summary>
        [FAIcon("file-excel", "Excel File", FAStyle.Solid, "\uf1c3")] FileExcelSolid,
        /// <summary>Excel File - file-excel - f1c3</summary>
        [FAIcon("file-excel", "Excel File", FAStyle.Regular, "\uf1c3")] FileExcelRegular,
        /// <summary>File Export - file-export - f56e</summary>
        [FAIcon("file-export", "File Export", FAStyle.Solid, "\uf56e")] FileExportSolid,
        /// <summary>Image File - file-image - f1c5</summary>
        [FAIcon("file-image", "Image File", FAStyle.Solid, "\uf1c5")] FileImageSolid,
        /// <summary>Image File - file-image - f1c5</summary>
        [FAIcon("file-image", "Image File", FAStyle.Regular, "\uf1c5")] FileImageRegular,
        /// <summary>File Import - file-import - f56f</summary>
        [FAIcon("file-import", "File Import", FAStyle.Solid, "\uf56f")] FileImportSolid,
        /// <summary>File Invoice - file-invoice - f570</summary>
        [FAIcon("file-invoice", "File Invoice", FAStyle.Solid, "\uf570")] FileInvoiceSolid,
        /// <summary>File Invoice with US Dollar - file-invoice-dollar - f571</summary>
        [FAIcon("file-invoice-dollar", "File Invoice with US Dollar", FAStyle.Solid, "\uf571")] FileInvoiceDollarSolid,
        /// <summary>Medical File - file-medical - f477</summary>
        [FAIcon("file-medical", "Medical File", FAStyle.Solid, "\uf477")] FileMedicalSolid,
        /// <summary>Alternate Medical File - file-medical-alt - f478</summary>
        [FAIcon("file-medical-alt", "Alternate Medical File", FAStyle.Solid, "\uf478")] FileMedicalAltSolid,
        /// <summary>PDF File - file-pdf - f1c1</summary>
        [FAIcon("file-pdf", "PDF File", FAStyle.Solid, "\uf1c1")] FilePdfSolid,
        /// <summary>PDF File - file-pdf - f1c1</summary>
        [FAIcon("file-pdf", "PDF File", FAStyle.Regular, "\uf1c1")] FilePdfRegular,
        /// <summary>Powerpoint File - file-powerpoint - f1c4</summary>
        [FAIcon("file-powerpoint", "Powerpoint File", FAStyle.Solid, "\uf1c4")] FilePowerpointSolid,
        /// <summary>Powerpoint File - file-powerpoint - f1c4</summary>
        [FAIcon("file-powerpoint", "Powerpoint File", FAStyle.Regular, "\uf1c4")] FilePowerpointRegular,
        /// <summary>File Prescription - file-prescription - f572</summary>
        [FAIcon("file-prescription", "File Prescription", FAStyle.Solid, "\uf572")] FilePrescriptionSolid,
        /// <summary>File Signature - file-signature - f573</summary>
        [FAIcon("file-signature", "File Signature", FAStyle.Solid, "\uf573")] FileSignatureSolid,
        /// <summary>File Upload - file-upload - f574</summary>
        [FAIcon("file-upload", "File Upload", FAStyle.Solid, "\uf574")] FileUploadSolid,
        /// <summary>Video File - file-video - f1c8</summary>
        [FAIcon("file-video", "Video File", FAStyle.Solid, "\uf1c8")] FileVideoSolid,
        /// <summary>Video File - file-video - f1c8</summary>
        [FAIcon("file-video", "Video File", FAStyle.Regular, "\uf1c8")] FileVideoRegular,
        /// <summary>Word File - file-word - f1c2</summary>
        [FAIcon("file-word", "Word File", FAStyle.Solid, "\uf1c2")] FileWordSolid,
        /// <summary>Word File - file-word - f1c2</summary>
        [FAIcon("file-word", "Word File", FAStyle.Regular, "\uf1c2")] FileWordRegular,
        /// <summary>Fill - fill - f575</summary>
        [FAIcon("fill", "Fill", FAStyle.Solid, "\uf575")] FillSolid,
        /// <summary>Fill Drip - fill-drip - f576</summary>
        [FAIcon("fill-drip", "Fill Drip", FAStyle.Solid, "\uf576")] FillDripSolid,
        /// <summary>Film - film - f008</summary>
        [FAIcon("film", "Film", FAStyle.Solid, "\uf008")] FilmSolid,
        /// <summary>Filter - filter - f0b0</summary>
        [FAIcon("filter", "Filter", FAStyle.Solid, "\uf0b0")] FilterSolid,
        /// <summary>Fingerprint - fingerprint - f577</summary>
        [FAIcon("fingerprint", "Fingerprint", FAStyle.Solid, "\uf577")] FingerprintSolid,
        /// <summary>fire - fire - f06d</summary>
        [FAIcon("fire", "fire", FAStyle.Solid, "\uf06d")] FireSolid,
        /// <summary>fire-extinguisher - fire-extinguisher - f134</summary>
        [FAIcon("fire-extinguisher", "fire-extinguisher", FAStyle.Solid, "\uf134")] FireExtinguisherSolid,
        /// <summary>Firefox - firefox - f269</summary>
        [FAIcon("firefox", "Firefox", FAStyle.Brands, "\uf269")] FirefoxBrands,
        /// <summary>First Aid - first-aid - f479</summary>
        [FAIcon("first-aid", "First Aid", FAStyle.Solid, "\uf479")] FirstAidSolid,
        /// <summary>First Order - first-order - f2b0</summary>
        [FAIcon("first-order", "First Order", FAStyle.Brands, "\uf2b0")] FirstOrderBrands,
        /// <summary>Alternate First Order - first-order-alt - f50a</summary>
        [FAIcon("first-order-alt", "Alternate First Order", FAStyle.Brands, "\uf50a")] FirstOrderAltBrands,
        /// <summary>firstdraft - firstdraft - f3a1</summary>
        [FAIcon("firstdraft", "firstdraft", FAStyle.Brands, "\uf3a1")] FirstdraftBrands,
        /// <summary>Fish - fish - f578</summary>
        [FAIcon("fish", "Fish", FAStyle.Solid, "\uf578")] FishSolid,
        /// <summary>flag - flag - f024</summary>
        [FAIcon("flag", "flag", FAStyle.Solid, "\uf024")] FlagSolid,
        /// <summary>flag - flag - f024</summary>
        [FAIcon("flag", "flag", FAStyle.Regular, "\uf024")] FlagRegular,
        /// <summary>flag-checkered - flag-checkered - f11e</summary>
        [FAIcon("flag-checkered", "flag-checkered", FAStyle.Solid, "\uf11e")] FlagCheckeredSolid,
        /// <summary>Flask - flask - f0c3</summary>
        [FAIcon("flask", "Flask", FAStyle.Solid, "\uf0c3")] FlaskSolid,
        /// <summary>Flickr - flickr - f16e</summary>
        [FAIcon("flickr", "Flickr", FAStyle.Brands, "\uf16e")] FlickrBrands,
        /// <summary>Flipboard - flipboard - f44d</summary>
        [FAIcon("flipboard", "Flipboard", FAStyle.Brands, "\uf44d")] FlipboardBrands,
        /// <summary>Flushed Face - flushed - f579</summary>
        [FAIcon("flushed", "Flushed Face", FAStyle.Solid, "\uf579")] FlushedSolid,
        /// <summary>Flushed Face - flushed - f579</summary>
        [FAIcon("flushed", "Flushed Face", FAStyle.Regular, "\uf579")] FlushedRegular,
        /// <summary>Fly - fly - f417</summary>
        [FAIcon("fly", "Fly", FAStyle.Brands, "\uf417")] FlyBrands,
        /// <summary>Folder - folder - f07b</summary>
        [FAIcon("folder", "Folder", FAStyle.Solid, "\uf07b")] FolderSolid,
        /// <summary>Folder - folder - f07b</summary>
        [FAIcon("folder", "Folder", FAStyle.Regular, "\uf07b")] FolderRegular,
        /// <summary>Folder Open - folder-open - f07c</summary>
        [FAIcon("folder-open", "Folder Open", FAStyle.Solid, "\uf07c")] FolderOpenSolid,
        /// <summary>Folder Open - folder-open - f07c</summary>
        [FAIcon("folder-open", "Folder Open", FAStyle.Regular, "\uf07c")] FolderOpenRegular,
        /// <summary>font - font - f031</summary>
        [FAIcon("font", "font", FAStyle.Solid, "\uf031")] FontSolid,
        /// <summary>Font Awesome - font-awesome - f2b4</summary>
        [FAIcon("font-awesome", "Font Awesome", FAStyle.Brands, "\uf2b4")] FontAwesomeBrands,
        /// <summary>Alternate Font Awesome - font-awesome-alt - f35c</summary>
        [FAIcon("font-awesome-alt", "Alternate Font Awesome", FAStyle.Brands, "\uf35c")] FontAwesomeAltBrands,
        /// <summary>Font Awesome Flag - font-awesome-flag - f425</summary>
        [FAIcon("font-awesome-flag", "Font Awesome Flag", FAStyle.Brands, "\uf425")] FontAwesomeFlagBrands,
        /// <summary>Font Awesome Full Logo - font-awesome-logo-full - f4e6</summary>
        [FAIcon("font-awesome-logo-full", "Font Awesome Full Logo", FAStyle.Regular, "\uf4e6")] FontAwesomeLogoFullRegular,
        /// <summary>Font Awesome Full Logo - font-awesome-logo-full - f4e6</summary>
        [FAIcon("font-awesome-logo-full", "Font Awesome Full Logo", FAStyle.Solid, "\uf4e6")] FontAwesomeLogoFullSolid,
        /// <summary>Font Awesome Full Logo - font-awesome-logo-full - f4e6</summary>
        [FAIcon("font-awesome-logo-full", "Font Awesome Full Logo", FAStyle.Brands, "\uf4e6")] FontAwesomeLogoFullBrands,
        /// <summary>Fonticons - fonticons - f280</summary>
        [FAIcon("fonticons", "Fonticons", FAStyle.Brands, "\uf280")] FonticonsBrands,
        /// <summary>Fonticons Fi - fonticons-fi - f3a2</summary>
        [FAIcon("fonticons-fi", "Fonticons Fi", FAStyle.Brands, "\uf3a2")] FonticonsFiBrands,
        /// <summary>Football Ball - football-ball - f44e</summary>
        [FAIcon("football-ball", "Football Ball", FAStyle.Solid, "\uf44e")] FootballBallSolid,
        /// <summary>Fort Awesome - fort-awesome - f286</summary>
        [FAIcon("fort-awesome", "Fort Awesome", FAStyle.Brands, "\uf286")] FortAwesomeBrands,
        /// <summary>Alternate Fort Awesome - fort-awesome-alt - f3a3</summary>
        [FAIcon("fort-awesome-alt", "Alternate Fort Awesome", FAStyle.Brands, "\uf3a3")] FortAwesomeAltBrands,
        /// <summary>Forumbee - forumbee - f211</summary>
        [FAIcon("forumbee", "Forumbee", FAStyle.Brands, "\uf211")] ForumbeeBrands,
        /// <summary>forward - forward - f04e</summary>
        [FAIcon("forward", "forward", FAStyle.Solid, "\uf04e")] ForwardSolid,
        /// <summary>Foursquare - foursquare - f180</summary>
        [FAIcon("foursquare", "Foursquare", FAStyle.Brands, "\uf180")] FoursquareBrands,
        /// <summary>Free Code Camp - free-code-camp - f2c5</summary>
        [FAIcon("free-code-camp", "Free Code Camp", FAStyle.Brands, "\uf2c5")] FreeCodeCampBrands,
        /// <summary>FreeBSD - freebsd - f3a4</summary>
        [FAIcon("freebsd", "FreeBSD", FAStyle.Brands, "\uf3a4")] FreebsdBrands,
        /// <summary>Frog - frog - f52e</summary>
        [FAIcon("frog", "Frog", FAStyle.Solid, "\uf52e")] FrogSolid,
        /// <summary>Frowning Face - frown - f119</summary>
        [FAIcon("frown", "Frowning Face", FAStyle.Solid, "\uf119")] FrownSolid,
        /// <summary>Frowning Face - frown - f119</summary>
        [FAIcon("frown", "Frowning Face", FAStyle.Regular, "\uf119")] FrownRegular,
        /// <summary>Frowning Face With Open Mouth - frown-open - f57a</summary>
        [FAIcon("frown-open", "Frowning Face With Open Mouth", FAStyle.Solid, "\uf57a")] FrownOpenSolid,
        /// <summary>Frowning Face With Open Mouth - frown-open - f57a</summary>
        [FAIcon("frown-open", "Frowning Face With Open Mouth", FAStyle.Regular, "\uf57a")] FrownOpenRegular,
        /// <summary>Fulcrum - fulcrum - f50b</summary>
        [FAIcon("fulcrum", "Fulcrum", FAStyle.Brands, "\uf50b")] FulcrumBrands,
        /// <summary>Futbol - futbol - f1e3</summary>
        [FAIcon("futbol", "Futbol", FAStyle.Solid, "\uf1e3")] FutbolSolid,
        /// <summary>Futbol - futbol - f1e3</summary>
        [FAIcon("futbol", "Futbol", FAStyle.Regular, "\uf1e3")] FutbolRegular,
        /// <summary>Galactic Republic - galactic-republic - f50c</summary>
        [FAIcon("galactic-republic", "Galactic Republic", FAStyle.Brands, "\uf50c")] GalacticRepublicBrands,
        /// <summary>Galactic Senate - galactic-senate - f50d</summary>
        [FAIcon("galactic-senate", "Galactic Senate", FAStyle.Brands, "\uf50d")] GalacticSenateBrands,
        /// <summary>Gamepad - gamepad - f11b</summary>
        [FAIcon("gamepad", "Gamepad", FAStyle.Solid, "\uf11b")] GamepadSolid,
        /// <summary>Gas Pump - gas-pump - f52f</summary>
        [FAIcon("gas-pump", "Gas Pump", FAStyle.Solid, "\uf52f")] GasPumpSolid,
        /// <summary>Gavel - gavel - f0e3</summary>
        [FAIcon("gavel", "Gavel", FAStyle.Solid, "\uf0e3")] GavelSolid,
        /// <summary>Gem - gem - f3a5</summary>
        [FAIcon("gem", "Gem", FAStyle.Solid, "\uf3a5")] GemSolid,
        /// <summary>Gem - gem - f3a5</summary>
        [FAIcon("gem", "Gem", FAStyle.Regular, "\uf3a5")] GemRegular,
        /// <summary>Genderless - genderless - f22d</summary>
        [FAIcon("genderless", "Genderless", FAStyle.Solid, "\uf22d")] GenderlessSolid,
        /// <summary>Get Pocket - get-pocket - f265</summary>
        [FAIcon("get-pocket", "Get Pocket", FAStyle.Brands, "\uf265")] GetPocketBrands,
        /// <summary>GG Currency - gg - f260</summary>
        [FAIcon("gg", "GG Currency", FAStyle.Brands, "\uf260")] GgBrands,
        /// <summary>GG Currency Circle - gg-circle - f261</summary>
        [FAIcon("gg-circle", "GG Currency Circle", FAStyle.Brands, "\uf261")] GgCircleBrands,
        /// <summary>gift - gift - f06b</summary>
        [FAIcon("gift", "gift", FAStyle.Solid, "\uf06b")] GiftSolid,
        /// <summary>Git - git - f1d3</summary>
        [FAIcon("git", "Git", FAStyle.Brands, "\uf1d3")] GitBrands,
        /// <summary>Git Square - git-square - f1d2</summary>
        [FAIcon("git-square", "Git Square", FAStyle.Brands, "\uf1d2")] GitSquareBrands,
        /// <summary>GitHub - github - f09b</summary>
        [FAIcon("github", "GitHub", FAStyle.Brands, "\uf09b")] GithubBrands,
        /// <summary>Alternate GitHub - github-alt - f113</summary>
        [FAIcon("github-alt", "Alternate GitHub", FAStyle.Brands, "\uf113")] GithubAltBrands,
        /// <summary>GitHub Square - github-square - f092</summary>
        [FAIcon("github-square", "GitHub Square", FAStyle.Brands, "\uf092")] GithubSquareBrands,
        /// <summary>GitKraken - gitkraken - f3a6</summary>
        [FAIcon("gitkraken", "GitKraken", FAStyle.Brands, "\uf3a6")] GitkrakenBrands,
        /// <summary>GitLab - gitlab - f296</summary>
        [FAIcon("gitlab", "GitLab", FAStyle.Brands, "\uf296")] GitlabBrands,
        /// <summary>Gitter - gitter - f426</summary>
        [FAIcon("gitter", "Gitter", FAStyle.Brands, "\uf426")] GitterBrands,
        /// <summary>Martini Glass - glass-martini - f000</summary>
        [FAIcon("glass-martini", "Martini Glass", FAStyle.Solid, "\uf000")] GlassMartiniSolid,
        /// <summary>Glass Martini-alt - glass-martini-alt - f57b</summary>
        [FAIcon("glass-martini-alt", "Glass Martini-alt", FAStyle.Solid, "\uf57b")] GlassMartiniAltSolid,
        /// <summary>Glasses - glasses - f530</summary>
        [FAIcon("glasses", "Glasses", FAStyle.Solid, "\uf530")] GlassesSolid,
        /// <summary>Glide - glide - f2a5</summary>
        [FAIcon("glide", "Glide", FAStyle.Brands, "\uf2a5")] GlideBrands,
        /// <summary>Glide G - glide-g - f2a6</summary>
        [FAIcon("glide-g", "Glide G", FAStyle.Brands, "\uf2a6")] GlideGBrands,
        /// <summary>Globe - globe - f0ac</summary>
        [FAIcon("globe", "Globe", FAStyle.Solid, "\uf0ac")] GlobeSolid,
        /// <summary>Globe with Africa shown - globe-africa - f57c</summary>
        [FAIcon("globe-africa", "Globe with Africa shown", FAStyle.Solid, "\uf57c")] GlobeAfricaSolid,
        /// <summary>Globe with Americas shown - globe-americas - f57d</summary>
        [FAIcon("globe-americas", "Globe with Americas shown", FAStyle.Solid, "\uf57d")] GlobeAmericasSolid,
        /// <summary>Globe with Asia shown - globe-asia - f57e</summary>
        [FAIcon("globe-asia", "Globe with Asia shown", FAStyle.Solid, "\uf57e")] GlobeAsiaSolid,
        /// <summary>Gofore - gofore - f3a7</summary>
        [FAIcon("gofore", "Gofore", FAStyle.Brands, "\uf3a7")] GoforeBrands,
        /// <summary>Golf Ball - golf-ball - f450</summary>
        [FAIcon("golf-ball", "Golf Ball", FAStyle.Solid, "\uf450")] GolfBallSolid,
        /// <summary>Goodreads - goodreads - f3a8</summary>
        [FAIcon("goodreads", "Goodreads", FAStyle.Brands, "\uf3a8")] GoodreadsBrands,
        /// <summary>Goodreads G - goodreads-g - f3a9</summary>
        [FAIcon("goodreads-g", "Goodreads G", FAStyle.Brands, "\uf3a9")] GoodreadsGBrands,
        /// <summary>Google Logo - google - f1a0</summary>
        [FAIcon("google", "Google Logo", FAStyle.Brands, "\uf1a0")] GoogleBrands,
        /// <summary>Google Drive - google-drive - f3aa</summary>
        [FAIcon("google-drive", "Google Drive", FAStyle.Brands, "\uf3aa")] GoogleDriveBrands,
        /// <summary>Google Play - google-play - f3ab</summary>
        [FAIcon("google-play", "Google Play", FAStyle.Brands, "\uf3ab")] GooglePlayBrands,
        /// <summary>Google Plus - google-plus - f2b3</summary>
        [FAIcon("google-plus", "Google Plus", FAStyle.Brands, "\uf2b3")] GooglePlusBrands,
        /// <summary>Google Plus G - google-plus-g - f0d5</summary>
        [FAIcon("google-plus-g", "Google Plus G", FAStyle.Brands, "\uf0d5")] GooglePlusGBrands,
        /// <summary>Google Plus Square - google-plus-square - f0d4</summary>
        [FAIcon("google-plus-square", "Google Plus Square", FAStyle.Brands, "\uf0d4")] GooglePlusSquareBrands,
        /// <summary>Google Wallet - google-wallet - f1ee</summary>
        [FAIcon("google-wallet", "Google Wallet", FAStyle.Brands, "\uf1ee")] GoogleWalletBrands,
        /// <summary>Graduation Cap - graduation-cap - f19d</summary>
        [FAIcon("graduation-cap", "Graduation Cap", FAStyle.Solid, "\uf19d")] GraduationCapSolid,
        /// <summary>Gratipay (Gittip) - gratipay - f184</summary>
        [FAIcon("gratipay", "Gratipay (Gittip)", FAStyle.Brands, "\uf184")] GratipayBrands,
        /// <summary>Grav - grav - f2d6</summary>
        [FAIcon("grav", "Grav", FAStyle.Brands, "\uf2d6")] GravBrands,
        /// <summary>Greater Than - greater-than - f531</summary>
        [FAIcon("greater-than", "Greater Than", FAStyle.Solid, "\uf531")] GreaterThanSolid,
        /// <summary>Greater Than Equal To - greater-than-equal - f532</summary>
        [FAIcon("greater-than-equal", "Greater Than Equal To", FAStyle.Solid, "\uf532")] GreaterThanEqualSolid,
        /// <summary>Grimacing Face - grimace - f57f</summary>
        [FAIcon("grimace", "Grimacing Face", FAStyle.Solid, "\uf57f")] GrimaceSolid,
        /// <summary>Grimacing Face - grimace - f57f</summary>
        [FAIcon("grimace", "Grimacing Face", FAStyle.Regular, "\uf57f")] GrimaceRegular,
        /// <summary>Grinning Face - grin - f580</summary>
        [FAIcon("grin", "Grinning Face", FAStyle.Solid, "\uf580")] GrinSolid,
        /// <summary>Grinning Face - grin - f580</summary>
        [FAIcon("grin", "Grinning Face", FAStyle.Regular, "\uf580")] GrinRegular,
        /// <summary>Alternate Grinning Face - grin-alt - f581</summary>
        [FAIcon("grin-alt", "Alternate Grinning Face", FAStyle.Solid, "\uf581")] GrinAltSolid,
        /// <summary>Alternate Grinning Face - grin-alt - f581</summary>
        [FAIcon("grin-alt", "Alternate Grinning Face", FAStyle.Regular, "\uf581")] GrinAltRegular,
        /// <summary>Grinning Face With Smiling Eyes - grin-beam - f582</summary>
        [FAIcon("grin-beam", "Grinning Face With Smiling Eyes", FAStyle.Solid, "\uf582")] GrinBeamSolid,
        /// <summary>Grinning Face With Smiling Eyes - grin-beam - f582</summary>
        [FAIcon("grin-beam", "Grinning Face With Smiling Eyes", FAStyle.Regular, "\uf582")] GrinBeamRegular,
        /// <summary>Grinning Face With Sweat - grin-beam-sweat - f583</summary>
        [FAIcon("grin-beam-sweat", "Grinning Face With Sweat", FAStyle.Solid, "\uf583")] GrinBeamSweatSolid,
        /// <summary>Grinning Face With Sweat - grin-beam-sweat - f583</summary>
        [FAIcon("grin-beam-sweat", "Grinning Face With Sweat", FAStyle.Regular, "\uf583")] GrinBeamSweatRegular,
        /// <summary>Smiling Face With Heart-Eyes - grin-hearts - f584</summary>
        [FAIcon("grin-hearts", "Smiling Face With Heart-Eyes", FAStyle.Solid, "\uf584")] GrinHeartsSolid,
        /// <summary>Smiling Face With Heart-Eyes - grin-hearts - f584</summary>
        [FAIcon("grin-hearts", "Smiling Face With Heart-Eyes", FAStyle.Regular, "\uf584")] GrinHeartsRegular,
        /// <summary>Grinning Squinting Face - grin-squint - f585</summary>
        [FAIcon("grin-squint", "Grinning Squinting Face", FAStyle.Solid, "\uf585")] GrinSquintSolid,
        /// <summary>Grinning Squinting Face - grin-squint - f585</summary>
        [FAIcon("grin-squint", "Grinning Squinting Face", FAStyle.Regular, "\uf585")] GrinSquintRegular,
        /// <summary>Rolling on the Floor Laughing - grin-squint-tears - f586</summary>
        [FAIcon("grin-squint-tears", "Rolling on the Floor Laughing", FAStyle.Solid, "\uf586")] GrinSquintTearsSolid,
        /// <summary>Rolling on the Floor Laughing - grin-squint-tears - f586</summary>
        [FAIcon("grin-squint-tears", "Rolling on the Floor Laughing", FAStyle.Regular, "\uf586")] GrinSquintTearsRegular,
        /// <summary>Star-Struck - grin-stars - f587</summary>
        [FAIcon("grin-stars", "Star-Struck", FAStyle.Solid, "\uf587")] GrinStarsSolid,
        /// <summary>Star-Struck - grin-stars - f587</summary>
        [FAIcon("grin-stars", "Star-Struck", FAStyle.Regular, "\uf587")] GrinStarsRegular,
        /// <summary>Face With Tears of Joy - grin-tears - f588</summary>
        [FAIcon("grin-tears", "Face With Tears of Joy", FAStyle.Solid, "\uf588")] GrinTearsSolid,
        /// <summary>Face With Tears of Joy - grin-tears - f588</summary>
        [FAIcon("grin-tears", "Face With Tears of Joy", FAStyle.Regular, "\uf588")] GrinTearsRegular,
        /// <summary>Face With Tongue - grin-tongue - f589</summary>
        [FAIcon("grin-tongue", "Face With Tongue", FAStyle.Solid, "\uf589")] GrinTongueSolid,
        /// <summary>Face With Tongue - grin-tongue - f589</summary>
        [FAIcon("grin-tongue", "Face With Tongue", FAStyle.Regular, "\uf589")] GrinTongueRegular,
        /// <summary>Squinting Face With Tongue - grin-tongue-squint - f58a</summary>
        [FAIcon("grin-tongue-squint", "Squinting Face With Tongue", FAStyle.Solid, "\uf58a")] GrinTongueSquintSolid,
        /// <summary>Squinting Face With Tongue - grin-tongue-squint - f58a</summary>
        [FAIcon("grin-tongue-squint", "Squinting Face With Tongue", FAStyle.Regular, "\uf58a")] GrinTongueSquintRegular,
        /// <summary>Winking Face With Tongue - grin-tongue-wink - f58b</summary>
        [FAIcon("grin-tongue-wink", "Winking Face With Tongue", FAStyle.Solid, "\uf58b")] GrinTongueWinkSolid,
        /// <summary>Winking Face With Tongue - grin-tongue-wink - f58b</summary>
        [FAIcon("grin-tongue-wink", "Winking Face With Tongue", FAStyle.Regular, "\uf58b")] GrinTongueWinkRegular,
        /// <summary>Grinning Winking Face - grin-wink - f58c</summary>
        [FAIcon("grin-wink", "Grinning Winking Face", FAStyle.Solid, "\uf58c")] GrinWinkSolid,
        /// <summary>Grinning Winking Face - grin-wink - f58c</summary>
        [FAIcon("grin-wink", "Grinning Winking Face", FAStyle.Regular, "\uf58c")] GrinWinkRegular,
        /// <summary>Grip Horizontal - grip-horizontal - f58d</summary>
        [FAIcon("grip-horizontal", "Grip Horizontal", FAStyle.Solid, "\uf58d")] GripHorizontalSolid,
        /// <summary>Grip Vertical - grip-vertical - f58e</summary>
        [FAIcon("grip-vertical", "Grip Vertical", FAStyle.Solid, "\uf58e")] GripVerticalSolid,
        /// <summary>Gripfire, Inc. - gripfire - f3ac</summary>
        [FAIcon("gripfire", "Gripfire, Inc.", FAStyle.Brands, "\uf3ac")] GripfireBrands,
        /// <summary>Grunt - grunt - f3ad</summary>
        [FAIcon("grunt", "Grunt", FAStyle.Brands, "\uf3ad")] GruntBrands,
        /// <summary>Gulp - gulp - f3ae</summary>
        [FAIcon("gulp", "Gulp", FAStyle.Brands, "\uf3ae")] GulpBrands,
        /// <summary>H Square - h-square - f0fd</summary>
        [FAIcon("h-square", "H Square", FAStyle.Solid, "\uf0fd")] HSquareSolid,
        /// <summary>Hacker News - hacker-news - f1d4</summary>
        [FAIcon("hacker-news", "Hacker News", FAStyle.Brands, "\uf1d4")] HackerNewsBrands,
        /// <summary>Hacker News Square - hacker-news-square - f3af</summary>
        [FAIcon("hacker-news-square", "Hacker News Square", FAStyle.Brands, "\uf3af")] HackerNewsSquareBrands,
        /// <summary>Hackerrank - hackerrank - f5f7</summary>
        [FAIcon("hackerrank", "Hackerrank", FAStyle.Brands, "\uf5f7")] HackerrankBrands,
        /// <summary>Hand Holding - hand-holding - f4bd</summary>
        [FAIcon("hand-holding", "Hand Holding", FAStyle.Solid, "\uf4bd")] HandHoldingSolid,
        /// <summary>Hand Holding Heart - hand-holding-heart - f4be</summary>
        [FAIcon("hand-holding-heart", "Hand Holding Heart", FAStyle.Solid, "\uf4be")] HandHoldingHeartSolid,
        /// <summary>Hand Holding US Dollar - hand-holding-usd - f4c0</summary>
        [FAIcon("hand-holding-usd", "Hand Holding US Dollar", FAStyle.Solid, "\uf4c0")] HandHoldingUsdSolid,
        /// <summary>Lizard (Hand) - hand-lizard - f258</summary>
        [FAIcon("hand-lizard", "Lizard (Hand)", FAStyle.Solid, "\uf258")] HandLizardSolid,
        /// <summary>Lizard (Hand) - hand-lizard - f258</summary>
        [FAIcon("hand-lizard", "Lizard (Hand)", FAStyle.Regular, "\uf258")] HandLizardRegular,
        /// <summary>Paper (Hand) - hand-paper - f256</summary>
        [FAIcon("hand-paper", "Paper (Hand)", FAStyle.Solid, "\uf256")] HandPaperSolid,
        /// <summary>Paper (Hand) - hand-paper - f256</summary>
        [FAIcon("hand-paper", "Paper (Hand)", FAStyle.Regular, "\uf256")] HandPaperRegular,
        /// <summary>Peace (Hand) - hand-peace - f25b</summary>
        [FAIcon("hand-peace", "Peace (Hand)", FAStyle.Solid, "\uf25b")] HandPeaceSolid,
        /// <summary>Peace (Hand) - hand-peace - f25b</summary>
        [FAIcon("hand-peace", "Peace (Hand)", FAStyle.Regular, "\uf25b")] HandPeaceRegular,
        /// <summary>Hand Pointing Down - hand-point-down - f0a7</summary>
        [FAIcon("hand-point-down", "Hand Pointing Down", FAStyle.Solid, "\uf0a7")] HandPointDownSolid,
        /// <summary>Hand Pointing Down - hand-point-down - f0a7</summary>
        [FAIcon("hand-point-down", "Hand Pointing Down", FAStyle.Regular, "\uf0a7")] HandPointDownRegular,
        /// <summary>Hand Pointing Left - hand-point-left - f0a5</summary>
        [FAIcon("hand-point-left", "Hand Pointing Left", FAStyle.Solid, "\uf0a5")] HandPointLeftSolid,
        /// <summary>Hand Pointing Left - hand-point-left - f0a5</summary>
        [FAIcon("hand-point-left", "Hand Pointing Left", FAStyle.Regular, "\uf0a5")] HandPointLeftRegular,
        /// <summary>Hand Pointing Right - hand-point-right - f0a4</summary>
        [FAIcon("hand-point-right", "Hand Pointing Right", FAStyle.Solid, "\uf0a4")] HandPointRightSolid,
        /// <summary>Hand Pointing Right - hand-point-right - f0a4</summary>
        [FAIcon("hand-point-right", "Hand Pointing Right", FAStyle.Regular, "\uf0a4")] HandPointRightRegular,
        /// <summary>Hand Pointing Up - hand-point-up - f0a6</summary>
        [FAIcon("hand-point-up", "Hand Pointing Up", FAStyle.Solid, "\uf0a6")] HandPointUpSolid,
        /// <summary>Hand Pointing Up - hand-point-up - f0a6</summary>
        [FAIcon("hand-point-up", "Hand Pointing Up", FAStyle.Regular, "\uf0a6")] HandPointUpRegular,
        /// <summary>Pointer (Hand) - hand-pointer - f25a</summary>
        [FAIcon("hand-pointer", "Pointer (Hand)", FAStyle.Solid, "\uf25a")] HandPointerSolid,
        /// <summary>Pointer (Hand) - hand-pointer - f25a</summary>
        [FAIcon("hand-pointer", "Pointer (Hand)", FAStyle.Regular, "\uf25a")] HandPointerRegular,
        /// <summary>Rock (Hand) - hand-rock - f255</summary>
        [FAIcon("hand-rock", "Rock (Hand)", FAStyle.Solid, "\uf255")] HandRockSolid,
        /// <summary>Rock (Hand) - hand-rock - f255</summary>
        [FAIcon("hand-rock", "Rock (Hand)", FAStyle.Regular, "\uf255")] HandRockRegular,
        /// <summary>Scissors (Hand) - hand-scissors - f257</summary>
        [FAIcon("hand-scissors", "Scissors (Hand)", FAStyle.Solid, "\uf257")] HandScissorsSolid,
        /// <summary>Scissors (Hand) - hand-scissors - f257</summary>
        [FAIcon("hand-scissors", "Scissors (Hand)", FAStyle.Regular, "\uf257")] HandScissorsRegular,
        /// <summary>Spock (Hand) - hand-spock - f259</summary>
        [FAIcon("hand-spock", "Spock (Hand)", FAStyle.Solid, "\uf259")] HandSpockSolid,
        /// <summary>Spock (Hand) - hand-spock - f259</summary>
        [FAIcon("hand-spock", "Spock (Hand)", FAStyle.Regular, "\uf259")] HandSpockRegular,
        /// <summary>Hands - hands - f4c2</summary>
        [FAIcon("hands", "Hands", FAStyle.Solid, "\uf4c2")] HandsSolid,
        /// <summary>Helping Hands - hands-helping - f4c4</summary>
        [FAIcon("hands-helping", "Helping Hands", FAStyle.Solid, "\uf4c4")] HandsHelpingSolid,
        /// <summary>Handshake - handshake - f2b5</summary>
        [FAIcon("handshake", "Handshake", FAStyle.Solid, "\uf2b5")] HandshakeSolid,
        /// <summary>Handshake - handshake - f2b5</summary>
        [FAIcon("handshake", "Handshake", FAStyle.Regular, "\uf2b5")] HandshakeRegular,
        /// <summary>Hashtag - hashtag - f292</summary>
        [FAIcon("hashtag", "Hashtag", FAStyle.Solid, "\uf292")] HashtagSolid,
        /// <summary>HDD - hdd - f0a0</summary>
        [FAIcon("hdd", "HDD", FAStyle.Solid, "\uf0a0")] HddSolid,
        /// <summary>HDD - hdd - f0a0</summary>
        [FAIcon("hdd", "HDD", FAStyle.Regular, "\uf0a0")] HddRegular,
        /// <summary>heading - heading - f1dc</summary>
        [FAIcon("heading", "heading", FAStyle.Solid, "\uf1dc")] HeadingSolid,
        /// <summary>headphones - headphones - f025</summary>
        [FAIcon("headphones", "headphones", FAStyle.Solid, "\uf025")] HeadphonesSolid,
        /// <summary>Headphones Alt - headphones-alt - f58f</summary>
        [FAIcon("headphones-alt", "Headphones Alt", FAStyle.Solid, "\uf58f")] HeadphonesAltSolid,
        /// <summary>Headset - headset - f590</summary>
        [FAIcon("headset", "Headset", FAStyle.Solid, "\uf590")] HeadsetSolid,
        /// <summary>Heart - heart - f004</summary>
        [FAIcon("heart", "Heart", FAStyle.Solid, "\uf004")] HeartSolid,
        /// <summary>Heart - heart - f004</summary>
        [FAIcon("heart", "Heart", FAStyle.Regular, "\uf004")] HeartRegular,
        /// <summary>Heartbeat - heartbeat - f21e</summary>
        [FAIcon("heartbeat", "Heartbeat", FAStyle.Solid, "\uf21e")] HeartbeatSolid,
        /// <summary>Helicopter - helicopter - f533</summary>
        [FAIcon("helicopter", "Helicopter", FAStyle.Solid, "\uf533")] HelicopterSolid,
        /// <summary>Highlighter - highlighter - f591</summary>
        [FAIcon("highlighter", "Highlighter", FAStyle.Solid, "\uf591")] HighlighterSolid,
        /// <summary>Hips - hips - f452</summary>
        [FAIcon("hips", "Hips", FAStyle.Brands, "\uf452")] HipsBrands,
        /// <summary>HireAHelper - hire-a-helper - f3b0</summary>
        [FAIcon("hire-a-helper", "HireAHelper", FAStyle.Brands, "\uf3b0")] HireAHelperBrands,
        /// <summary>History - history - f1da</summary>
        [FAIcon("history", "History", FAStyle.Solid, "\uf1da")] HistorySolid,
        /// <summary>Hockey Puck - hockey-puck - f453</summary>
        [FAIcon("hockey-puck", "Hockey Puck", FAStyle.Solid, "\uf453")] HockeyPuckSolid,
        /// <summary>home - home - f015</summary>
        [FAIcon("home", "home", FAStyle.Solid, "\uf015")] HomeSolid,
        /// <summary>Hooli - hooli - f427</summary>
        [FAIcon("hooli", "Hooli", FAStyle.Brands, "\uf427")] HooliBrands,
        /// <summary>Hornbill - hornbill - f592</summary>
        [FAIcon("hornbill", "Hornbill", FAStyle.Brands, "\uf592")] HornbillBrands,
        /// <summary>hospital - hospital - f0f8</summary>
        [FAIcon("hospital", "hospital", FAStyle.Solid, "\uf0f8")] HospitalSolid,
        /// <summary>hospital - hospital - f0f8</summary>
        [FAIcon("hospital", "hospital", FAStyle.Regular, "\uf0f8")] HospitalRegular,
        /// <summary>Alternate Hospital - hospital-alt - f47d</summary>
        [FAIcon("hospital-alt", "Alternate Hospital", FAStyle.Solid, "\uf47d")] HospitalAltSolid,
        /// <summary>Hospital Symbol - hospital-symbol - f47e</summary>
        [FAIcon("hospital-symbol", "Hospital Symbol", FAStyle.Solid, "\uf47e")] HospitalSymbolSolid,
        /// <summary>Hot Tub - hot-tub - f593</summary>
        [FAIcon("hot-tub", "Hot Tub", FAStyle.Solid, "\uf593")] HotTubSolid,
        /// <summary>Hotel - hotel - f594</summary>
        [FAIcon("hotel", "Hotel", FAStyle.Solid, "\uf594")] HotelSolid,
        /// <summary>Hotjar - hotjar - f3b1</summary>
        [FAIcon("hotjar", "Hotjar", FAStyle.Brands, "\uf3b1")] HotjarBrands,
        /// <summary>Hourglass - hourglass - f254</summary>
        [FAIcon("hourglass", "Hourglass", FAStyle.Solid, "\uf254")] HourglassSolid,
        /// <summary>Hourglass - hourglass - f254</summary>
        [FAIcon("hourglass", "Hourglass", FAStyle.Regular, "\uf254")] HourglassRegular,
        /// <summary>Hourglass End - hourglass-end - f253</summary>
        [FAIcon("hourglass-end", "Hourglass End", FAStyle.Solid, "\uf253")] HourglassEndSolid,
        /// <summary>Hourglass Half - hourglass-half - f252</summary>
        [FAIcon("hourglass-half", "Hourglass Half", FAStyle.Solid, "\uf252")] HourglassHalfSolid,
        /// <summary>Hourglass Start - hourglass-start - f251</summary>
        [FAIcon("hourglass-start", "Hourglass Start", FAStyle.Solid, "\uf251")] HourglassStartSolid,
        /// <summary>Houzz - houzz - f27c</summary>
        [FAIcon("houzz", "Houzz", FAStyle.Brands, "\uf27c")] HouzzBrands,
        /// <summary>HTML 5 Logo - html5 - f13b</summary>
        [FAIcon("html5", "HTML 5 Logo", FAStyle.Brands, "\uf13b")] Html5Brands,
        /// <summary>HubSpot - hubspot - f3b2</summary>
        [FAIcon("hubspot", "HubSpot", FAStyle.Brands, "\uf3b2")] HubspotBrands,
        /// <summary>I Beam Cursor - i-cursor - f246</summary>
        [FAIcon("i-cursor", "I Beam Cursor", FAStyle.Solid, "\uf246")] ICursorSolid,
        /// <summary>Identification Badge - id-badge - f2c1</summary>
        [FAIcon("id-badge", "Identification Badge", FAStyle.Solid, "\uf2c1")] IdBadgeSolid,
        /// <summary>Identification Badge - id-badge - f2c1</summary>
        [FAIcon("id-badge", "Identification Badge", FAStyle.Regular, "\uf2c1")] IdBadgeRegular,
        /// <summary>Identification Card - id-card - f2c2</summary>
        [FAIcon("id-card", "Identification Card", FAStyle.Solid, "\uf2c2")] IdCardSolid,
        /// <summary>Identification Card - id-card - f2c2</summary>
        [FAIcon("id-card", "Identification Card", FAStyle.Regular, "\uf2c2")] IdCardRegular,
        /// <summary>Alternate Identification Card - id-card-alt - f47f</summary>
        [FAIcon("id-card-alt", "Alternate Identification Card", FAStyle.Solid, "\uf47f")] IdCardAltSolid,
        /// <summary>Image - image - f03e</summary>
        [FAIcon("image", "Image", FAStyle.Solid, "\uf03e")] ImageSolid,
        /// <summary>Image - image - f03e</summary>
        [FAIcon("image", "Image", FAStyle.Regular, "\uf03e")] ImageRegular,
        /// <summary>Images - images - f302</summary>
        [FAIcon("images", "Images", FAStyle.Solid, "\uf302")] ImagesSolid,
        /// <summary>Images - images - f302</summary>
        [FAIcon("images", "Images", FAStyle.Regular, "\uf302")] ImagesRegular,
        /// <summary>IMDB - imdb - f2d8</summary>
        [FAIcon("imdb", "IMDB", FAStyle.Brands, "\uf2d8")] ImdbBrands,
        /// <summary>inbox - inbox - f01c</summary>
        [FAIcon("inbox", "inbox", FAStyle.Solid, "\uf01c")] InboxSolid,
        /// <summary>Indent - indent - f03c</summary>
        [FAIcon("indent", "Indent", FAStyle.Solid, "\uf03c")] IndentSolid,
        /// <summary>Industry - industry - f275</summary>
        [FAIcon("industry", "Industry", FAStyle.Solid, "\uf275")] IndustrySolid,
        /// <summary>Infinity - infinity - f534</summary>
        [FAIcon("infinity", "Infinity", FAStyle.Solid, "\uf534")] InfinitySolid,
        /// <summary>Info - info - f129</summary>
        [FAIcon("info", "Info", FAStyle.Solid, "\uf129")] InfoSolid,
        /// <summary>Info Circle - info-circle - f05a</summary>
        [FAIcon("info-circle", "Info Circle", FAStyle.Solid, "\uf05a")] InfoCircleSolid,
        /// <summary>Instagram - instagram - f16d</summary>
        [FAIcon("instagram", "Instagram", FAStyle.Brands, "\uf16d")] InstagramBrands,
        /// <summary>Internet-explorer - internet-explorer - f26b</summary>
        [FAIcon("internet-explorer", "Internet-explorer", FAStyle.Brands, "\uf26b")] InternetExplorerBrands,
        /// <summary>ioxhost - ioxhost - f208</summary>
        [FAIcon("ioxhost", "ioxhost", FAStyle.Brands, "\uf208")] IoxhostBrands,
        /// <summary>italic - italic - f033</summary>
        [FAIcon("italic", "italic", FAStyle.Solid, "\uf033")] ItalicSolid,
        /// <summary>iTunes - itunes - f3b4</summary>
        [FAIcon("itunes", "iTunes", FAStyle.Brands, "\uf3b4")] ItunesBrands,
        /// <summary>Itunes Note - itunes-note - f3b5</summary>
        [FAIcon("itunes-note", "Itunes Note", FAStyle.Brands, "\uf3b5")] ItunesNoteBrands,
        /// <summary>Java - java - f4e4</summary>
        [FAIcon("java", "Java", FAStyle.Brands, "\uf4e4")] JavaBrands,
        /// <summary>Jedi Order - jedi-order - f50e</summary>
        [FAIcon("jedi-order", "Jedi Order", FAStyle.Brands, "\uf50e")] JediOrderBrands,
        /// <summary>Jenkis - jenkins - f3b6</summary>
        [FAIcon("jenkins", "Jenkis", FAStyle.Brands, "\uf3b6")] JenkinsBrands,
        /// <summary>Joget - joget - f3b7</summary>
        [FAIcon("joget", "Joget", FAStyle.Brands, "\uf3b7")] JogetBrands,
        /// <summary>Joint - joint - f595</summary>
        [FAIcon("joint", "Joint", FAStyle.Solid, "\uf595")] JointSolid,
        /// <summary>Joomla Logo - joomla - f1aa</summary>
        [FAIcon("joomla", "Joomla Logo", FAStyle.Brands, "\uf1aa")] JoomlaBrands,
        /// <summary>JavaScript (JS) - js - f3b8</summary>
        [FAIcon("js", "JavaScript (JS)", FAStyle.Brands, "\uf3b8")] JsBrands,
        /// <summary>JavaScript (JS) Square - js-square - f3b9</summary>
        [FAIcon("js-square", "JavaScript (JS) Square", FAStyle.Brands, "\uf3b9")] JsSquareBrands,
        /// <summary>jsFiddle - jsfiddle - f1cc</summary>
        [FAIcon("jsfiddle", "jsFiddle", FAStyle.Brands, "\uf1cc")] JsfiddleBrands,
        /// <summary>Kaggle - kaggle - f5fa</summary>
        [FAIcon("kaggle", "Kaggle", FAStyle.Brands, "\uf5fa")] KaggleBrands,
        /// <summary>key - key - f084</summary>
        [FAIcon("key", "key", FAStyle.Solid, "\uf084")] KeySolid,
        /// <summary>Keybase - keybase - f4f5</summary>
        [FAIcon("keybase", "Keybase", FAStyle.Brands, "\uf4f5")] KeybaseBrands,
        /// <summary>Keyboard - keyboard - f11c</summary>
        [FAIcon("keyboard", "Keyboard", FAStyle.Solid, "\uf11c")] KeyboardSolid,
        /// <summary>Keyboard - keyboard - f11c</summary>
        [FAIcon("keyboard", "Keyboard", FAStyle.Regular, "\uf11c")] KeyboardRegular,
        /// <summary>KeyCDN - keycdn - f3ba</summary>
        [FAIcon("keycdn", "KeyCDN", FAStyle.Brands, "\uf3ba")] KeycdnBrands,
        /// <summary>Kickstarter - kickstarter - f3bb</summary>
        [FAIcon("kickstarter", "Kickstarter", FAStyle.Brands, "\uf3bb")] KickstarterBrands,
        /// <summary>Kickstarter K - kickstarter-k - f3bc</summary>
        [FAIcon("kickstarter-k", "Kickstarter K", FAStyle.Brands, "\uf3bc")] KickstarterKBrands,
        /// <summary>Kissing Face - kiss - f596</summary>
        [FAIcon("kiss", "Kissing Face", FAStyle.Solid, "\uf596")] KissSolid,
        /// <summary>Kissing Face - kiss - f596</summary>
        [FAIcon("kiss", "Kissing Face", FAStyle.Regular, "\uf596")] KissRegular,
        /// <summary>Kissing Face With Smiling Eyes - kiss-beam - f597</summary>
        [FAIcon("kiss-beam", "Kissing Face With Smiling Eyes", FAStyle.Solid, "\uf597")] KissBeamSolid,
        /// <summary>Kissing Face With Smiling Eyes - kiss-beam - f597</summary>
        [FAIcon("kiss-beam", "Kissing Face With Smiling Eyes", FAStyle.Regular, "\uf597")] KissBeamRegular,
        /// <summary>Face Blowing a Kiss - kiss-wink-heart - f598</summary>
        [FAIcon("kiss-wink-heart", "Face Blowing a Kiss", FAStyle.Solid, "\uf598")] KissWinkHeartSolid,
        /// <summary>Face Blowing a Kiss - kiss-wink-heart - f598</summary>
        [FAIcon("kiss-wink-heart", "Face Blowing a Kiss", FAStyle.Regular, "\uf598")] KissWinkHeartRegular,
        /// <summary>Kiwi Bird - kiwi-bird - f535</summary>
        [FAIcon("kiwi-bird", "Kiwi Bird", FAStyle.Solid, "\uf535")] KiwiBirdSolid,
        /// <summary>KORVUE - korvue - f42f</summary>
        [FAIcon("korvue", "KORVUE", FAStyle.Brands, "\uf42f")] KorvueBrands,
        /// <summary>Language - language - f1ab</summary>
        [FAIcon("language", "Language", FAStyle.Solid, "\uf1ab")] LanguageSolid,
        /// <summary>Laptop - laptop - f109</summary>
        [FAIcon("laptop", "Laptop", FAStyle.Solid, "\uf109")] LaptopSolid,
        /// <summary>Laptop Code - laptop-code - f5fc</summary>
        [FAIcon("laptop-code", "Laptop Code", FAStyle.Solid, "\uf5fc")] LaptopCodeSolid,
        /// <summary>Laravel - laravel - f3bd</summary>
        [FAIcon("laravel", "Laravel", FAStyle.Brands, "\uf3bd")] LaravelBrands,
        /// <summary>last.fm - lastfm - f202</summary>
        [FAIcon("lastfm", "last.fm", FAStyle.Brands, "\uf202")] LastfmBrands,
        /// <summary>last.fm Square - lastfm-square - f203</summary>
        [FAIcon("lastfm-square", "last.fm Square", FAStyle.Brands, "\uf203")] LastfmSquareBrands,
        /// <summary>Grinning Face With Big Eyes - laugh - f599</summary>
        [FAIcon("laugh", "Grinning Face With Big Eyes", FAStyle.Solid, "\uf599")] LaughSolid,
        /// <summary>Grinning Face With Big Eyes - laugh - f599</summary>
        [FAIcon("laugh", "Grinning Face With Big Eyes", FAStyle.Regular, "\uf599")] LaughRegular,
        /// <summary>Laugh Face with Beaming Eyes - laugh-beam - f59a</summary>
        [FAIcon("laugh-beam", "Laugh Face with Beaming Eyes", FAStyle.Solid, "\uf59a")] LaughBeamSolid,
        /// <summary>Laugh Face with Beaming Eyes - laugh-beam - f59a</summary>
        [FAIcon("laugh-beam", "Laugh Face with Beaming Eyes", FAStyle.Regular, "\uf59a")] LaughBeamRegular,
        /// <summary>Laughing Squinting Face - laugh-squint - f59b</summary>
        [FAIcon("laugh-squint", "Laughing Squinting Face", FAStyle.Solid, "\uf59b")] LaughSquintSolid,
        /// <summary>Laughing Squinting Face - laugh-squint - f59b</summary>
        [FAIcon("laugh-squint", "Laughing Squinting Face", FAStyle.Regular, "\uf59b")] LaughSquintRegular,
        /// <summary>Laughing Winking Face - laugh-wink - f59c</summary>
        [FAIcon("laugh-wink", "Laughing Winking Face", FAStyle.Solid, "\uf59c")] LaughWinkSolid,
        /// <summary>Laughing Winking Face - laugh-wink - f59c</summary>
        [FAIcon("laugh-wink", "Laughing Winking Face", FAStyle.Regular, "\uf59c")] LaughWinkRegular,
        /// <summary>Layer Group - layer-group - f5fd</summary>
        [FAIcon("layer-group", "Layer Group", FAStyle.Solid, "\uf5fd")] LayerGroupSolid,
        /// <summary>leaf - leaf - f06c</summary>
        [FAIcon("leaf", "leaf", FAStyle.Solid, "\uf06c")] LeafSolid,
        /// <summary>Leanpub - leanpub - f212</summary>
        [FAIcon("leanpub", "Leanpub", FAStyle.Brands, "\uf212")] LeanpubBrands,
        /// <summary>Lemon - lemon - f094</summary>
        [FAIcon("lemon", "Lemon", FAStyle.Solid, "\uf094")] LemonSolid,
        /// <summary>Lemon - lemon - f094</summary>
        [FAIcon("lemon", "Lemon", FAStyle.Regular, "\uf094")] LemonRegular,
        /// <summary>Less - less - f41d</summary>
        [FAIcon("less", "Less", FAStyle.Brands, "\uf41d")] LessBrands,
        /// <summary>Less Than - less-than - f536</summary>
        [FAIcon("less-than", "Less Than", FAStyle.Solid, "\uf536")] LessThanSolid,
        /// <summary>Less Than Equal To - less-than-equal - f537</summary>
        [FAIcon("less-than-equal", "Less Than Equal To", FAStyle.Solid, "\uf537")] LessThanEqualSolid,
        /// <summary>Alternate Level Down - level-down-alt - f3be</summary>
        [FAIcon("level-down-alt", "Alternate Level Down", FAStyle.Solid, "\uf3be")] LevelDownAltSolid,
        /// <summary>Alternate Level Up - level-up-alt - f3bf</summary>
        [FAIcon("level-up-alt", "Alternate Level Up", FAStyle.Solid, "\uf3bf")] LevelUpAltSolid,
        /// <summary>Life Ring - life-ring - f1cd</summary>
        [FAIcon("life-ring", "Life Ring", FAStyle.Solid, "\uf1cd")] LifeRingSolid,
        /// <summary>Life Ring - life-ring - f1cd</summary>
        [FAIcon("life-ring", "Life Ring", FAStyle.Regular, "\uf1cd")] LifeRingRegular,
        /// <summary>Lightbulb - lightbulb - f0eb</summary>
        [FAIcon("lightbulb", "Lightbulb", FAStyle.Solid, "\uf0eb")] LightbulbSolid,
        /// <summary>Lightbulb - lightbulb - f0eb</summary>
        [FAIcon("lightbulb", "Lightbulb", FAStyle.Regular, "\uf0eb")] LightbulbRegular,
        /// <summary>Line - line - f3c0</summary>
        [FAIcon("line", "Line", FAStyle.Brands, "\uf3c0")] LineBrands,
        /// <summary>Link - link - f0c1</summary>
        [FAIcon("link", "Link", FAStyle.Solid, "\uf0c1")] LinkSolid,
        /// <summary>LinkedIn - linkedin - f08c</summary>
        [FAIcon("linkedin", "LinkedIn", FAStyle.Brands, "\uf08c")] LinkedinBrands,
        /// <summary>LinkedIn In - linkedin-in - f0e1</summary>
        [FAIcon("linkedin-in", "LinkedIn In", FAStyle.Brands, "\uf0e1")] LinkedinInBrands,
        /// <summary>Linode - linode - f2b8</summary>
        [FAIcon("linode", "Linode", FAStyle.Brands, "\uf2b8")] LinodeBrands,
        /// <summary>Linux - linux - f17c</summary>
        [FAIcon("linux", "Linux", FAStyle.Brands, "\uf17c")] LinuxBrands,
        /// <summary>Turkish Lira Sign - lira-sign - f195</summary>
        [FAIcon("lira-sign", "Turkish Lira Sign", FAStyle.Solid, "\uf195")] LiraSignSolid,
        /// <summary>List - list - f03a</summary>
        [FAIcon("list", "List", FAStyle.Solid, "\uf03a")] ListSolid,
        /// <summary>Alternate List - list-alt - f022</summary>
        [FAIcon("list-alt", "Alternate List", FAStyle.Solid, "\uf022")] ListAltSolid,
        /// <summary>Alternate List - list-alt - f022</summary>
        [FAIcon("list-alt", "Alternate List", FAStyle.Regular, "\uf022")] ListAltRegular,
        /// <summary>list-ol - list-ol - f0cb</summary>
        [FAIcon("list-ol", "list-ol", FAStyle.Solid, "\uf0cb")] ListOlSolid,
        /// <summary>list-ul - list-ul - f0ca</summary>
        [FAIcon("list-ul", "list-ul", FAStyle.Solid, "\uf0ca")] ListUlSolid,
        /// <summary>location-arrow - location-arrow - f124</summary>
        [FAIcon("location-arrow", "location-arrow", FAStyle.Solid, "\uf124")] LocationArrowSolid,
        /// <summary>lock - lock - f023</summary>
        [FAIcon("lock", "lock", FAStyle.Solid, "\uf023")] LockSolid,
        /// <summary>Lock Open - lock-open - f3c1</summary>
        [FAIcon("lock-open", "Lock Open", FAStyle.Solid, "\uf3c1")] LockOpenSolid,
        /// <summary>Alternate Long Arrow Down - long-arrow-alt-down - f309</summary>
        [FAIcon("long-arrow-alt-down", "Alternate Long Arrow Down", FAStyle.Solid, "\uf309")] LongArrowAltDownSolid,
        /// <summary>Alternate Long Arrow Left - long-arrow-alt-left - f30a</summary>
        [FAIcon("long-arrow-alt-left", "Alternate Long Arrow Left", FAStyle.Solid, "\uf30a")] LongArrowAltLeftSolid,
        /// <summary>Alternate Long Arrow Right - long-arrow-alt-right - f30b</summary>
        [FAIcon("long-arrow-alt-right", "Alternate Long Arrow Right", FAStyle.Solid, "\uf30b")] LongArrowAltRightSolid,
        /// <summary>Alternate Long Arrow Up - long-arrow-alt-up - f30c</summary>
        [FAIcon("long-arrow-alt-up", "Alternate Long Arrow Up", FAStyle.Solid, "\uf30c")] LongArrowAltUpSolid,
        /// <summary>Low Vision - low-vision - f2a8</summary>
        [FAIcon("low-vision", "Low Vision", FAStyle.Solid, "\uf2a8")] LowVisionSolid,
        /// <summary>Luggage Cart - luggage-cart - f59d</summary>
        [FAIcon("luggage-cart", "Luggage Cart", FAStyle.Solid, "\uf59d")] LuggageCartSolid,
        /// <summary>lyft - lyft - f3c3</summary>
        [FAIcon("lyft", "lyft", FAStyle.Brands, "\uf3c3")] LyftBrands,
        /// <summary>Magento - magento - f3c4</summary>
        [FAIcon("magento", "Magento", FAStyle.Brands, "\uf3c4")] MagentoBrands,
        /// <summary>magic - magic - f0d0</summary>
        [FAIcon("magic", "magic", FAStyle.Solid, "\uf0d0")] MagicSolid,
        /// <summary>magnet - magnet - f076</summary>
        [FAIcon("magnet", "magnet", FAStyle.Solid, "\uf076")] MagnetSolid,
        /// <summary>Mailchimp - mailchimp - f59e</summary>
        [FAIcon("mailchimp", "Mailchimp", FAStyle.Brands, "\uf59e")] MailchimpBrands,
        /// <summary>Male - male - f183</summary>
        [FAIcon("male", "Male", FAStyle.Solid, "\uf183")] MaleSolid,
        /// <summary>Mandalorian - mandalorian - f50f</summary>
        [FAIcon("mandalorian", "Mandalorian", FAStyle.Brands, "\uf50f")] MandalorianBrands,
        /// <summary>Map - map - f279</summary>
        [FAIcon("map", "Map", FAStyle.Solid, "\uf279")] MapSolid,
        /// <summary>Map - map - f279</summary>
        [FAIcon("map", "Map", FAStyle.Regular, "\uf279")] MapRegular,
        /// <summary>Map Marked - map-marked - f59f</summary>
        [FAIcon("map-marked", "Map Marked", FAStyle.Solid, "\uf59f")] MapMarkedSolid,
        /// <summary>Map Marked-alt - map-marked-alt - f5a0</summary>
        [FAIcon("map-marked-alt", "Map Marked-alt", FAStyle.Solid, "\uf5a0")] MapMarkedAltSolid,
        /// <summary>map-marker - map-marker - f041</summary>
        [FAIcon("map-marker", "map-marker", FAStyle.Solid, "\uf041")] MapMarkerSolid,
        /// <summary>Alternate Map Marker - map-marker-alt - f3c5</summary>
        [FAIcon("map-marker-alt", "Alternate Map Marker", FAStyle.Solid, "\uf3c5")] MapMarkerAltSolid,
        /// <summary>Map Pin - map-pin - f276</summary>
        [FAIcon("map-pin", "Map Pin", FAStyle.Solid, "\uf276")] MapPinSolid,
        /// <summary>Map Signs - map-signs - f277</summary>
        [FAIcon("map-signs", "Map Signs", FAStyle.Solid, "\uf277")] MapSignsSolid,
        /// <summary>Markdown - markdown - f60f</summary>
        [FAIcon("markdown", "Markdown", FAStyle.Brands, "\uf60f")] MarkdownBrands,
        /// <summary>Marker - marker - f5a1</summary>
        [FAIcon("marker", "Marker", FAStyle.Solid, "\uf5a1")] MarkerSolid,
        /// <summary>Mars - mars - f222</summary>
        [FAIcon("mars", "Mars", FAStyle.Solid, "\uf222")] MarsSolid,
        /// <summary>Mars Double - mars-double - f227</summary>
        [FAIcon("mars-double", "Mars Double", FAStyle.Solid, "\uf227")] MarsDoubleSolid,
        /// <summary>Mars Stroke - mars-stroke - f229</summary>
        [FAIcon("mars-stroke", "Mars Stroke", FAStyle.Solid, "\uf229")] MarsStrokeSolid,
        /// <summary>Mars Stroke Horizontal - mars-stroke-h - f22b</summary>
        [FAIcon("mars-stroke-h", "Mars Stroke Horizontal", FAStyle.Solid, "\uf22b")] MarsStrokeHSolid,
        /// <summary>Mars Stroke Vertical - mars-stroke-v - f22a</summary>
        [FAIcon("mars-stroke-v", "Mars Stroke Vertical", FAStyle.Solid, "\uf22a")] MarsStrokeVSolid,
        /// <summary>Mastodon - mastodon - f4f6</summary>
        [FAIcon("mastodon", "Mastodon", FAStyle.Brands, "\uf4f6")] MastodonBrands,
        /// <summary>MaxCDN - maxcdn - f136</summary>
        [FAIcon("maxcdn", "MaxCDN", FAStyle.Brands, "\uf136")] MaxcdnBrands,
        /// <summary>Medal - medal - f5a2</summary>
        [FAIcon("medal", "Medal", FAStyle.Solid, "\uf5a2")] MedalSolid,
        /// <summary>MedApps - medapps - f3c6</summary>
        [FAIcon("medapps", "MedApps", FAStyle.Brands, "\uf3c6")] MedappsBrands,
        /// <summary>Medium - medium - f23a</summary>
        [FAIcon("medium", "Medium", FAStyle.Brands, "\uf23a")] MediumBrands,
        /// <summary>Medium M - medium-m - f3c7</summary>
        [FAIcon("medium-m", "Medium M", FAStyle.Brands, "\uf3c7")] MediumMBrands,
        /// <summary>medkit - medkit - f0fa</summary>
        [FAIcon("medkit", "medkit", FAStyle.Solid, "\uf0fa")] MedkitSolid,
        /// <summary>MRT - medrt - f3c8</summary>
        [FAIcon("medrt", "MRT", FAStyle.Brands, "\uf3c8")] MedrtBrands,
        /// <summary>Meetup - meetup - f2e0</summary>
        [FAIcon("meetup", "Meetup", FAStyle.Brands, "\uf2e0")] MeetupBrands,
        /// <summary>Megaport - megaport - f5a3</summary>
        [FAIcon("megaport", "Megaport", FAStyle.Brands, "\uf5a3")] MegaportBrands,
        /// <summary>Neutral Face - meh - f11a</summary>
        [FAIcon("meh", "Neutral Face", FAStyle.Solid, "\uf11a")] MehSolid,
        /// <summary>Neutral Face - meh - f11a</summary>
        [FAIcon("meh", "Neutral Face", FAStyle.Regular, "\uf11a")] MehRegular,
        /// <summary>Face Without Mouth - meh-blank - f5a4</summary>
        [FAIcon("meh-blank", "Face Without Mouth", FAStyle.Solid, "\uf5a4")] MehBlankSolid,
        /// <summary>Face Without Mouth - meh-blank - f5a4</summary>
        [FAIcon("meh-blank", "Face Without Mouth", FAStyle.Regular, "\uf5a4")] MehBlankRegular,
        /// <summary>Face With Rolling Eyes - meh-rolling-eyes - f5a5</summary>
        [FAIcon("meh-rolling-eyes", "Face With Rolling Eyes", FAStyle.Solid, "\uf5a5")] MehRollingEyesSolid,
        /// <summary>Face With Rolling Eyes - meh-rolling-eyes - f5a5</summary>
        [FAIcon("meh-rolling-eyes", "Face With Rolling Eyes", FAStyle.Regular, "\uf5a5")] MehRollingEyesRegular,
        /// <summary>Memory - memory - f538</summary>
        [FAIcon("memory", "Memory", FAStyle.Solid, "\uf538")] MemorySolid,
        /// <summary>Mercury - mercury - f223</summary>
        [FAIcon("mercury", "Mercury", FAStyle.Solid, "\uf223")] MercurySolid,
        /// <summary>Microchip - microchip - f2db</summary>
        [FAIcon("microchip", "Microchip", FAStyle.Solid, "\uf2db")] MicrochipSolid,
        /// <summary>microphone - microphone - f130</summary>
        [FAIcon("microphone", "microphone", FAStyle.Solid, "\uf130")] MicrophoneSolid,
        /// <summary>Alternate Microphone - microphone-alt - f3c9</summary>
        [FAIcon("microphone-alt", "Alternate Microphone", FAStyle.Solid, "\uf3c9")] MicrophoneAltSolid,
        /// <summary>Alternate Microphone Slash - microphone-alt-slash - f539</summary>
        [FAIcon("microphone-alt-slash", "Alternate Microphone Slash", FAStyle.Solid, "\uf539")] MicrophoneAltSlashSolid,
        /// <summary>Microphone Slash - microphone-slash - f131</summary>
        [FAIcon("microphone-slash", "Microphone Slash", FAStyle.Solid, "\uf131")] MicrophoneSlashSolid,
        /// <summary>Microscope - microscope - f610</summary>
        [FAIcon("microscope", "Microscope", FAStyle.Solid, "\uf610")] MicroscopeSolid,
        /// <summary>Microsoft - microsoft - f3ca</summary>
        [FAIcon("microsoft", "Microsoft", FAStyle.Brands, "\uf3ca")] MicrosoftBrands,
        /// <summary>minus - minus - f068</summary>
        [FAIcon("minus", "minus", FAStyle.Solid, "\uf068")] MinusSolid,
        /// <summary>Minus Circle - minus-circle - f056</summary>
        [FAIcon("minus-circle", "Minus Circle", FAStyle.Solid, "\uf056")] MinusCircleSolid,
        /// <summary>Minus Square - minus-square - f146</summary>
        [FAIcon("minus-square", "Minus Square", FAStyle.Solid, "\uf146")] MinusSquareSolid,
        /// <summary>Minus Square - minus-square - f146</summary>
        [FAIcon("minus-square", "Minus Square", FAStyle.Regular, "\uf146")] MinusSquareRegular,
        /// <summary>Mix - mix - f3cb</summary>
        [FAIcon("mix", "Mix", FAStyle.Brands, "\uf3cb")] MixBrands,
        /// <summary>Mixcloud - mixcloud - f289</summary>
        [FAIcon("mixcloud", "Mixcloud", FAStyle.Brands, "\uf289")] MixcloudBrands,
        /// <summary>Mizuni - mizuni - f3cc</summary>
        [FAIcon("mizuni", "Mizuni", FAStyle.Brands, "\uf3cc")] MizuniBrands,
        /// <summary>Mobile Phone - mobile - f10b</summary>
        [FAIcon("mobile", "Mobile Phone", FAStyle.Solid, "\uf10b")] MobileSolid,
        /// <summary>Alternate Mobile - mobile-alt - f3cd</summary>
        [FAIcon("mobile-alt", "Alternate Mobile", FAStyle.Solid, "\uf3cd")] MobileAltSolid,
        /// <summary>MODX - modx - f285</summary>
        [FAIcon("modx", "MODX", FAStyle.Brands, "\uf285")] ModxBrands,
        /// <summary>Monero - monero - f3d0</summary>
        [FAIcon("monero", "Monero", FAStyle.Brands, "\uf3d0")] MoneroBrands,
        /// <summary>Money Bill - money-bill - f0d6</summary>
        [FAIcon("money-bill", "Money Bill", FAStyle.Solid, "\uf0d6")] MoneyBillSolid,
        /// <summary>Alternate Money Bill - money-bill-alt - f3d1</summary>
        [FAIcon("money-bill-alt", "Alternate Money Bill", FAStyle.Solid, "\uf3d1")] MoneyBillAltSolid,
        /// <summary>Alternate Money Bill - money-bill-alt - f3d1</summary>
        [FAIcon("money-bill-alt", "Alternate Money Bill", FAStyle.Regular, "\uf3d1")] MoneyBillAltRegular,
        /// <summary>Wavy Money Bill - money-bill-wave - f53a</summary>
        [FAIcon("money-bill-wave", "Wavy Money Bill", FAStyle.Solid, "\uf53a")] MoneyBillWaveSolid,
        /// <summary>Alternate Wavy Money Bill - money-bill-wave-alt - f53b</summary>
        [FAIcon("money-bill-wave-alt", "Alternate Wavy Money Bill", FAStyle.Solid, "\uf53b")] MoneyBillWaveAltSolid,
        /// <summary>Money Check - money-check - f53c</summary>
        [FAIcon("money-check", "Money Check", FAStyle.Solid, "\uf53c")] MoneyCheckSolid,
        /// <summary>Alternate Money Check - money-check-alt - f53d</summary>
        [FAIcon("money-check-alt", "Alternate Money Check", FAStyle.Solid, "\uf53d")] MoneyCheckAltSolid,
        /// <summary>Monument - monument - f5a6</summary>
        [FAIcon("monument", "Monument", FAStyle.Solid, "\uf5a6")] MonumentSolid,
        /// <summary>Moon - moon - f186</summary>
        [FAIcon("moon", "Moon", FAStyle.Solid, "\uf186")] MoonSolid,
        /// <summary>Moon - moon - f186</summary>
        [FAIcon("moon", "Moon", FAStyle.Regular, "\uf186")] MoonRegular,
        /// <summary>Mortar Pestle - mortar-pestle - f5a7</summary>
        [FAIcon("mortar-pestle", "Mortar Pestle", FAStyle.Solid, "\uf5a7")] MortarPestleSolid,
        /// <summary>Motorcycle - motorcycle - f21c</summary>
        [FAIcon("motorcycle", "Motorcycle", FAStyle.Solid, "\uf21c")] MotorcycleSolid,
        /// <summary>Mouse Pointer - mouse-pointer - f245</summary>
        [FAIcon("mouse-pointer", "Mouse Pointer", FAStyle.Solid, "\uf245")] MousePointerSolid,
        /// <summary>Music - music - f001</summary>
        [FAIcon("music", "Music", FAStyle.Solid, "\uf001")] MusicSolid,
        /// <summary>Napster - napster - f3d2</summary>
        [FAIcon("napster", "Napster", FAStyle.Brands, "\uf3d2")] NapsterBrands,
        /// <summary>Neos - neos - f612</summary>
        [FAIcon("neos", "Neos", FAStyle.Brands, "\uf612")] NeosBrands,
        /// <summary>Neuter - neuter - f22c</summary>
        [FAIcon("neuter", "Neuter", FAStyle.Solid, "\uf22c")] NeuterSolid,
        /// <summary>Newspaper - newspaper - f1ea</summary>
        [FAIcon("newspaper", "Newspaper", FAStyle.Solid, "\uf1ea")] NewspaperSolid,
        /// <summary>Newspaper - newspaper - f1ea</summary>
        [FAIcon("newspaper", "Newspaper", FAStyle.Regular, "\uf1ea")] NewspaperRegular,
        /// <summary>Nimblr - nimblr - f5a8</summary>
        [FAIcon("nimblr", "Nimblr", FAStyle.Brands, "\uf5a8")] NimblrBrands,
        /// <summary>Nintendo Switch - nintendo-switch - f418</summary>
        [FAIcon("nintendo-switch", "Nintendo Switch", FAStyle.Brands, "\uf418")] NintendoSwitchBrands,
        /// <summary>Node.js - node - f419</summary>
        [FAIcon("node", "Node.js", FAStyle.Brands, "\uf419")] NodeBrands,
        /// <summary>Node.js JS - node-js - f3d3</summary>
        [FAIcon("node-js", "Node.js JS", FAStyle.Brands, "\uf3d3")] NodeJsBrands,
        /// <summary>Not Equal - not-equal - f53e</summary>
        [FAIcon("not-equal", "Not Equal", FAStyle.Solid, "\uf53e")] NotEqualSolid,
        /// <summary>Medical Notes - notes-medical - f481</summary>
        [FAIcon("notes-medical", "Medical Notes", FAStyle.Solid, "\uf481")] NotesMedicalSolid,
        /// <summary>npm - npm - f3d4</summary>
        [FAIcon("npm", "npm", FAStyle.Brands, "\uf3d4")] NpmBrands,
        /// <summary>NS8 - ns8 - f3d5</summary>
        [FAIcon("ns8", "NS8", FAStyle.Brands, "\uf3d5")] Ns8Brands,
        /// <summary>Nutritionix - nutritionix - f3d6</summary>
        [FAIcon("nutritionix", "Nutritionix", FAStyle.Brands, "\uf3d6")] NutritionixBrands,
        /// <summary>Object Group - object-group - f247</summary>
        [FAIcon("object-group", "Object Group", FAStyle.Solid, "\uf247")] ObjectGroupSolid,
        /// <summary>Object Group - object-group - f247</summary>
        [FAIcon("object-group", "Object Group", FAStyle.Regular, "\uf247")] ObjectGroupRegular,
        /// <summary>Object Ungroup - object-ungroup - f248</summary>
        [FAIcon("object-ungroup", "Object Ungroup", FAStyle.Solid, "\uf248")] ObjectUngroupSolid,
        /// <summary>Object Ungroup - object-ungroup - f248</summary>
        [FAIcon("object-ungroup", "Object Ungroup", FAStyle.Regular, "\uf248")] ObjectUngroupRegular,
        /// <summary>Odnoklassniki - odnoklassniki - f263</summary>
        [FAIcon("odnoklassniki", "Odnoklassniki", FAStyle.Brands, "\uf263")] OdnoklassnikiBrands,
        /// <summary>Odnoklassniki Square - odnoklassniki-square - f264</summary>
        [FAIcon("odnoklassniki-square", "Odnoklassniki Square", FAStyle.Brands, "\uf264")] OdnoklassnikiSquareBrands,
        /// <summary>Oil Can - oil-can - f613</summary>
        [FAIcon("oil-can", "Oil Can", FAStyle.Solid, "\uf613")] OilCanSolid,
        /// <summary>Old Republic - old-republic - f510</summary>
        [FAIcon("old-republic", "Old Republic", FAStyle.Brands, "\uf510")] OldRepublicBrands,
        /// <summary>OpenCart - opencart - f23d</summary>
        [FAIcon("opencart", "OpenCart", FAStyle.Brands, "\uf23d")] OpencartBrands,
        /// <summary>OpenID - openid - f19b</summary>
        [FAIcon("openid", "OpenID", FAStyle.Brands, "\uf19b")] OpenidBrands,
        /// <summary>Opera - opera - f26a</summary>
        [FAIcon("opera", "Opera", FAStyle.Brands, "\uf26a")] OperaBrands,
        /// <summary>Optin Monster - optin-monster - f23c</summary>
        [FAIcon("optin-monster", "Optin Monster", FAStyle.Brands, "\uf23c")] OptinMonsterBrands,
        /// <summary>Open Source Initiative - osi - f41a</summary>
        [FAIcon("osi", "Open Source Initiative", FAStyle.Brands, "\uf41a")] OsiBrands,
        /// <summary>Outdent - outdent - f03b</summary>
        [FAIcon("outdent", "Outdent", FAStyle.Solid, "\uf03b")] OutdentSolid,
        /// <summary>page4 Corporation - page4 - f3d7</summary>
        [FAIcon("page4", "page4 Corporation", FAStyle.Brands, "\uf3d7")] Page4Brands,
        /// <summary>Pagelines - pagelines - f18c</summary>
        [FAIcon("pagelines", "Pagelines", FAStyle.Brands, "\uf18c")] PagelinesBrands,
        /// <summary>Paint Brush - paint-brush - f1fc</summary>
        [FAIcon("paint-brush", "Paint Brush", FAStyle.Solid, "\uf1fc")] PaintBrushSolid,
        /// <summary>Paint Roller - paint-roller - f5aa</summary>
        [FAIcon("paint-roller", "Paint Roller", FAStyle.Solid, "\uf5aa")] PaintRollerSolid,
        /// <summary>Palette - palette - f53f</summary>
        [FAIcon("palette", "Palette", FAStyle.Solid, "\uf53f")] PaletteSolid,
        /// <summary>Palfed - palfed - f3d8</summary>
        [FAIcon("palfed", "Palfed", FAStyle.Brands, "\uf3d8")] PalfedBrands,
        /// <summary>Pallet - pallet - f482</summary>
        [FAIcon("pallet", "Pallet", FAStyle.Solid, "\uf482")] PalletSolid,
        /// <summary>Paper Plane - paper-plane - f1d8</summary>
        [FAIcon("paper-plane", "Paper Plane", FAStyle.Solid, "\uf1d8")] PaperPlaneSolid,
        /// <summary>Paper Plane - paper-plane - f1d8</summary>
        [FAIcon("paper-plane", "Paper Plane", FAStyle.Regular, "\uf1d8")] PaperPlaneRegular,
        /// <summary>Paperclip - paperclip - f0c6</summary>
        [FAIcon("paperclip", "Paperclip", FAStyle.Solid, "\uf0c6")] PaperclipSolid,
        /// <summary>Parachute Box - parachute-box - f4cd</summary>
        [FAIcon("parachute-box", "Parachute Box", FAStyle.Solid, "\uf4cd")] ParachuteBoxSolid,
        /// <summary>paragraph - paragraph - f1dd</summary>
        [FAIcon("paragraph", "paragraph", FAStyle.Solid, "\uf1dd")] ParagraphSolid,
        /// <summary>Parking - parking - f540</summary>
        [FAIcon("parking", "Parking", FAStyle.Solid, "\uf540")] ParkingSolid,
        /// <summary>Passport - passport - f5ab</summary>
        [FAIcon("passport", "Passport", FAStyle.Solid, "\uf5ab")] PassportSolid,
        /// <summary>Paste - paste - f0ea</summary>
        [FAIcon("paste", "Paste", FAStyle.Solid, "\uf0ea")] PasteSolid,
        /// <summary>Patreon - patreon - f3d9</summary>
        [FAIcon("patreon", "Patreon", FAStyle.Brands, "\uf3d9")] PatreonBrands,
        /// <summary>pause - pause - f04c</summary>
        [FAIcon("pause", "pause", FAStyle.Solid, "\uf04c")] PauseSolid,
        /// <summary>Pause Circle - pause-circle - f28b</summary>
        [FAIcon("pause-circle", "Pause Circle", FAStyle.Solid, "\uf28b")] PauseCircleSolid,
        /// <summary>Pause Circle - pause-circle - f28b</summary>
        [FAIcon("pause-circle", "Pause Circle", FAStyle.Regular, "\uf28b")] PauseCircleRegular,
        /// <summary>Paw - paw - f1b0</summary>
        [FAIcon("paw", "Paw", FAStyle.Solid, "\uf1b0")] PawSolid,
        /// <summary>Paypal - paypal - f1ed</summary>
        [FAIcon("paypal", "Paypal", FAStyle.Brands, "\uf1ed")] PaypalBrands,
        /// <summary>Pen - pen - f304</summary>
        [FAIcon("pen", "Pen", FAStyle.Solid, "\uf304")] PenSolid,
        /// <summary>Alternate Pen - pen-alt - f305</summary>
        [FAIcon("pen-alt", "Alternate Pen", FAStyle.Solid, "\uf305")] PenAltSolid,
        /// <summary>Pen Fancy - pen-fancy - f5ac</summary>
        [FAIcon("pen-fancy", "Pen Fancy", FAStyle.Solid, "\uf5ac")] PenFancySolid,
        /// <summary>Pen Nib - pen-nib - f5ad</summary>
        [FAIcon("pen-nib", "Pen Nib", FAStyle.Solid, "\uf5ad")] PenNibSolid,
        /// <summary>Pen Square - pen-square - f14b</summary>
        [FAIcon("pen-square", "Pen Square", FAStyle.Solid, "\uf14b")] PenSquareSolid,
        /// <summary>Alternate Pencil - pencil-alt - f303</summary>
        [FAIcon("pencil-alt", "Alternate Pencil", FAStyle.Solid, "\uf303")] PencilAltSolid,
        /// <summary>Pencil Ruler - pencil-ruler - f5ae</summary>
        [FAIcon("pencil-ruler", "Pencil Ruler", FAStyle.Solid, "\uf5ae")] PencilRulerSolid,
        /// <summary>People Carry - people-carry - f4ce</summary>
        [FAIcon("people-carry", "People Carry", FAStyle.Solid, "\uf4ce")] PeopleCarrySolid,
        /// <summary>Percent - percent - f295</summary>
        [FAIcon("percent", "Percent", FAStyle.Solid, "\uf295")] PercentSolid,
        /// <summary>Percentage - percentage - f541</summary>
        [FAIcon("percentage", "Percentage", FAStyle.Solid, "\uf541")] PercentageSolid,
        /// <summary>Periscope - periscope - f3da</summary>
        [FAIcon("periscope", "Periscope", FAStyle.Brands, "\uf3da")] PeriscopeBrands,
        /// <summary>Phabricator - phabricator - f3db</summary>
        [FAIcon("phabricator", "Phabricator", FAStyle.Brands, "\uf3db")] PhabricatorBrands,
        /// <summary>Phoenix Framework - phoenix-framework - f3dc</summary>
        [FAIcon("phoenix-framework", "Phoenix Framework", FAStyle.Brands, "\uf3dc")] PhoenixFrameworkBrands,
        /// <summary>Phoenix Squadron - phoenix-squadron - f511</summary>
        [FAIcon("phoenix-squadron", "Phoenix Squadron", FAStyle.Brands, "\uf511")] PhoenixSquadronBrands,
        /// <summary>Phone - phone - f095</summary>
        [FAIcon("phone", "Phone", FAStyle.Solid, "\uf095")] PhoneSolid,
        /// <summary>Phone Slash - phone-slash - f3dd</summary>
        [FAIcon("phone-slash", "Phone Slash", FAStyle.Solid, "\uf3dd")] PhoneSlashSolid,
        /// <summary>Phone Square - phone-square - f098</summary>
        [FAIcon("phone-square", "Phone Square", FAStyle.Solid, "\uf098")] PhoneSquareSolid,
        /// <summary>Phone Volume - phone-volume - f2a0</summary>
        [FAIcon("phone-volume", "Phone Volume", FAStyle.Solid, "\uf2a0")] PhoneVolumeSolid,
        /// <summary>PHP - php - f457</summary>
        [FAIcon("php", "PHP", FAStyle.Brands, "\uf457")] PhpBrands,
        /// <summary>Pied Piper Logo - pied-piper - f2ae</summary>
        [FAIcon("pied-piper", "Pied Piper Logo", FAStyle.Brands, "\uf2ae")] PiedPiperBrands,
        /// <summary>Alternate Pied Piper Logo - pied-piper-alt - f1a8</summary>
        [FAIcon("pied-piper-alt", "Alternate Pied Piper Logo", FAStyle.Brands, "\uf1a8")] PiedPiperAltBrands,
        /// <summary>Pied Piper-hat - pied-piper-hat - f4e5</summary>
        [FAIcon("pied-piper-hat", "Pied Piper-hat", FAStyle.Brands, "\uf4e5")] PiedPiperHatBrands,
        /// <summary>Pied Piper PP Logo (Old) - pied-piper-pp - f1a7</summary>
        [FAIcon("pied-piper-pp", "Pied Piper PP Logo (Old)", FAStyle.Brands, "\uf1a7")] PiedPiperPpBrands,
        /// <summary>Piggy Bank - piggy-bank - f4d3</summary>
        [FAIcon("piggy-bank", "Piggy Bank", FAStyle.Solid, "\uf4d3")] PiggyBankSolid,
        /// <summary>Pills - pills - f484</summary>
        [FAIcon("pills", "Pills", FAStyle.Solid, "\uf484")] PillsSolid,
        /// <summary>Pinterest - pinterest - f0d2</summary>
        [FAIcon("pinterest", "Pinterest", FAStyle.Brands, "\uf0d2")] PinterestBrands,
        /// <summary>Pinterest P - pinterest-p - f231</summary>
        [FAIcon("pinterest-p", "Pinterest P", FAStyle.Brands, "\uf231")] PinterestPBrands,
        /// <summary>Pinterest Square - pinterest-square - f0d3</summary>
        [FAIcon("pinterest-square", "Pinterest Square", FAStyle.Brands, "\uf0d3")] PinterestSquareBrands,
        /// <summary>plane - plane - f072</summary>
        [FAIcon("plane", "plane", FAStyle.Solid, "\uf072")] PlaneSolid,
        /// <summary>Plane Arrival - plane-arrival - f5af</summary>
        [FAIcon("plane-arrival", "Plane Arrival", FAStyle.Solid, "\uf5af")] PlaneArrivalSolid,
        /// <summary>Plane Departure - plane-departure - f5b0</summary>
        [FAIcon("plane-departure", "Plane Departure", FAStyle.Solid, "\uf5b0")] PlaneDepartureSolid,
        /// <summary>play - play - f04b</summary>
        [FAIcon("play", "play", FAStyle.Solid, "\uf04b")] PlaySolid,
        /// <summary>Play Circle - play-circle - f144</summary>
        [FAIcon("play-circle", "Play Circle", FAStyle.Solid, "\uf144")] PlayCircleSolid,
        /// <summary>Play Circle - play-circle - f144</summary>
        [FAIcon("play-circle", "Play Circle", FAStyle.Regular, "\uf144")] PlayCircleRegular,
        /// <summary>PlayStation - playstation - f3df</summary>
        [FAIcon("playstation", "PlayStation", FAStyle.Brands, "\uf3df")] PlaystationBrands,
        /// <summary>Plug - plug - f1e6</summary>
        [FAIcon("plug", "Plug", FAStyle.Solid, "\uf1e6")] PlugSolid,
        /// <summary>plus - plus - f067</summary>
        [FAIcon("plus", "plus", FAStyle.Solid, "\uf067")] PlusSolid,
        /// <summary>Plus Circle - plus-circle - f055</summary>
        [FAIcon("plus-circle", "Plus Circle", FAStyle.Solid, "\uf055")] PlusCircleSolid,
        /// <summary>Plus Square - plus-square - f0fe</summary>
        [FAIcon("plus-square", "Plus Square", FAStyle.Solid, "\uf0fe")] PlusSquareSolid,
        /// <summary>Plus Square - plus-square - f0fe</summary>
        [FAIcon("plus-square", "Plus Square", FAStyle.Regular, "\uf0fe")] PlusSquareRegular,
        /// <summary>Podcast - podcast - f2ce</summary>
        [FAIcon("podcast", "Podcast", FAStyle.Solid, "\uf2ce")] PodcastSolid,
        /// <summary>Poo - poo - f2fe</summary>
        [FAIcon("poo", "Poo", FAStyle.Solid, "\uf2fe")] PooSolid,
        /// <summary>Poop - poop - f619</summary>
        [FAIcon("poop", "Poop", FAStyle.Solid, "\uf619")] PoopSolid,
        /// <summary>Portrait - portrait - f3e0</summary>
        [FAIcon("portrait", "Portrait", FAStyle.Solid, "\uf3e0")] PortraitSolid,
        /// <summary>Pound Sign - pound-sign - f154</summary>
        [FAIcon("pound-sign", "Pound Sign", FAStyle.Solid, "\uf154")] PoundSignSolid,
        /// <summary>Power Off - power-off - f011</summary>
        [FAIcon("power-off", "Power Off", FAStyle.Solid, "\uf011")] PowerOffSolid,
        /// <summary>Prescription - prescription - f5b1</summary>
        [FAIcon("prescription", "Prescription", FAStyle.Solid, "\uf5b1")] PrescriptionSolid,
        /// <summary>Prescription Bottle - prescription-bottle - f485</summary>
        [FAIcon("prescription-bottle", "Prescription Bottle", FAStyle.Solid, "\uf485")] PrescriptionBottleSolid,
        /// <summary>Alternate Prescription Bottle - prescription-bottle-alt - f486</summary>
        [FAIcon("prescription-bottle-alt", "Alternate Prescription Bottle", FAStyle.Solid, "\uf486")] PrescriptionBottleAltSolid,
        /// <summary>print - print - f02f</summary>
        [FAIcon("print", "print", FAStyle.Solid, "\uf02f")] PrintSolid,
        /// <summary>Procedures - procedures - f487</summary>
        [FAIcon("procedures", "Procedures", FAStyle.Solid, "\uf487")] ProceduresSolid,
        /// <summary>Product Hunt - product-hunt - f288</summary>
        [FAIcon("product-hunt", "Product Hunt", FAStyle.Brands, "\uf288")] ProductHuntBrands,
        /// <summary>Project Diagram - project-diagram - f542</summary>
        [FAIcon("project-diagram", "Project Diagram", FAStyle.Solid, "\uf542")] ProjectDiagramSolid,
        /// <summary>Pushed - pushed - f3e1</summary>
        [FAIcon("pushed", "Pushed", FAStyle.Brands, "\uf3e1")] PushedBrands,
        /// <summary>Puzzle Piece - puzzle-piece - f12e</summary>
        [FAIcon("puzzle-piece", "Puzzle Piece", FAStyle.Solid, "\uf12e")] PuzzlePieceSolid,
        /// <summary>Python - python - f3e2</summary>
        [FAIcon("python", "Python", FAStyle.Brands, "\uf3e2")] PythonBrands,
        /// <summary>QQ - qq - f1d6</summary>
        [FAIcon("qq", "QQ", FAStyle.Brands, "\uf1d6")] QqBrands,
        /// <summary>qrcode - qrcode - f029</summary>
        [FAIcon("qrcode", "qrcode", FAStyle.Solid, "\uf029")] QrcodeSolid,
        /// <summary>Question - question - f128</summary>
        [FAIcon("question", "Question", FAStyle.Solid, "\uf128")] QuestionSolid,
        /// <summary>Question Circle - question-circle - f059</summary>
        [FAIcon("question-circle", "Question Circle", FAStyle.Solid, "\uf059")] QuestionCircleSolid,
        /// <summary>Question Circle - question-circle - f059</summary>
        [FAIcon("question-circle", "Question Circle", FAStyle.Regular, "\uf059")] QuestionCircleRegular,
        /// <summary>Quidditch - quidditch - f458</summary>
        [FAIcon("quidditch", "Quidditch", FAStyle.Solid, "\uf458")] QuidditchSolid,
        /// <summary>QuinScape - quinscape - f459</summary>
        [FAIcon("quinscape", "QuinScape", FAStyle.Brands, "\uf459")] QuinscapeBrands,
        /// <summary>Quora - quora - f2c4</summary>
        [FAIcon("quora", "Quora", FAStyle.Brands, "\uf2c4")] QuoraBrands,
        /// <summary>quote-left - quote-left - f10d</summary>
        [FAIcon("quote-left", "quote-left", FAStyle.Solid, "\uf10d")] QuoteLeftSolid,
        /// <summary>quote-right - quote-right - f10e</summary>
        [FAIcon("quote-right", "quote-right", FAStyle.Solid, "\uf10e")] QuoteRightSolid,
        /// <summary>R Project - r-project - f4f7</summary>
        [FAIcon("r-project", "R Project", FAStyle.Brands, "\uf4f7")] RProjectBrands,
        /// <summary>random - random - f074</summary>
        [FAIcon("random", "random", FAStyle.Solid, "\uf074")] RandomSolid,
        /// <summary>Ravelry - ravelry - f2d9</summary>
        [FAIcon("ravelry", "Ravelry", FAStyle.Brands, "\uf2d9")] RavelryBrands,
        /// <summary>React - react - f41b</summary>
        [FAIcon("react", "React", FAStyle.Brands, "\uf41b")] ReactBrands,
        /// <summary>ReadMe - readme - f4d5</summary>
        [FAIcon("readme", "ReadMe", FAStyle.Brands, "\uf4d5")] ReadmeBrands,
        /// <summary>Rebel Alliance - rebel - f1d0</summary>
        [FAIcon("rebel", "Rebel Alliance", FAStyle.Brands, "\uf1d0")] RebelBrands,
        /// <summary>Receipt - receipt - f543</summary>
        [FAIcon("receipt", "Receipt", FAStyle.Solid, "\uf543")] ReceiptSolid,
        /// <summary>Recycle - recycle - f1b8</summary>
        [FAIcon("recycle", "Recycle", FAStyle.Solid, "\uf1b8")] RecycleSolid,
        /// <summary>red river - red-river - f3e3</summary>
        [FAIcon("red-river", "red river", FAStyle.Brands, "\uf3e3")] RedRiverBrands,
        /// <summary>reddit Logo - reddit - f1a1</summary>
        [FAIcon("reddit", "reddit Logo", FAStyle.Brands, "\uf1a1")] RedditBrands,
        /// <summary>reddit Alien - reddit-alien - f281</summary>
        [FAIcon("reddit-alien", "reddit Alien", FAStyle.Brands, "\uf281")] RedditAlienBrands,
        /// <summary>reddit Square - reddit-square - f1a2</summary>
        [FAIcon("reddit-square", "reddit Square", FAStyle.Brands, "\uf1a2")] RedditSquareBrands,
        /// <summary>Redo - redo - f01e</summary>
        [FAIcon("redo", "Redo", FAStyle.Solid, "\uf01e")] RedoSolid,
        /// <summary>Alternate Redo - redo-alt - f2f9</summary>
        [FAIcon("redo-alt", "Alternate Redo", FAStyle.Solid, "\uf2f9")] RedoAltSolid,
        /// <summary>Registered Trademark - registered - f25d</summary>
        [FAIcon("registered", "Registered Trademark", FAStyle.Solid, "\uf25d")] RegisteredSolid,
        /// <summary>Registered Trademark - registered - f25d</summary>
        [FAIcon("registered", "Registered Trademark", FAStyle.Regular, "\uf25d")] RegisteredRegular,
        /// <summary>Rendact - rendact - f3e4</summary>
        [FAIcon("rendact", "Rendact", FAStyle.Brands, "\uf3e4")] RendactBrands,
        /// <summary>Renren - renren - f18b</summary>
        [FAIcon("renren", "Renren", FAStyle.Brands, "\uf18b")] RenrenBrands,
        /// <summary>Reply - reply - f3e5</summary>
        [FAIcon("reply", "Reply", FAStyle.Solid, "\uf3e5")] ReplySolid,
        /// <summary>reply-all - reply-all - f122</summary>
        [FAIcon("reply-all", "reply-all", FAStyle.Solid, "\uf122")] ReplyAllSolid,
        /// <summary>replyd - replyd - f3e6</summary>
        [FAIcon("replyd", "replyd", FAStyle.Brands, "\uf3e6")] ReplydBrands,
        /// <summary>Researchgate - researchgate - f4f8</summary>
        [FAIcon("researchgate", "Researchgate", FAStyle.Brands, "\uf4f8")] ResearchgateBrands,
        /// <summary>Resolving - resolving - f3e7</summary>
        [FAIcon("resolving", "Resolving", FAStyle.Brands, "\uf3e7")] ResolvingBrands,
        /// <summary>Retweet - retweet - f079</summary>
        [FAIcon("retweet", "Retweet", FAStyle.Solid, "\uf079")] RetweetSolid,
        /// <summary>Rev.io - rev - f5b2</summary>
        [FAIcon("rev", "Rev.io", FAStyle.Brands, "\uf5b2")] RevBrands,
        /// <summary>Ribbon - ribbon - f4d6</summary>
        [FAIcon("ribbon", "Ribbon", FAStyle.Solid, "\uf4d6")] RibbonSolid,
        /// <summary>road - road - f018</summary>
        [FAIcon("road", "road", FAStyle.Solid, "\uf018")] RoadSolid,
        /// <summary>Robot - robot - f544</summary>
        [FAIcon("robot", "Robot", FAStyle.Solid, "\uf544")] RobotSolid,
        /// <summary>rocket - rocket - f135</summary>
        [FAIcon("rocket", "rocket", FAStyle.Solid, "\uf135")] RocketSolid,
        /// <summary>Rocket.Chat - rocketchat - f3e8</summary>
        [FAIcon("rocketchat", "Rocket.Chat", FAStyle.Brands, "\uf3e8")] RocketchatBrands,
        /// <summary>Rockrms - rockrms - f3e9</summary>
        [FAIcon("rockrms", "Rockrms", FAStyle.Brands, "\uf3e9")] RockrmsBrands,
        /// <summary>Route - route - f4d7</summary>
        [FAIcon("route", "Route", FAStyle.Solid, "\uf4d7")] RouteSolid,
        /// <summary>rss - rss - f09e</summary>
        [FAIcon("rss", "rss", FAStyle.Solid, "\uf09e")] RssSolid,
        /// <summary>RSS Square - rss-square - f143</summary>
        [FAIcon("rss-square", "RSS Square", FAStyle.Solid, "\uf143")] RssSquareSolid,
        /// <summary>Ruble Sign - ruble-sign - f158</summary>
        [FAIcon("ruble-sign", "Ruble Sign", FAStyle.Solid, "\uf158")] RubleSignSolid,
        /// <summary>Ruler - ruler - f545</summary>
        [FAIcon("ruler", "Ruler", FAStyle.Solid, "\uf545")] RulerSolid,
        /// <summary>Ruler Combined - ruler-combined - f546</summary>
        [FAIcon("ruler-combined", "Ruler Combined", FAStyle.Solid, "\uf546")] RulerCombinedSolid,
        /// <summary>Ruler Horizontal - ruler-horizontal - f547</summary>
        [FAIcon("ruler-horizontal", "Ruler Horizontal", FAStyle.Solid, "\uf547")] RulerHorizontalSolid,
        /// <summary>Ruler Vertical - ruler-vertical - f548</summary>
        [FAIcon("ruler-vertical", "Ruler Vertical", FAStyle.Solid, "\uf548")] RulerVerticalSolid,
        /// <summary>Indian Rupee Sign - rupee-sign - f156</summary>
        [FAIcon("rupee-sign", "Indian Rupee Sign", FAStyle.Solid, "\uf156")] RupeeSignSolid,
        /// <summary>Crying Face - sad-cry - f5b3</summary>
        [FAIcon("sad-cry", "Crying Face", FAStyle.Solid, "\uf5b3")] SadCrySolid,
        /// <summary>Crying Face - sad-cry - f5b3</summary>
        [FAIcon("sad-cry", "Crying Face", FAStyle.Regular, "\uf5b3")] SadCryRegular,
        /// <summary>Loudly Crying Face - sad-tear - f5b4</summary>
        [FAIcon("sad-tear", "Loudly Crying Face", FAStyle.Solid, "\uf5b4")] SadTearSolid,
        /// <summary>Loudly Crying Face - sad-tear - f5b4</summary>
        [FAIcon("sad-tear", "Loudly Crying Face", FAStyle.Regular, "\uf5b4")] SadTearRegular,
        /// <summary>Safari - safari - f267</summary>
        [FAIcon("safari", "Safari", FAStyle.Brands, "\uf267")] SafariBrands,
        /// <summary>Sass - sass - f41e</summary>
        [FAIcon("sass", "Sass", FAStyle.Brands, "\uf41e")] SassBrands,
        /// <summary>Save - save - f0c7</summary>
        [FAIcon("save", "Save", FAStyle.Solid, "\uf0c7")] SaveSolid,
        /// <summary>Save - save - f0c7</summary>
        [FAIcon("save", "Save", FAStyle.Regular, "\uf0c7")] SaveRegular,
        /// <summary>SCHLIX - schlix - f3ea</summary>
        [FAIcon("schlix", "SCHLIX", FAStyle.Brands, "\uf3ea")] SchlixBrands,
        /// <summary>School - school - f549</summary>
        [FAIcon("school", "School", FAStyle.Solid, "\uf549")] SchoolSolid,
        /// <summary>Screwdriver - screwdriver - f54a</summary>
        [FAIcon("screwdriver", "Screwdriver", FAStyle.Solid, "\uf54a")] ScrewdriverSolid,
        /// <summary>Scribd - scribd - f28a</summary>
        [FAIcon("scribd", "Scribd", FAStyle.Brands, "\uf28a")] ScribdBrands,
        /// <summary>Search - search - f002</summary>
        [FAIcon("search", "Search", FAStyle.Solid, "\uf002")] SearchSolid,
        /// <summary>Search Minus - search-minus - f010</summary>
        [FAIcon("search-minus", "Search Minus", FAStyle.Solid, "\uf010")] SearchMinusSolid,
        /// <summary>Search Plus - search-plus - f00e</summary>
        [FAIcon("search-plus", "Search Plus", FAStyle.Solid, "\uf00e")] SearchPlusSolid,
        /// <summary>Searchengin - searchengin - f3eb</summary>
        [FAIcon("searchengin", "Searchengin", FAStyle.Brands, "\uf3eb")] SearchenginBrands,
        /// <summary>Seedling - seedling - f4d8</summary>
        [FAIcon("seedling", "Seedling", FAStyle.Solid, "\uf4d8")] SeedlingSolid,
        /// <summary>Sellcast - sellcast - f2da</summary>
        [FAIcon("sellcast", "Sellcast", FAStyle.Brands, "\uf2da")] SellcastBrands,
        /// <summary>Sellsy - sellsy - f213</summary>
        [FAIcon("sellsy", "Sellsy", FAStyle.Brands, "\uf213")] SellsyBrands,
        /// <summary>Server - server - f233</summary>
        [FAIcon("server", "Server", FAStyle.Solid, "\uf233")] ServerSolid,
        /// <summary>Servicestack - servicestack - f3ec</summary>
        [FAIcon("servicestack", "Servicestack", FAStyle.Brands, "\uf3ec")] ServicestackBrands,
        /// <summary>Shapes - shapes - f61f</summary>
        [FAIcon("shapes", "Shapes", FAStyle.Solid, "\uf61f")] ShapesSolid,
        /// <summary>Share - share - f064</summary>
        [FAIcon("share", "Share", FAStyle.Solid, "\uf064")] ShareSolid,
        /// <summary>Alternate Share - share-alt - f1e0</summary>
        [FAIcon("share-alt", "Alternate Share", FAStyle.Solid, "\uf1e0")] ShareAltSolid,
        /// <summary>Alternate Share Square - share-alt-square - f1e1</summary>
        [FAIcon("share-alt-square", "Alternate Share Square", FAStyle.Solid, "\uf1e1")] ShareAltSquareSolid,
        /// <summary>Share Square - share-square - f14d</summary>
        [FAIcon("share-square", "Share Square", FAStyle.Solid, "\uf14d")] ShareSquareSolid,
        /// <summary>Share Square - share-square - f14d</summary>
        [FAIcon("share-square", "Share Square", FAStyle.Regular, "\uf14d")] ShareSquareRegular,
        /// <summary>Shekel Sign - shekel-sign - f20b</summary>
        [FAIcon("shekel-sign", "Shekel Sign", FAStyle.Solid, "\uf20b")] ShekelSignSolid,
        /// <summary>Alternate Shield - shield-alt - f3ed</summary>
        [FAIcon("shield-alt", "Alternate Shield", FAStyle.Solid, "\uf3ed")] ShieldAltSolid,
        /// <summary>Ship - ship - f21a</summary>
        [FAIcon("ship", "Ship", FAStyle.Solid, "\uf21a")] ShipSolid,
        /// <summary>Shipping Fast - shipping-fast - f48b</summary>
        [FAIcon("shipping-fast", "Shipping Fast", FAStyle.Solid, "\uf48b")] ShippingFastSolid,
        /// <summary>Shirts in Bulk - shirtsinbulk - f214</summary>
        [FAIcon("shirtsinbulk", "Shirts in Bulk", FAStyle.Brands, "\uf214")] ShirtsinbulkBrands,
        /// <summary>Shoe Prints - shoe-prints - f54b</summary>
        [FAIcon("shoe-prints", "Shoe Prints", FAStyle.Solid, "\uf54b")] ShoePrintsSolid,
        /// <summary>Shopping Bag - shopping-bag - f290</summary>
        [FAIcon("shopping-bag", "Shopping Bag", FAStyle.Solid, "\uf290")] ShoppingBagSolid,
        /// <summary>Shopping Basket - shopping-basket - f291</summary>
        [FAIcon("shopping-basket", "Shopping Basket", FAStyle.Solid, "\uf291")] ShoppingBasketSolid,
        /// <summary>shopping-cart - shopping-cart - f07a</summary>
        [FAIcon("shopping-cart", "shopping-cart", FAStyle.Solid, "\uf07a")] ShoppingCartSolid,
        /// <summary>Shopware - shopware - f5b5</summary>
        [FAIcon("shopware", "Shopware", FAStyle.Brands, "\uf5b5")] ShopwareBrands,
        /// <summary>Shower - shower - f2cc</summary>
        [FAIcon("shower", "Shower", FAStyle.Solid, "\uf2cc")] ShowerSolid,
        /// <summary>Shuttle Van - shuttle-van - f5b6</summary>
        [FAIcon("shuttle-van", "Shuttle Van", FAStyle.Solid, "\uf5b6")] ShuttleVanSolid,
        /// <summary>Sign - sign - f4d9</summary>
        [FAIcon("sign", "Sign", FAStyle.Solid, "\uf4d9")] SignSolid,
        /// <summary>Alternate Sign In - sign-in-alt - f2f6</summary>
        [FAIcon("sign-in-alt", "Alternate Sign In", FAStyle.Solid, "\uf2f6")] SignInAltSolid,
        /// <summary>Sign Language - sign-language - f2a7</summary>
        [FAIcon("sign-language", "Sign Language", FAStyle.Solid, "\uf2a7")] SignLanguageSolid,
        /// <summary>Alternate Sign Out - sign-out-alt - f2f5</summary>
        [FAIcon("sign-out-alt", "Alternate Sign Out", FAStyle.Solid, "\uf2f5")] SignOutAltSolid,
        /// <summary>signal - signal - f012</summary>
        [FAIcon("signal", "signal", FAStyle.Solid, "\uf012")] SignalSolid,
        /// <summary>Signature - signature - f5b7</summary>
        [FAIcon("signature", "Signature", FAStyle.Solid, "\uf5b7")] SignatureSolid,
        /// <summary>SimplyBuilt - simplybuilt - f215</summary>
        [FAIcon("simplybuilt", "SimplyBuilt", FAStyle.Brands, "\uf215")] SimplybuiltBrands,
        /// <summary>SISTRIX - sistrix - f3ee</summary>
        [FAIcon("sistrix", "SISTRIX", FAStyle.Brands, "\uf3ee")] SistrixBrands,
        /// <summary>Sitemap - sitemap - f0e8</summary>
        [FAIcon("sitemap", "Sitemap", FAStyle.Solid, "\uf0e8")] SitemapSolid,
        /// <summary>Sith - sith - f512</summary>
        [FAIcon("sith", "Sith", FAStyle.Brands, "\uf512")] SithBrands,
        /// <summary>Skull - skull - f54c</summary>
        [FAIcon("skull", "Skull", FAStyle.Solid, "\uf54c")] SkullSolid,
        /// <summary>skyatlas - skyatlas - f216</summary>
        [FAIcon("skyatlas", "skyatlas", FAStyle.Brands, "\uf216")] SkyatlasBrands,
        /// <summary>Skype - skype - f17e</summary>
        [FAIcon("skype", "Skype", FAStyle.Brands, "\uf17e")] SkypeBrands,
        /// <summary>Slack Logo - slack - f198</summary>
        [FAIcon("slack", "Slack Logo", FAStyle.Brands, "\uf198")] SlackBrands,
        /// <summary>Slack Hashtag - slack-hash - f3ef</summary>
        [FAIcon("slack-hash", "Slack Hashtag", FAStyle.Brands, "\uf3ef")] SlackHashBrands,
        /// <summary>Horizontal Sliders - sliders-h - f1de</summary>
        [FAIcon("sliders-h", "Horizontal Sliders", FAStyle.Solid, "\uf1de")] SlidersHSolid,
        /// <summary>Slideshare - slideshare - f1e7</summary>
        [FAIcon("slideshare", "Slideshare", FAStyle.Brands, "\uf1e7")] SlideshareBrands,
        /// <summary>Smiling Face - smile - f118</summary>
        [FAIcon("smile", "Smiling Face", FAStyle.Solid, "\uf118")] SmileSolid,
        /// <summary>Smiling Face - smile - f118</summary>
        [FAIcon("smile", "Smiling Face", FAStyle.Regular, "\uf118")] SmileRegular,
        /// <summary>Beaming Face With Smiling Eyes - smile-beam - f5b8</summary>
        [FAIcon("smile-beam", "Beaming Face With Smiling Eyes", FAStyle.Solid, "\uf5b8")] SmileBeamSolid,
        /// <summary>Beaming Face With Smiling Eyes - smile-beam - f5b8</summary>
        [FAIcon("smile-beam", "Beaming Face With Smiling Eyes", FAStyle.Regular, "\uf5b8")] SmileBeamRegular,
        /// <summary>Winking Face - smile-wink - f4da</summary>
        [FAIcon("smile-wink", "Winking Face", FAStyle.Solid, "\uf4da")] SmileWinkSolid,
        /// <summary>Winking Face - smile-wink - f4da</summary>
        [FAIcon("smile-wink", "Winking Face", FAStyle.Regular, "\uf4da")] SmileWinkRegular,
        /// <summary>Smoking - smoking - f48d</summary>
        [FAIcon("smoking", "Smoking", FAStyle.Solid, "\uf48d")] SmokingSolid,
        /// <summary>Smoking Ban - smoking-ban - f54d</summary>
        [FAIcon("smoking-ban", "Smoking Ban", FAStyle.Solid, "\uf54d")] SmokingBanSolid,
        /// <summary>Snapchat - snapchat - f2ab</summary>
        [FAIcon("snapchat", "Snapchat", FAStyle.Brands, "\uf2ab")] SnapchatBrands,
        /// <summary>Snapchat Ghost - snapchat-ghost - f2ac</summary>
        [FAIcon("snapchat-ghost", "Snapchat Ghost", FAStyle.Brands, "\uf2ac")] SnapchatGhostBrands,
        /// <summary>Snapchat Square - snapchat-square - f2ad</summary>
        [FAIcon("snapchat-square", "Snapchat Square", FAStyle.Brands, "\uf2ad")] SnapchatSquareBrands,
        /// <summary>Snowflake - snowflake - f2dc</summary>
        [FAIcon("snowflake", "Snowflake", FAStyle.Solid, "\uf2dc")] SnowflakeSolid,
        /// <summary>Snowflake - snowflake - f2dc</summary>
        [FAIcon("snowflake", "Snowflake", FAStyle.Regular, "\uf2dc")] SnowflakeRegular,
        /// <summary>Solar Panel - solar-panel - f5ba</summary>
        [FAIcon("solar-panel", "Solar Panel", FAStyle.Solid, "\uf5ba")] SolarPanelSolid,
        /// <summary>Sort - sort - f0dc</summary>
        [FAIcon("sort", "Sort", FAStyle.Solid, "\uf0dc")] SortSolid,
        /// <summary>Sort Alpha Down - sort-alpha-down - f15d</summary>
        [FAIcon("sort-alpha-down", "Sort Alpha Down", FAStyle.Solid, "\uf15d")] SortAlphaDownSolid,
        /// <summary>Sort Alpha Up - sort-alpha-up - f15e</summary>
        [FAIcon("sort-alpha-up", "Sort Alpha Up", FAStyle.Solid, "\uf15e")] SortAlphaUpSolid,
        /// <summary>Sort Amount Down - sort-amount-down - f160</summary>
        [FAIcon("sort-amount-down", "Sort Amount Down", FAStyle.Solid, "\uf160")] SortAmountDownSolid,
        /// <summary>Sort Amount Up - sort-amount-up - f161</summary>
        [FAIcon("sort-amount-up", "Sort Amount Up", FAStyle.Solid, "\uf161")] SortAmountUpSolid,
        /// <summary>Sort Down (Descending) - sort-down - f0dd</summary>
        [FAIcon("sort-down", "Sort Down (Descending)", FAStyle.Solid, "\uf0dd")] SortDownSolid,
        /// <summary>Sort Numeric Down - sort-numeric-down - f162</summary>
        [FAIcon("sort-numeric-down", "Sort Numeric Down", FAStyle.Solid, "\uf162")] SortNumericDownSolid,
        /// <summary>Sort Numeric Up - sort-numeric-up - f163</summary>
        [FAIcon("sort-numeric-up", "Sort Numeric Up", FAStyle.Solid, "\uf163")] SortNumericUpSolid,
        /// <summary>Sort Up (Ascending) - sort-up - f0de</summary>
        [FAIcon("sort-up", "Sort Up (Ascending)", FAStyle.Solid, "\uf0de")] SortUpSolid,
        /// <summary>SoundCloud - soundcloud - f1be</summary>
        [FAIcon("soundcloud", "SoundCloud", FAStyle.Brands, "\uf1be")] SoundcloudBrands,
        /// <summary>Spa - spa - f5bb</summary>
        [FAIcon("spa", "Spa", FAStyle.Solid, "\uf5bb")] SpaSolid,
        /// <summary>Space Shuttle - space-shuttle - f197</summary>
        [FAIcon("space-shuttle", "Space Shuttle", FAStyle.Solid, "\uf197")] SpaceShuttleSolid,
        /// <summary>Speakap - speakap - f3f3</summary>
        [FAIcon("speakap", "Speakap", FAStyle.Brands, "\uf3f3")] SpeakapBrands,
        /// <summary>Spinner - spinner - f110</summary>
        [FAIcon("spinner", "Spinner", FAStyle.Solid, "\uf110")] SpinnerSolid,
        /// <summary>Splotch - splotch - f5bc</summary>
        [FAIcon("splotch", "Splotch", FAStyle.Solid, "\uf5bc")] SplotchSolid,
        /// <summary>Spotify - spotify - f1bc</summary>
        [FAIcon("spotify", "Spotify", FAStyle.Brands, "\uf1bc")] SpotifyBrands,
        /// <summary>Spray Can - spray-can - f5bd</summary>
        [FAIcon("spray-can", "Spray Can", FAStyle.Solid, "\uf5bd")] SprayCanSolid,
        /// <summary>Square - square - f0c8</summary>
        [FAIcon("square", "Square", FAStyle.Solid, "\uf0c8")] SquareSolid,
        /// <summary>Square - square - f0c8</summary>
        [FAIcon("square", "Square", FAStyle.Regular, "\uf0c8")] SquareRegular,
        /// <summary>Square Full - square-full - f45c</summary>
        [FAIcon("square-full", "Square Full", FAStyle.Solid, "\uf45c")] SquareFullSolid,
        /// <summary>Squarespace - squarespace - f5be</summary>
        [FAIcon("squarespace", "Squarespace", FAStyle.Brands, "\uf5be")] SquarespaceBrands,
        /// <summary>Stack Exchange - stack-exchange - f18d</summary>
        [FAIcon("stack-exchange", "Stack Exchange", FAStyle.Brands, "\uf18d")] StackExchangeBrands,
        /// <summary>Stack Overflow - stack-overflow - f16c</summary>
        [FAIcon("stack-overflow", "Stack Overflow", FAStyle.Brands, "\uf16c")] StackOverflowBrands,
        /// <summary>Stamp - stamp - f5bf</summary>
        [FAIcon("stamp", "Stamp", FAStyle.Solid, "\uf5bf")] StampSolid,
        /// <summary>Star - star - f005</summary>
        [FAIcon("star", "Star", FAStyle.Solid, "\uf005")] StarSolid,
        /// <summary>Star - star - f005</summary>
        [FAIcon("star", "Star", FAStyle.Regular, "\uf005")] StarRegular,
        /// <summary>star-half - star-half - f089</summary>
        [FAIcon("star-half", "star-half", FAStyle.Solid, "\uf089")] StarHalfSolid,
        /// <summary>star-half - star-half - f089</summary>
        [FAIcon("star-half", "star-half", FAStyle.Regular, "\uf089")] StarHalfRegular,
        /// <summary>Alternate Star Half - star-half-alt - f5c0</summary>
        [FAIcon("star-half-alt", "Alternate Star Half", FAStyle.Solid, "\uf5c0")] StarHalfAltSolid,
        /// <summary>Star Of-life - star-of-life - f621</summary>
        [FAIcon("star-of-life", "Star Of-life", FAStyle.Solid, "\uf621")] StarOfLifeSolid,
        /// <summary>StayLinked - staylinked - f3f5</summary>
        [FAIcon("staylinked", "StayLinked", FAStyle.Brands, "\uf3f5")] StaylinkedBrands,
        /// <summary>Steam - steam - f1b6</summary>
        [FAIcon("steam", "Steam", FAStyle.Brands, "\uf1b6")] SteamBrands,
        /// <summary>Steam Square - steam-square - f1b7</summary>
        [FAIcon("steam-square", "Steam Square", FAStyle.Brands, "\uf1b7")] SteamSquareBrands,
        /// <summary>Steam Symbol - steam-symbol - f3f6</summary>
        [FAIcon("steam-symbol", "Steam Symbol", FAStyle.Brands, "\uf3f6")] SteamSymbolBrands,
        /// <summary>step-backward - step-backward - f048</summary>
        [FAIcon("step-backward", "step-backward", FAStyle.Solid, "\uf048")] StepBackwardSolid,
        /// <summary>step-forward - step-forward - f051</summary>
        [FAIcon("step-forward", "step-forward", FAStyle.Solid, "\uf051")] StepForwardSolid,
        /// <summary>Stethoscope - stethoscope - f0f1</summary>
        [FAIcon("stethoscope", "Stethoscope", FAStyle.Solid, "\uf0f1")] StethoscopeSolid,
        /// <summary>Sticker Mule - sticker-mule - f3f7</summary>
        [FAIcon("sticker-mule", "Sticker Mule", FAStyle.Brands, "\uf3f7")] StickerMuleBrands,
        /// <summary>Sticky Note - sticky-note - f249</summary>
        [FAIcon("sticky-note", "Sticky Note", FAStyle.Solid, "\uf249")] StickyNoteSolid,
        /// <summary>Sticky Note - sticky-note - f249</summary>
        [FAIcon("sticky-note", "Sticky Note", FAStyle.Regular, "\uf249")] StickyNoteRegular,
        /// <summary>stop - stop - f04d</summary>
        [FAIcon("stop", "stop", FAStyle.Solid, "\uf04d")] StopSolid,
        /// <summary>Stop Circle - stop-circle - f28d</summary>
        [FAIcon("stop-circle", "Stop Circle", FAStyle.Solid, "\uf28d")] StopCircleSolid,
        /// <summary>Stop Circle - stop-circle - f28d</summary>
        [FAIcon("stop-circle", "Stop Circle", FAStyle.Regular, "\uf28d")] StopCircleRegular,
        /// <summary>Stopwatch - stopwatch - f2f2</summary>
        [FAIcon("stopwatch", "Stopwatch", FAStyle.Solid, "\uf2f2")] StopwatchSolid,
        /// <summary>Store - store - f54e</summary>
        [FAIcon("store", "Store", FAStyle.Solid, "\uf54e")] StoreSolid,
        /// <summary>Alternate Store - store-alt - f54f</summary>
        [FAIcon("store-alt", "Alternate Store", FAStyle.Solid, "\uf54f")] StoreAltSolid,
        /// <summary>Strava - strava - f428</summary>
        [FAIcon("strava", "Strava", FAStyle.Brands, "\uf428")] StravaBrands,
        /// <summary>Stream - stream - f550</summary>
        [FAIcon("stream", "Stream", FAStyle.Solid, "\uf550")] StreamSolid,
        /// <summary>Street View - street-view - f21d</summary>
        [FAIcon("street-view", "Street View", FAStyle.Solid, "\uf21d")] StreetViewSolid,
        /// <summary>Strikethrough - strikethrough - f0cc</summary>
        [FAIcon("strikethrough", "Strikethrough", FAStyle.Solid, "\uf0cc")] StrikethroughSolid,
        /// <summary>Stripe - stripe - f429</summary>
        [FAIcon("stripe", "Stripe", FAStyle.Brands, "\uf429")] StripeBrands,
        /// <summary>Stripe S - stripe-s - f42a</summary>
        [FAIcon("stripe-s", "Stripe S", FAStyle.Brands, "\uf42a")] StripeSBrands,
        /// <summary>Stroopwafel - stroopwafel - f551</summary>
        [FAIcon("stroopwafel", "Stroopwafel", FAStyle.Solid, "\uf551")] StroopwafelSolid,
        /// <summary>Studio Vinari - studiovinari - f3f8</summary>
        [FAIcon("studiovinari", "Studio Vinari", FAStyle.Brands, "\uf3f8")] StudiovinariBrands,
        /// <summary>StumbleUpon Logo - stumbleupon - f1a4</summary>
        [FAIcon("stumbleupon", "StumbleUpon Logo", FAStyle.Brands, "\uf1a4")] StumbleuponBrands,
        /// <summary>StumbleUpon Circle - stumbleupon-circle - f1a3</summary>
        [FAIcon("stumbleupon-circle", "StumbleUpon Circle", FAStyle.Brands, "\uf1a3")] StumbleuponCircleBrands,
        /// <summary>subscript - subscript - f12c</summary>
        [FAIcon("subscript", "subscript", FAStyle.Solid, "\uf12c")] SubscriptSolid,
        /// <summary>Subway - subway - f239</summary>
        [FAIcon("subway", "Subway", FAStyle.Solid, "\uf239")] SubwaySolid,
        /// <summary>Suitcase - suitcase - f0f2</summary>
        [FAIcon("suitcase", "Suitcase", FAStyle.Solid, "\uf0f2")] SuitcaseSolid,
        /// <summary>Suitcase Rolling - suitcase-rolling - f5c1</summary>
        [FAIcon("suitcase-rolling", "Suitcase Rolling", FAStyle.Solid, "\uf5c1")] SuitcaseRollingSolid,
        /// <summary>Sun - sun - f185</summary>
        [FAIcon("sun", "Sun", FAStyle.Solid, "\uf185")] SunSolid,
        /// <summary>Sun - sun - f185</summary>
        [FAIcon("sun", "Sun", FAStyle.Regular, "\uf185")] SunRegular,
        /// <summary>Superpowers - superpowers - f2dd</summary>
        [FAIcon("superpowers", "Superpowers", FAStyle.Brands, "\uf2dd")] SuperpowersBrands,
        /// <summary>superscript - superscript - f12b</summary>
        [FAIcon("superscript", "superscript", FAStyle.Solid, "\uf12b")] SuperscriptSolid,
        /// <summary>Supple - supple - f3f9</summary>
        [FAIcon("supple", "Supple", FAStyle.Brands, "\uf3f9")] SuppleBrands,
        /// <summary>Hushed Face - surprise - f5c2</summary>
        [FAIcon("surprise", "Hushed Face", FAStyle.Solid, "\uf5c2")] SurpriseSolid,
        /// <summary>Hushed Face - surprise - f5c2</summary>
        [FAIcon("surprise", "Hushed Face", FAStyle.Regular, "\uf5c2")] SurpriseRegular,
        /// <summary>Swatchbook - swatchbook - f5c3</summary>
        [FAIcon("swatchbook", "Swatchbook", FAStyle.Solid, "\uf5c3")] SwatchbookSolid,
        /// <summary>Swimmer - swimmer - f5c4</summary>
        [FAIcon("swimmer", "Swimmer", FAStyle.Solid, "\uf5c4")] SwimmerSolid,
        /// <summary>Swimming Pool - swimming-pool - f5c5</summary>
        [FAIcon("swimming-pool", "Swimming Pool", FAStyle.Solid, "\uf5c5")] SwimmingPoolSolid,
        /// <summary>Sync - sync - f021</summary>
        [FAIcon("sync", "Sync", FAStyle.Solid, "\uf021")] SyncSolid,
        /// <summary>Alternate Sync - sync-alt - f2f1</summary>
        [FAIcon("sync-alt", "Alternate Sync", FAStyle.Solid, "\uf2f1")] SyncAltSolid,
        /// <summary>Syringe - syringe - f48e</summary>
        [FAIcon("syringe", "Syringe", FAStyle.Solid, "\uf48e")] SyringeSolid,
        /// <summary>table - table - f0ce</summary>
        [FAIcon("table", "table", FAStyle.Solid, "\uf0ce")] TableSolid,
        /// <summary>Table Tennis - table-tennis - f45d</summary>
        [FAIcon("table-tennis", "Table Tennis", FAStyle.Solid, "\uf45d")] TableTennisSolid,
        /// <summary>tablet - tablet - f10a</summary>
        [FAIcon("tablet", "tablet", FAStyle.Solid, "\uf10a")] TabletSolid,
        /// <summary>Alternate Tablet - tablet-alt - f3fa</summary>
        [FAIcon("tablet-alt", "Alternate Tablet", FAStyle.Solid, "\uf3fa")] TabletAltSolid,
        /// <summary>Tablets - tablets - f490</summary>
        [FAIcon("tablets", "Tablets", FAStyle.Solid, "\uf490")] TabletsSolid,
        /// <summary>Alternate Tachometer - tachometer-alt - f3fd</summary>
        [FAIcon("tachometer-alt", "Alternate Tachometer", FAStyle.Solid, "\uf3fd")] TachometerAltSolid,
        /// <summary>tag - tag - f02b</summary>
        [FAIcon("tag", "tag", FAStyle.Solid, "\uf02b")] TagSolid,
        /// <summary>tags - tags - f02c</summary>
        [FAIcon("tags", "tags", FAStyle.Solid, "\uf02c")] TagsSolid,
        /// <summary>Tape - tape - f4db</summary>
        [FAIcon("tape", "Tape", FAStyle.Solid, "\uf4db")] TapeSolid,
        /// <summary>Tasks - tasks - f0ae</summary>
        [FAIcon("tasks", "Tasks", FAStyle.Solid, "\uf0ae")] TasksSolid,
        /// <summary>Taxi - taxi - f1ba</summary>
        [FAIcon("taxi", "Taxi", FAStyle.Solid, "\uf1ba")] TaxiSolid,
        /// <summary>TeamSpeak - teamspeak - f4f9</summary>
        [FAIcon("teamspeak", "TeamSpeak", FAStyle.Brands, "\uf4f9")] TeamspeakBrands,
        /// <summary>Teeth - teeth - f62e</summary>
        [FAIcon("teeth", "Teeth", FAStyle.Solid, "\uf62e")] TeethSolid,
        /// <summary>Teeth Open - teeth-open - f62f</summary>
        [FAIcon("teeth-open", "Teeth Open", FAStyle.Solid, "\uf62f")] TeethOpenSolid,
        /// <summary>Telegram - telegram - f2c6</summary>
        [FAIcon("telegram", "Telegram", FAStyle.Brands, "\uf2c6")] TelegramBrands,
        /// <summary>Telegram Plane - telegram-plane - f3fe</summary>
        [FAIcon("telegram-plane", "Telegram Plane", FAStyle.Brands, "\uf3fe")] TelegramPlaneBrands,
        /// <summary>Tencent Weibo - tencent-weibo - f1d5</summary>
        [FAIcon("tencent-weibo", "Tencent Weibo", FAStyle.Brands, "\uf1d5")] TencentWeiboBrands,
        /// <summary>Terminal - terminal - f120</summary>
        [FAIcon("terminal", "Terminal", FAStyle.Solid, "\uf120")] TerminalSolid,
        /// <summary>text-height - text-height - f034</summary>
        [FAIcon("text-height", "text-height", FAStyle.Solid, "\uf034")] TextHeightSolid,
        /// <summary>text-width - text-width - f035</summary>
        [FAIcon("text-width", "text-width", FAStyle.Solid, "\uf035")] TextWidthSolid,
        /// <summary>th - th - f00a</summary>
        [FAIcon("th", "th", FAStyle.Solid, "\uf00a")] ThSolid,
        /// <summary>th-large - th-large - f009</summary>
        [FAIcon("th-large", "th-large", FAStyle.Solid, "\uf009")] ThLargeSolid,
        /// <summary>th-list - th-list - f00b</summary>
        [FAIcon("th-list", "th-list", FAStyle.Solid, "\uf00b")] ThListSolid,
        /// <summary>Theater Masks - theater-masks - f630</summary>
        [FAIcon("theater-masks", "Theater Masks", FAStyle.Solid, "\uf630")] TheaterMasksSolid,
        /// <summary>Themeco - themeco - f5c6</summary>
        [FAIcon("themeco", "Themeco", FAStyle.Brands, "\uf5c6")] ThemecoBrands,
        /// <summary>ThemeIsle - themeisle - f2b2</summary>
        [FAIcon("themeisle", "ThemeIsle", FAStyle.Brands, "\uf2b2")] ThemeisleBrands,
        /// <summary>Thermometer - thermometer - f491</summary>
        [FAIcon("thermometer", "Thermometer", FAStyle.Solid, "\uf491")] ThermometerSolid,
        /// <summary>Thermometer Empty - thermometer-empty - f2cb</summary>
        [FAIcon("thermometer-empty", "Thermometer Empty", FAStyle.Solid, "\uf2cb")] ThermometerEmptySolid,
        /// <summary>Thermometer Full - thermometer-full - f2c7</summary>
        [FAIcon("thermometer-full", "Thermometer Full", FAStyle.Solid, "\uf2c7")] ThermometerFullSolid,
        /// <summary>Thermometer 1/2 Full - thermometer-half - f2c9</summary>
        [FAIcon("thermometer-half", "Thermometer 1/2 Full", FAStyle.Solid, "\uf2c9")] ThermometerHalfSolid,
        /// <summary>Thermometer 1/4 Full - thermometer-quarter - f2ca</summary>
        [FAIcon("thermometer-quarter", "Thermometer 1/4 Full", FAStyle.Solid, "\uf2ca")] ThermometerQuarterSolid,
        /// <summary>Thermometer 3/4 Full - thermometer-three-quarters - f2c8</summary>
        [FAIcon("thermometer-three-quarters", "Thermometer 3/4 Full", FAStyle.Solid, "\uf2c8")] ThermometerThreeQuartersSolid,
        /// <summary>thumbs-down - thumbs-down - f165</summary>
        [FAIcon("thumbs-down", "thumbs-down", FAStyle.Solid, "\uf165")] ThumbsDownSolid,
        /// <summary>thumbs-down - thumbs-down - f165</summary>
        [FAIcon("thumbs-down", "thumbs-down", FAStyle.Regular, "\uf165")] ThumbsDownRegular,
        /// <summary>thumbs-up - thumbs-up - f164</summary>
        [FAIcon("thumbs-up", "thumbs-up", FAStyle.Solid, "\uf164")] ThumbsUpSolid,
        /// <summary>thumbs-up - thumbs-up - f164</summary>
        [FAIcon("thumbs-up", "thumbs-up", FAStyle.Regular, "\uf164")] ThumbsUpRegular,
        /// <summary>Thumbtack - thumbtack - f08d</summary>
        [FAIcon("thumbtack", "Thumbtack", FAStyle.Solid, "\uf08d")] ThumbtackSolid,
        /// <summary>Alternate Ticket - ticket-alt - f3ff</summary>
        [FAIcon("ticket-alt", "Alternate Ticket", FAStyle.Solid, "\uf3ff")] TicketAltSolid,
        /// <summary>Times - times - f00d</summary>
        [FAIcon("times", "Times", FAStyle.Solid, "\uf00d")] TimesSolid,
        /// <summary>Times Circle - times-circle - f057</summary>
        [FAIcon("times-circle", "Times Circle", FAStyle.Solid, "\uf057")] TimesCircleSolid,
        /// <summary>Times Circle - times-circle - f057</summary>
        [FAIcon("times-circle", "Times Circle", FAStyle.Regular, "\uf057")] TimesCircleRegular,
        /// <summary>tint - tint - f043</summary>
        [FAIcon("tint", "tint", FAStyle.Solid, "\uf043")] TintSolid,
        /// <summary>Tint Slash - tint-slash - f5c7</summary>
        [FAIcon("tint-slash", "Tint Slash", FAStyle.Solid, "\uf5c7")] TintSlashSolid,
        /// <summary>Tired Face - tired - f5c8</summary>
        [FAIcon("tired", "Tired Face", FAStyle.Solid, "\uf5c8")] TiredSolid,
        /// <summary>Tired Face - tired - f5c8</summary>
        [FAIcon("tired", "Tired Face", FAStyle.Regular, "\uf5c8")] TiredRegular,
        /// <summary>Toggle Off - toggle-off - f204</summary>
        [FAIcon("toggle-off", "Toggle Off", FAStyle.Solid, "\uf204")] ToggleOffSolid,
        /// <summary>Toggle On - toggle-on - f205</summary>
        [FAIcon("toggle-on", "Toggle On", FAStyle.Solid, "\uf205")] ToggleOnSolid,
        /// <summary>Toolbox - toolbox - f552</summary>
        [FAIcon("toolbox", "Toolbox", FAStyle.Solid, "\uf552")] ToolboxSolid,
        /// <summary>Tooth - tooth - f5c9</summary>
        [FAIcon("tooth", "Tooth", FAStyle.Solid, "\uf5c9")] ToothSolid,
        /// <summary>Trade Federation - trade-federation - f513</summary>
        [FAIcon("trade-federation", "Trade Federation", FAStyle.Brands, "\uf513")] TradeFederationBrands,
        /// <summary>Trademark - trademark - f25c</summary>
        [FAIcon("trademark", "Trademark", FAStyle.Solid, "\uf25c")] TrademarkSolid,
        /// <summary>Traffic Light - traffic-light - f637</summary>
        [FAIcon("traffic-light", "Traffic Light", FAStyle.Solid, "\uf637")] TrafficLightSolid,
        /// <summary>Train - train - f238</summary>
        [FAIcon("train", "Train", FAStyle.Solid, "\uf238")] TrainSolid,
        /// <summary>Transgender - transgender - f224</summary>
        [FAIcon("transgender", "Transgender", FAStyle.Solid, "\uf224")] TransgenderSolid,
        /// <summary>Alternate Transgender - transgender-alt - f225</summary>
        [FAIcon("transgender-alt", "Alternate Transgender", FAStyle.Solid, "\uf225")] TransgenderAltSolid,
        /// <summary>Trash - trash - f1f8</summary>
        [FAIcon("trash", "Trash", FAStyle.Solid, "\uf1f8")] TrashSolid,
        /// <summary>Alternate Trash - trash-alt - f2ed</summary>
        [FAIcon("trash-alt", "Alternate Trash", FAStyle.Solid, "\uf2ed")] TrashAltSolid,
        /// <summary>Alternate Trash - trash-alt - f2ed</summary>
        [FAIcon("trash-alt", "Alternate Trash", FAStyle.Regular, "\uf2ed")] TrashAltRegular,
        /// <summary>Tree - tree - f1bb</summary>
        [FAIcon("tree", "Tree", FAStyle.Solid, "\uf1bb")] TreeSolid,
        /// <summary>Trello - trello - f181</summary>
        [FAIcon("trello", "Trello", FAStyle.Brands, "\uf181")] TrelloBrands,
        /// <summary>TripAdvisor - tripadvisor - f262</summary>
        [FAIcon("tripadvisor", "TripAdvisor", FAStyle.Brands, "\uf262")] TripadvisorBrands,
        /// <summary>trophy - trophy - f091</summary>
        [FAIcon("trophy", "trophy", FAStyle.Solid, "\uf091")] TrophySolid,
        /// <summary>truck - truck - f0d1</summary>
        [FAIcon("truck", "truck", FAStyle.Solid, "\uf0d1")] TruckSolid,
        /// <summary>Truck Loading - truck-loading - f4de</summary>
        [FAIcon("truck-loading", "Truck Loading", FAStyle.Solid, "\uf4de")] TruckLoadingSolid,
        /// <summary>Truck Monster - truck-monster - f63b</summary>
        [FAIcon("truck-monster", "Truck Monster", FAStyle.Solid, "\uf63b")] TruckMonsterSolid,
        /// <summary>Truck Moving - truck-moving - f4df</summary>
        [FAIcon("truck-moving", "Truck Moving", FAStyle.Solid, "\uf4df")] TruckMovingSolid,
        /// <summary>Truck Side - truck-pickup - f63c</summary>
        [FAIcon("truck-pickup", "Truck Side", FAStyle.Solid, "\uf63c")] TruckPickupSolid,
        /// <summary>T-Shirt - tshirt - f553</summary>
        [FAIcon("tshirt", "T-Shirt", FAStyle.Solid, "\uf553")] TshirtSolid,
        /// <summary>TTY - tty - f1e4</summary>
        [FAIcon("tty", "TTY", FAStyle.Solid, "\uf1e4")] TtySolid,
        /// <summary>Tumblr - tumblr - f173</summary>
        [FAIcon("tumblr", "Tumblr", FAStyle.Brands, "\uf173")] TumblrBrands,
        /// <summary>Tumblr Square - tumblr-square - f174</summary>
        [FAIcon("tumblr-square", "Tumblr Square", FAStyle.Brands, "\uf174")] TumblrSquareBrands,
        /// <summary>Television - tv - f26c</summary>
        [FAIcon("tv", "Television", FAStyle.Solid, "\uf26c")] TvSolid,
        /// <summary>Twitch - twitch - f1e8</summary>
        [FAIcon("twitch", "Twitch", FAStyle.Brands, "\uf1e8")] TwitchBrands,
        /// <summary>Twitter - twitter - f099</summary>
        [FAIcon("twitter", "Twitter", FAStyle.Brands, "\uf099")] TwitterBrands,
        /// <summary>Twitter Square - twitter-square - f081</summary>
        [FAIcon("twitter-square", "Twitter Square", FAStyle.Brands, "\uf081")] TwitterSquareBrands,
        /// <summary>Typo3 - typo3 - f42b</summary>
        [FAIcon("typo3", "Typo3", FAStyle.Brands, "\uf42b")] Typo3Brands,
        /// <summary>Uber - uber - f402</summary>
        [FAIcon("uber", "Uber", FAStyle.Brands, "\uf402")] UberBrands,
        /// <summary>UIkit - uikit - f403</summary>
        [FAIcon("uikit", "UIkit", FAStyle.Brands, "\uf403")] UikitBrands,
        /// <summary>Umbrella - umbrella - f0e9</summary>
        [FAIcon("umbrella", "Umbrella", FAStyle.Solid, "\uf0e9")] UmbrellaSolid,
        /// <summary>Umbrella Beach - umbrella-beach - f5ca</summary>
        [FAIcon("umbrella-beach", "Umbrella Beach", FAStyle.Solid, "\uf5ca")] UmbrellaBeachSolid,
        /// <summary>Underline - underline - f0cd</summary>
        [FAIcon("underline", "Underline", FAStyle.Solid, "\uf0cd")] UnderlineSolid,
        /// <summary>Undo - undo - f0e2</summary>
        [FAIcon("undo", "Undo", FAStyle.Solid, "\uf0e2")] UndoSolid,
        /// <summary>Alternate Undo - undo-alt - f2ea</summary>
        [FAIcon("undo-alt", "Alternate Undo", FAStyle.Solid, "\uf2ea")] UndoAltSolid,
        /// <summary>Uniregistry - uniregistry - f404</summary>
        [FAIcon("uniregistry", "Uniregistry", FAStyle.Brands, "\uf404")] UniregistryBrands,
        /// <summary>Universal Access - universal-access - f29a</summary>
        [FAIcon("universal-access", "Universal Access", FAStyle.Solid, "\uf29a")] UniversalAccessSolid,
        /// <summary>University - university - f19c</summary>
        [FAIcon("university", "University", FAStyle.Solid, "\uf19c")] UniversitySolid,
        /// <summary>unlink - unlink - f127</summary>
        [FAIcon("unlink", "unlink", FAStyle.Solid, "\uf127")] UnlinkSolid,
        /// <summary>unlock - unlock - f09c</summary>
        [FAIcon("unlock", "unlock", FAStyle.Solid, "\uf09c")] UnlockSolid,
        /// <summary>Alternate Unlock - unlock-alt - f13e</summary>
        [FAIcon("unlock-alt", "Alternate Unlock", FAStyle.Solid, "\uf13e")] UnlockAltSolid,
        /// <summary>Untappd - untappd - f405</summary>
        [FAIcon("untappd", "Untappd", FAStyle.Brands, "\uf405")] UntappdBrands,
        /// <summary>Upload - upload - f093</summary>
        [FAIcon("upload", "Upload", FAStyle.Solid, "\uf093")] UploadSolid,
        /// <summary>USB - usb - f287</summary>
        [FAIcon("usb", "USB", FAStyle.Brands, "\uf287")] UsbBrands,
        /// <summary>User - user - f007</summary>
        [FAIcon("user", "User", FAStyle.Solid, "\uf007")] UserSolid,
        /// <summary>User - user - f007</summary>
        [FAIcon("user", "User", FAStyle.Regular, "\uf007")] UserRegular,
        /// <summary>Alternate User - user-alt - f406</summary>
        [FAIcon("user-alt", "Alternate User", FAStyle.Solid, "\uf406")] UserAltSolid,
        /// <summary>Alternate User Slash - user-alt-slash - f4fa</summary>
        [FAIcon("user-alt-slash", "Alternate User Slash", FAStyle.Solid, "\uf4fa")] UserAltSlashSolid,
        /// <summary>User Astronaut - user-astronaut - f4fb</summary>
        [FAIcon("user-astronaut", "User Astronaut", FAStyle.Solid, "\uf4fb")] UserAstronautSolid,
        /// <summary>User Check - user-check - f4fc</summary>
        [FAIcon("user-check", "User Check", FAStyle.Solid, "\uf4fc")] UserCheckSolid,
        /// <summary>User Circle - user-circle - f2bd</summary>
        [FAIcon("user-circle", "User Circle", FAStyle.Solid, "\uf2bd")] UserCircleSolid,
        /// <summary>User Circle - user-circle - f2bd</summary>
        [FAIcon("user-circle", "User Circle", FAStyle.Regular, "\uf2bd")] UserCircleRegular,
        /// <summary>User Clock - user-clock - f4fd</summary>
        [FAIcon("user-clock", "User Clock", FAStyle.Solid, "\uf4fd")] UserClockSolid,
        /// <summary>User Cog - user-cog - f4fe</summary>
        [FAIcon("user-cog", "User Cog", FAStyle.Solid, "\uf4fe")] UserCogSolid,
        /// <summary>User Edit - user-edit - f4ff</summary>
        [FAIcon("user-edit", "User Edit", FAStyle.Solid, "\uf4ff")] UserEditSolid,
        /// <summary>User Friends - user-friends - f500</summary>
        [FAIcon("user-friends", "User Friends", FAStyle.Solid, "\uf500")] UserFriendsSolid,
        /// <summary>User Graduate - user-graduate - f501</summary>
        [FAIcon("user-graduate", "User Graduate", FAStyle.Solid, "\uf501")] UserGraduateSolid,
        /// <summary>User Lock - user-lock - f502</summary>
        [FAIcon("user-lock", "User Lock", FAStyle.Solid, "\uf502")] UserLockSolid,
        /// <summary>user-md - user-md - f0f0</summary>
        [FAIcon("user-md", "user-md", FAStyle.Solid, "\uf0f0")] UserMdSolid,
        /// <summary>User Minus - user-minus - f503</summary>
        [FAIcon("user-minus", "User Minus", FAStyle.Solid, "\uf503")] UserMinusSolid,
        /// <summary>User Ninja - user-ninja - f504</summary>
        [FAIcon("user-ninja", "User Ninja", FAStyle.Solid, "\uf504")] UserNinjaSolid,
        /// <summary>Add User - user-plus - f234</summary>
        [FAIcon("user-plus", "Add User", FAStyle.Solid, "\uf234")] UserPlusSolid,
        /// <summary>User Secret - user-secret - f21b</summary>
        [FAIcon("user-secret", "User Secret", FAStyle.Solid, "\uf21b")] UserSecretSolid,
        /// <summary>User Shield - user-shield - f505</summary>
        [FAIcon("user-shield", "User Shield", FAStyle.Solid, "\uf505")] UserShieldSolid,
        /// <summary>User Slash - user-slash - f506</summary>
        [FAIcon("user-slash", "User Slash", FAStyle.Solid, "\uf506")] UserSlashSolid,
        /// <summary>User Tag - user-tag - f507</summary>
        [FAIcon("user-tag", "User Tag", FAStyle.Solid, "\uf507")] UserTagSolid,
        /// <summary>User Tie - user-tie - f508</summary>
        [FAIcon("user-tie", "User Tie", FAStyle.Solid, "\uf508")] UserTieSolid,
        /// <summary>Remove User - user-times - f235</summary>
        [FAIcon("user-times", "Remove User", FAStyle.Solid, "\uf235")] UserTimesSolid,
        /// <summary>Users - users - f0c0</summary>
        [FAIcon("users", "Users", FAStyle.Solid, "\uf0c0")] UsersSolid,
        /// <summary>Users Cog - users-cog - f509</summary>
        [FAIcon("users-cog", "Users Cog", FAStyle.Solid, "\uf509")] UsersCogSolid,
        /// <summary>us-Sunnah Foundation - ussunnah - f407</summary>
        [FAIcon("ussunnah", "us-Sunnah Foundation", FAStyle.Brands, "\uf407")] UssunnahBrands,
        /// <summary>Utensil Spoon - utensil-spoon - f2e5</summary>
        [FAIcon("utensil-spoon", "Utensil Spoon", FAStyle.Solid, "\uf2e5")] UtensilSpoonSolid,
        /// <summary>Utensils - utensils - f2e7</summary>
        [FAIcon("utensils", "Utensils", FAStyle.Solid, "\uf2e7")] UtensilsSolid,
        /// <summary>Vaadin - vaadin - f408</summary>
        [FAIcon("vaadin", "Vaadin", FAStyle.Brands, "\uf408")] VaadinBrands,
        /// <summary>Vector Square - vector-square - f5cb</summary>
        [FAIcon("vector-square", "Vector Square", FAStyle.Solid, "\uf5cb")] VectorSquareSolid,
        /// <summary>Venus - venus - f221</summary>
        [FAIcon("venus", "Venus", FAStyle.Solid, "\uf221")] VenusSolid,
        /// <summary>Venus Double - venus-double - f226</summary>
        [FAIcon("venus-double", "Venus Double", FAStyle.Solid, "\uf226")] VenusDoubleSolid,
        /// <summary>Venus Mars - venus-mars - f228</summary>
        [FAIcon("venus-mars", "Venus Mars", FAStyle.Solid, "\uf228")] VenusMarsSolid,
        /// <summary>Viacoin - viacoin - f237</summary>
        [FAIcon("viacoin", "Viacoin", FAStyle.Brands, "\uf237")] ViacoinBrands,
        /// <summary>Viadeo - viadeo - f2a9</summary>
        [FAIcon("viadeo", "Viadeo", FAStyle.Brands, "\uf2a9")] ViadeoBrands,
        /// <summary>Viadeo Square - viadeo-square - f2aa</summary>
        [FAIcon("viadeo-square", "Viadeo Square", FAStyle.Brands, "\uf2aa")] ViadeoSquareBrands,
        /// <summary>Vial - vial - f492</summary>
        [FAIcon("vial", "Vial", FAStyle.Solid, "\uf492")] VialSolid,
        /// <summary>Vials - vials - f493</summary>
        [FAIcon("vials", "Vials", FAStyle.Solid, "\uf493")] VialsSolid,
        /// <summary>Viber - viber - f409</summary>
        [FAIcon("viber", "Viber", FAStyle.Brands, "\uf409")] ViberBrands,
        /// <summary>Video - video - f03d</summary>
        [FAIcon("video", "Video", FAStyle.Solid, "\uf03d")] VideoSolid,
        /// <summary>Video Slash - video-slash - f4e2</summary>
        [FAIcon("video-slash", "Video Slash", FAStyle.Solid, "\uf4e2")] VideoSlashSolid,
        /// <summary>Vimeo - vimeo - f40a</summary>
        [FAIcon("vimeo", "Vimeo", FAStyle.Brands, "\uf40a")] VimeoBrands,
        /// <summary>Vimeo Square - vimeo-square - f194</summary>
        [FAIcon("vimeo-square", "Vimeo Square", FAStyle.Brands, "\uf194")] VimeoSquareBrands,
        /// <summary>Vimeo - vimeo-v - f27d</summary>
        [FAIcon("vimeo-v", "Vimeo", FAStyle.Brands, "\uf27d")] VimeoVBrands,
        /// <summary>Vine - vine - f1ca</summary>
        [FAIcon("vine", "Vine", FAStyle.Brands, "\uf1ca")] VineBrands,
        /// <summary>VK - vk - f189</summary>
        [FAIcon("vk", "VK", FAStyle.Brands, "\uf189")] VkBrands,
        /// <summary>VNV - vnv - f40b</summary>
        [FAIcon("vnv", "VNV", FAStyle.Brands, "\uf40b")] VnvBrands,
        /// <summary>Volleyball Ball - volleyball-ball - f45f</summary>
        [FAIcon("volleyball-ball", "Volleyball Ball", FAStyle.Solid, "\uf45f")] VolleyballBallSolid,
        /// <summary>volume-down - volume-down - f027</summary>
        [FAIcon("volume-down", "volume-down", FAStyle.Solid, "\uf027")] VolumeDownSolid,
        /// <summary>volume-off - volume-off - f026</summary>
        [FAIcon("volume-off", "volume-off", FAStyle.Solid, "\uf026")] VolumeOffSolid,
        /// <summary>volume-up - volume-up - f028</summary>
        [FAIcon("volume-up", "volume-up", FAStyle.Solid, "\uf028")] VolumeUpSolid,
        /// <summary>Vue.js - vuejs - f41f</summary>
        [FAIcon("vuejs", "Vue.js", FAStyle.Brands, "\uf41f")] VuejsBrands,
        /// <summary>Walking - walking - f554</summary>
        [FAIcon("walking", "Walking", FAStyle.Solid, "\uf554")] WalkingSolid,
        /// <summary>Wallet - wallet - f555</summary>
        [FAIcon("wallet", "Wallet", FAStyle.Solid, "\uf555")] WalletSolid,
        /// <summary>Warehouse - warehouse - f494</summary>
        [FAIcon("warehouse", "Warehouse", FAStyle.Solid, "\uf494")] WarehouseSolid,
        /// <summary>Weebly - weebly - f5cc</summary>
        [FAIcon("weebly", "Weebly", FAStyle.Brands, "\uf5cc")] WeeblyBrands,
        /// <summary>Weibo - weibo - f18a</summary>
        [FAIcon("weibo", "Weibo", FAStyle.Brands, "\uf18a")] WeiboBrands,
        /// <summary>Weight - weight - f496</summary>
        [FAIcon("weight", "Weight", FAStyle.Solid, "\uf496")] WeightSolid,
        /// <summary>Hanging Weight - weight-hanging - f5cd</summary>
        [FAIcon("weight-hanging", "Hanging Weight", FAStyle.Solid, "\uf5cd")] WeightHangingSolid,
        /// <summary>Weixin (WeChat) - weixin - f1d7</summary>
        [FAIcon("weixin", "Weixin (WeChat)", FAStyle.Brands, "\uf1d7")] WeixinBrands,
        /// <summary>What's App - whatsapp - f232</summary>
        [FAIcon("whatsapp", "What's App", FAStyle.Brands, "\uf232")] WhatsappBrands,
        /// <summary>What's App Square - whatsapp-square - f40c</summary>
        [FAIcon("whatsapp-square", "What's App Square", FAStyle.Brands, "\uf40c")] WhatsappSquareBrands,
        /// <summary>Wheelchair - wheelchair - f193</summary>
        [FAIcon("wheelchair", "Wheelchair", FAStyle.Solid, "\uf193")] WheelchairSolid,
        /// <summary>WHMCS - whmcs - f40d</summary>
        [FAIcon("whmcs", "WHMCS", FAStyle.Brands, "\uf40d")] WhmcsBrands,
        /// <summary>WiFi - wifi - f1eb</summary>
        [FAIcon("wifi", "WiFi", FAStyle.Solid, "\uf1eb")] WifiSolid,
        /// <summary>Wikipedia W - wikipedia-w - f266</summary>
        [FAIcon("wikipedia-w", "Wikipedia W", FAStyle.Brands, "\uf266")] WikipediaWBrands,
        /// <summary>Window Close - window-close - f410</summary>
        [FAIcon("window-close", "Window Close", FAStyle.Solid, "\uf410")] WindowCloseSolid,
        /// <summary>Window Close - window-close - f410</summary>
        [FAIcon("window-close", "Window Close", FAStyle.Regular, "\uf410")] WindowCloseRegular,
        /// <summary>Window Maximize - window-maximize - f2d0</summary>
        [FAIcon("window-maximize", "Window Maximize", FAStyle.Solid, "\uf2d0")] WindowMaximizeSolid,
        /// <summary>Window Maximize - window-maximize - f2d0</summary>
        [FAIcon("window-maximize", "Window Maximize", FAStyle.Regular, "\uf2d0")] WindowMaximizeRegular,
        /// <summary>Window Minimize - window-minimize - f2d1</summary>
        [FAIcon("window-minimize", "Window Minimize", FAStyle.Solid, "\uf2d1")] WindowMinimizeSolid,
        /// <summary>Window Minimize - window-minimize - f2d1</summary>
        [FAIcon("window-minimize", "Window Minimize", FAStyle.Regular, "\uf2d1")] WindowMinimizeRegular,
        /// <summary>Window Restore - window-restore - f2d2</summary>
        [FAIcon("window-restore", "Window Restore", FAStyle.Solid, "\uf2d2")] WindowRestoreSolid,
        /// <summary>Window Restore - window-restore - f2d2</summary>
        [FAIcon("window-restore", "Window Restore", FAStyle.Regular, "\uf2d2")] WindowRestoreRegular,
        /// <summary>Windows - windows - f17a</summary>
        [FAIcon("windows", "Windows", FAStyle.Brands, "\uf17a")] WindowsBrands,
        /// <summary>Wine Glass - wine-glass - f4e3</summary>
        [FAIcon("wine-glass", "Wine Glass", FAStyle.Solid, "\uf4e3")] WineGlassSolid,
        /// <summary>Wine Glass-alt - wine-glass-alt - f5ce</summary>
        [FAIcon("wine-glass-alt", "Wine Glass-alt", FAStyle.Solid, "\uf5ce")] WineGlassAltSolid,
        /// <summary>Wix - wix - f5cf</summary>
        [FAIcon("wix", "Wix", FAStyle.Brands, "\uf5cf")] WixBrands,
        /// <summary>Wolf Pack-battalion - wolf-pack-battalion - f514</summary>
        [FAIcon("wolf-pack-battalion", "Wolf Pack-battalion", FAStyle.Brands, "\uf514")] WolfPackBattalionBrands,
        /// <summary>Won Sign - won-sign - f159</summary>
        [FAIcon("won-sign", "Won Sign", FAStyle.Solid, "\uf159")] WonSignSolid,
        /// <summary>WordPress Logo - wordpress - f19a</summary>
        [FAIcon("wordpress", "WordPress Logo", FAStyle.Brands, "\uf19a")] WordpressBrands,
        /// <summary>Wordpress Simple - wordpress-simple - f411</summary>
        [FAIcon("wordpress-simple", "Wordpress Simple", FAStyle.Brands, "\uf411")] WordpressSimpleBrands,
        /// <summary>WPBeginner - wpbeginner - f297</summary>
        [FAIcon("wpbeginner", "WPBeginner", FAStyle.Brands, "\uf297")] WpbeginnerBrands,
        /// <summary>WPExplorer - wpexplorer - f2de</summary>
        [FAIcon("wpexplorer", "WPExplorer", FAStyle.Brands, "\uf2de")] WpexplorerBrands,
        /// <summary>WPForms - wpforms - f298</summary>
        [FAIcon("wpforms", "WPForms", FAStyle.Brands, "\uf298")] WpformsBrands,
        /// <summary>Wrench - wrench - f0ad</summary>
        [FAIcon("wrench", "Wrench", FAStyle.Solid, "\uf0ad")] WrenchSolid,
        /// <summary>X-Ray - x-ray - f497</summary>
        [FAIcon("x-ray", "X-Ray", FAStyle.Solid, "\uf497")] XRaySolid,
        /// <summary>Xbox - xbox - f412</summary>
        [FAIcon("xbox", "Xbox", FAStyle.Brands, "\uf412")] XboxBrands,
        /// <summary>Xing - xing - f168</summary>
        [FAIcon("xing", "Xing", FAStyle.Brands, "\uf168")] XingBrands,
        /// <summary>Xing Square - xing-square - f169</summary>
        [FAIcon("xing-square", "Xing Square", FAStyle.Brands, "\uf169")] XingSquareBrands,
        /// <summary>Y Combinator - y-combinator - f23b</summary>
        [FAIcon("y-combinator", "Y Combinator", FAStyle.Brands, "\uf23b")] YCombinatorBrands,
        /// <summary>Yahoo Logo - yahoo - f19e</summary>
        [FAIcon("yahoo", "Yahoo Logo", FAStyle.Brands, "\uf19e")] YahooBrands,
        /// <summary>Yandex - yandex - f413</summary>
        [FAIcon("yandex", "Yandex", FAStyle.Brands, "\uf413")] YandexBrands,
        /// <summary>Yandex International - yandex-international - f414</summary>
        [FAIcon("yandex-international", "Yandex International", FAStyle.Brands, "\uf414")] YandexInternationalBrands,
        /// <summary>Yelp - yelp - f1e9</summary>
        [FAIcon("yelp", "Yelp", FAStyle.Brands, "\uf1e9")] YelpBrands,
        /// <summary>Yen Sign - yen-sign - f157</summary>
        [FAIcon("yen-sign", "Yen Sign", FAStyle.Solid, "\uf157")] YenSignSolid,
        /// <summary>Yoast - yoast - f2b1</summary>
        [FAIcon("yoast", "Yoast", FAStyle.Brands, "\uf2b1")] YoastBrands,
        /// <summary>YouTube - youtube - f167</summary>
        [FAIcon("youtube", "YouTube", FAStyle.Brands, "\uf167")] YoutubeBrands,
        /// <summary>YouTube Square - youtube-square - f431</summary>
        [FAIcon("youtube-square", "YouTube Square", FAStyle.Brands, "\uf431")] YoutubeSquareBrands,
        /// <summary>Zhihu - zhihu - f63f</summary>
        [FAIcon("zhihu", "Zhihu", FAStyle.Brands, "\uf63f")] ZhihuBrands,
    }
}




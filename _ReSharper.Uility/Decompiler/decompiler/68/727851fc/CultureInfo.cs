// Type: System.Globalization.CultureInfo
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System;
using System.Collections;
using System.Resources;
using System.Runtime;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Permissions;
using System.Threading;

namespace System.Globalization
{
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public class CultureInfo : ICloneable, IFormatProvider
  {
    private static readonly bool init = CultureInfo.Init();
    [OptionalField(VersionAdded = 1)]
    internal int cultureID = (int) sbyte.MaxValue;
    internal const int LOCALE_NEUTRAL = 0;
    internal const int LOCALE_CUSTOM_DEFAULT = 3072;
    internal const int LOCALE_CUSTOM_UNSPECIFIED = 4096;
    internal const int LOCALE_INVARIANT = 127;
    internal bool m_isReadOnly;
    internal CompareInfo compareInfo;
    internal TextInfo textInfo;
    [NonSerialized]
    internal RegionInfo regionInfo;
    internal NumberFormatInfo numInfo;
    internal DateTimeFormatInfo dateTimeInfo;
    internal Calendar calendar;
    [OptionalField(VersionAdded = 1)]
    internal int m_dataItem;
    [NonSerialized]
    internal CultureData m_cultureData;
    [NonSerialized]
    internal bool m_isInherited;
    [NonSerialized]
    private bool m_isSafeCrossDomain;
    [NonSerialized]
    private int m_createdDomainID;
    [NonSerialized]
    private CultureInfo m_consoleFallbackCulture;
    internal string m_name;
    [NonSerialized]
    private string m_nonSortName;
    [NonSerialized]
    private string m_sortName;
    private static volatile CultureInfo s_userDefaultCulture;
    private static volatile CultureInfo s_InvariantCultureInfo;
    private static volatile CultureInfo s_userDefaultUICulture;
    private static volatile CultureInfo s_InstalledUICultureInfo;
    private static volatile CultureInfo s_DefaultThreadCurrentUICulture;
    private static volatile CultureInfo s_DefaultThreadCurrentCulture;
    private static volatile Hashtable s_LcidCachedCultures;
    private static volatile Hashtable s_NameCachedCultures;
    [SecurityCritical]
    private static volatile WindowsRuntimeResourceManagerBase s_WindowsRuntimeResourceManager;
    [ThreadStatic]
    private static bool ts_IsDoingAppXCultureInfoLookup;
    [NonSerialized]
    private CultureInfo m_parent;
    private bool m_useUserOverride;
    private static volatile bool s_isTaiwanSku;
    private static volatile bool s_haveIsTaiwanSku;
    private const int LOCALE_USER_DEFAULT = 1024;
    private const int LOCALE_SYSTEM_DEFAULT = 2048;
    private const int LOCALE_TRADITIONAL_SPANISH = 1034;
    private const int LOCALE_SORTID_MASK = 983040;

    internal bool IsSafeCrossDomain
    {
      [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")] get
      {
        return this.m_isSafeCrossDomain;
      }
    }

    internal int CreatedDomainID
    {
      [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")] get
      {
        return this.m_createdDomainID;
      }
    }

    [__DynamicallyInvokable]
    public static CultureInfo CurrentCulture
    {
      [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries"), __DynamicallyInvokable] get
      {
        return Thread.CurrentThread.CurrentCulture;
      }
    }

    internal static CultureInfo UserDefaultCulture
    {
      get
      {
        CultureInfo cultureInfo = CultureInfo.s_userDefaultCulture;
        if (cultureInfo == null)
        {
          CultureInfo.s_userDefaultCulture = CultureInfo.InvariantCulture;
          cultureInfo = CultureInfo.InitUserDefaultCulture();
          CultureInfo.s_userDefaultCulture = cultureInfo;
        }
        return cultureInfo;
      }
    }

    internal static CultureInfo UserDefaultUICulture
    {
      get
      {
        CultureInfo cultureInfo = CultureInfo.s_userDefaultUICulture;
        if (cultureInfo == null)
        {
          CultureInfo.s_userDefaultUICulture = CultureInfo.InvariantCulture;
          cultureInfo = CultureInfo.InitUserDefaultUICulture();
          CultureInfo.s_userDefaultUICulture = cultureInfo;
        }
        return cultureInfo;
      }
    }

    [__DynamicallyInvokable]
    public static CultureInfo CurrentUICulture
    {
      [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries"), __DynamicallyInvokable] get
      {
        return Thread.CurrentThread.CurrentUICulture;
      }
    }

    public static CultureInfo InstalledUICulture
    {
      get
      {
        CultureInfo cultureInfo = CultureInfo.s_InstalledUICultureInfo;
        if (cultureInfo == null)
        {
          cultureInfo = CultureInfo.GetCultureByName(CultureInfo.GetSystemDefaultUILanguage(), true) ?? CultureInfo.InvariantCulture;
          cultureInfo.m_isReadOnly = true;
          CultureInfo.s_InstalledUICultureInfo = cultureInfo;
        }
        return cultureInfo;
      }
    }

    [__DynamicallyInvokable]
    public static CultureInfo DefaultThreadCurrentCulture
    {
      [__DynamicallyInvokable] get
      {
        return CultureInfo.s_DefaultThreadCurrentCulture;
      }
      [SecuritySafeCritical, __DynamicallyInvokable, SecurityPermission(SecurityAction.Demand, ControlThread = true)] set
      {
        CultureInfo.s_DefaultThreadCurrentCulture = value;
      }
    }

    [__DynamicallyInvokable]
    public static CultureInfo DefaultThreadCurrentUICulture
    {
      [__DynamicallyInvokable] get
      {
        return CultureInfo.s_DefaultThreadCurrentUICulture;
      }
      [SecuritySafeCritical, __DynamicallyInvokable, SecurityPermission(SecurityAction.Demand, ControlThread = true)] set
      {
        if (value != null)
          CultureInfo.VerifyCultureName(value, true);
        CultureInfo.s_DefaultThreadCurrentUICulture = value;
      }
    }

    [__DynamicallyInvokable]
    public static CultureInfo InvariantCulture
    {
      [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries"), __DynamicallyInvokable] get
      {
        return CultureInfo.s_InvariantCultureInfo;
      }
    }

    [__DynamicallyInvokable]
    public virtual CultureInfo Parent
    {
      [SecuritySafeCritical, __DynamicallyInvokable] get
      {
        if (this.m_parent == null)
        {
          try
          {
            string sparent = this.m_cultureData.SPARENT;
            this.m_parent = !string.IsNullOrEmpty(sparent) ? new CultureInfo(sparent, this.m_cultureData.UseUserOverride) : CultureInfo.InvariantCulture;
          }
          catch (ArgumentException ex)
          {
            this.m_parent = CultureInfo.InvariantCulture;
          }
        }
        return this.m_parent;
      }
    }

    public virtual int LCID
    {
      [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")] get
      {
        return this.m_cultureData.ILANGUAGE;
      }
    }

    [ComVisible(false)]
    public virtual int KeyboardLayoutId
    {
      get
      {
        return this.m_cultureData.IINPUTLANGUAGEHANDLE;
      }
    }

    [__DynamicallyInvokable]
    public virtual string Name
    {
      [__DynamicallyInvokable] get
      {
        if (this.m_nonSortName == null)
        {
          this.m_nonSortName = this.m_cultureData.SNAME;
          if (this.m_nonSortName == null)
            this.m_nonSortName = string.Empty;
        }
        return this.m_nonSortName;
      }
    }

    internal string SortName
    {
      get
      {
        if (this.m_sortName == null)
          this.m_sortName = this.m_cultureData.SCOMPAREINFO;
        return this.m_sortName;
      }
    }

    [ComVisible(false)]
    public string IetfLanguageTag
    {
      get
      {
        switch (this.Name)
        {
          case "zh-CHT":
            return "zh-Hant";
          case "zh-CHS":
            return "zh-Hans";
          default:
            return this.Name;
        }
      }
    }

    [__DynamicallyInvokable]
    public virtual string DisplayName
    {
      [SecuritySafeCritical, __DynamicallyInvokable] get
      {
        return this.m_cultureData.SLOCALIZEDDISPLAYNAME;
      }
    }

    [__DynamicallyInvokable]
    public virtual string NativeName
    {
      [SecuritySafeCritical, __DynamicallyInvokable] get
      {
        return this.m_cultureData.SNATIVEDISPLAYNAME;
      }
    }

    [__DynamicallyInvokable]
    public virtual string EnglishName
    {
      [SecuritySafeCritical, __DynamicallyInvokable] get
      {
        return this.m_cultureData.SENGDISPLAYNAME;
      }
    }

    [__DynamicallyInvokable]
    public virtual string TwoLetterISOLanguageName
    {
      [SecuritySafeCritical, __DynamicallyInvokable] get
      {
        return this.m_cultureData.SISO639LANGNAME;
      }
    }

    public virtual string ThreeLetterISOLanguageName
    {
      [SecuritySafeCritical] get
      {
        return this.m_cultureData.SISO639LANGNAME2;
      }
    }

    public virtual string ThreeLetterWindowsLanguageName
    {
      [SecuritySafeCritical] get
      {
        return this.m_cultureData.SABBREVLANGNAME;
      }
    }

    [__DynamicallyInvokable]
    public virtual CompareInfo CompareInfo
    {
      [__DynamicallyInvokable] get
      {
        if (this.compareInfo == null)
        {
          CompareInfo compareInfo = this.UseUserOverride ? CultureInfo.GetCultureInfo(this.m_name).CompareInfo : new CompareInfo(this);
          if (!CompatibilitySwitches.IsCompatibilityBehaviorDefined)
            return compareInfo;
          this.compareInfo = compareInfo;
        }
        return this.compareInfo;
      }
    }

    RegionInfo Region
    {
      private get
      {
        if (this.regionInfo == null)
          this.regionInfo = new RegionInfo(this.m_cultureData);
        return this.regionInfo;
      }
    }

    [__DynamicallyInvokable]
    public virtual TextInfo TextInfo
    {
      [__DynamicallyInvokable] get
      {
        if (this.textInfo == null)
        {
          TextInfo textInfo = new TextInfo(this.m_cultureData);
          textInfo.SetReadOnlyState(this.m_isReadOnly);
          if (!CompatibilitySwitches.IsCompatibilityBehaviorDefined)
            return textInfo;
          this.textInfo = textInfo;
        }
        return this.textInfo;
      }
    }

    [__DynamicallyInvokable]
    public virtual bool IsNeutralCulture
    {
      [__DynamicallyInvokable] get
      {
        return this.m_cultureData.IsNeutralCulture;
      }
    }

    [ComVisible(false)]
    public CultureTypes CultureTypes
    {
      get
      {
        CultureTypes cultureTypes = (CultureTypes) 0;
        return (CultureTypes) ((!this.m_cultureData.IsNeutralCulture ? (int) (cultureTypes | CultureTypes.SpecificCultures) : (int) (cultureTypes | CultureTypes.NeutralCultures)) | (this.m_cultureData.IsWin32Installed ? 4 : 0) | (this.m_cultureData.IsFramework ? 64 : 0) | (this.m_cultureData.IsSupplementalCustomCulture ? 8 : 0) | (this.m_cultureData.IsReplacementCulture ? 24 : 0));
      }
    }

    [__DynamicallyInvokable]
    public virtual NumberFormatInfo NumberFormat
    {
      [__DynamicallyInvokable] get
      {
        if (this.numInfo == null)
          this.numInfo = new NumberFormatInfo(this.m_cultureData)
          {
            isReadOnly = this.m_isReadOnly
          };
        return this.numInfo;
      }
      [__DynamicallyInvokable] set
      {
        if (value == null)
          throw new ArgumentNullException("value", Environment.GetResourceString("ArgumentNull_Obj"));
        this.VerifyWritable();
        this.numInfo = value;
      }
    }

    [__DynamicallyInvokable]
    public virtual DateTimeFormatInfo DateTimeFormat
    {
      [__DynamicallyInvokable] get
      {
        if (this.dateTimeInfo == null)
        {
          DateTimeFormatInfo dateTimeFormatInfo = new DateTimeFormatInfo(this.m_cultureData, this.Calendar);
          dateTimeFormatInfo.m_isReadOnly = this.m_isReadOnly;
          Thread.MemoryBarrier();
          this.dateTimeInfo = dateTimeFormatInfo;
        }
        return this.dateTimeInfo;
      }
      [__DynamicallyInvokable] set
      {
        if (value == null)
          throw new ArgumentNullException("value", Environment.GetResourceString("ArgumentNull_Obj"));
        this.VerifyWritable();
        this.dateTimeInfo = value;
      }
    }

    [__DynamicallyInvokable]
    public virtual Calendar Calendar
    {
      [__DynamicallyInvokable] get
      {
        if (this.calendar == null)
        {
          Calendar defaultCalendar = this.m_cultureData.DefaultCalendar;
          Thread.MemoryBarrier();
          defaultCalendar.SetReadOnlyState(this.m_isReadOnly);
          this.calendar = defaultCalendar;
        }
        return this.calendar;
      }
    }

    [__DynamicallyInvokable]
    public virtual Calendar[] OptionalCalendars
    {
      [__DynamicallyInvokable] get
      {
        int[] calendarIds = this.m_cultureData.CalendarIds;
        Calendar[] calendarArray = new Calendar[calendarIds.Length];
        for (int index = 0; index < calendarArray.Length; ++index)
          calendarArray[index] = CultureInfo.GetCalendarInstance(calendarIds[index]);
        return calendarArray;
      }
    }

    public bool UseUserOverride
    {
      get
      {
        return this.m_cultureData.UseUserOverride;
      }
    }

    [__DynamicallyInvokable]
    public bool IsReadOnly
    {
      [__DynamicallyInvokable, TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")] get
      {
        return this.m_isReadOnly;
      }
    }

    internal bool HasInvariantCultureName
    {
      get
      {
        return this.Name == CultureInfo.InvariantCulture.Name;
      }
    }

    internal static bool IsTaiwanSku
    {
      get
      {
        if (!CultureInfo.s_haveIsTaiwanSku)
        {
          CultureInfo.s_isTaiwanSku = CultureInfo.GetSystemDefaultUILanguage() == "zh-TW";
          CultureInfo.s_haveIsTaiwanSku = true;
        }
        return CultureInfo.s_isTaiwanSku;
      }
    }

    static CultureInfo()
    {
    }

    [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
    [__DynamicallyInvokable]
    public CultureInfo(string name)
      : this(name, true)
    {
    }

    public CultureInfo(string name, bool useUserOverride)
    {
      if (name == null)
        throw new ArgumentNullException("name", Environment.GetResourceString("ArgumentNull_String"));
      this.m_cultureData = CultureData.GetCultureData(name, useUserOverride);
      if (this.m_cultureData == null)
        throw new CultureNotFoundException("name", name, Environment.GetResourceString("Argument_CultureNotSupported"));
      this.m_name = this.m_cultureData.CultureName;
      this.m_isInherited = this.GetType() != typeof (CultureInfo);
    }

    [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
    public CultureInfo(int culture)
      : this(culture, true)
    {
    }

    public CultureInfo(int culture, bool useUserOverride)
    {
      if (culture < 0)
        throw new ArgumentOutOfRangeException("culture", Environment.GetResourceString("ArgumentOutOfRange_NeedPosNum"));
      this.InitializeFromCultureId(culture, useUserOverride);
    }

    internal CultureInfo(string cultureName, string textAndCompareCultureName)
    {
      if (cultureName == null)
        throw new ArgumentNullException("cultureName", Environment.GetResourceString("ArgumentNull_String"));
      this.m_cultureData = CultureData.GetCultureData(cultureName, false);
      if (this.m_cultureData == null)
        throw new CultureNotFoundException("cultureName", cultureName, Environment.GetResourceString("Argument_CultureNotSupported"));
      this.m_name = this.m_cultureData.CultureName;
      CultureInfo cultureInfo = CultureInfo.GetCultureInfo(textAndCompareCultureName);
      this.compareInfo = cultureInfo.CompareInfo;
      this.textInfo = cultureInfo.TextInfo;
    }

    [SecuritySafeCritical]
    internal static CultureInfo GetCultureInfoForUserPreferredLanguageInAppX()
    {
      if (CultureInfo.ts_IsDoingAppXCultureInfoLookup)
        return (CultureInfo) null;
      if (AppDomain.IsAppXNGen)
        return (CultureInfo) null;
      try
      {
        CultureInfo.ts_IsDoingAppXCultureInfoLookup = true;
        if (CultureInfo.s_WindowsRuntimeResourceManager == null)
          CultureInfo.s_WindowsRuntimeResourceManager = ResourceManager.GetWinRTResourceManager();
        return CultureInfo.s_WindowsRuntimeResourceManager.GlobalResourceContextBestFitCultureInfo;
      }
      finally
      {
        CultureInfo.ts_IsDoingAppXCultureInfoLookup = false;
      }
    }

    internal static void CheckDomainSafetyObject(object obj, object container)
    {
      if (!(obj.GetType().Assembly != typeof (CultureInfo).Assembly))
        return;
      throw new InvalidOperationException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("InvalidOperation_SubclassedObject"), new object[2]
      {
        (object) obj.GetType(),
        (object) container.GetType()
      }));
    }

    internal bool CanSendCrossDomain()
    {
      bool flag = false;
      if (this.GetType() == typeof (CultureInfo))
        flag = true;
      return flag;
    }

    internal void StartCrossDomainTracking()
    {
      if (this.m_createdDomainID != 0)
        return;
      if (this.CanSendCrossDomain())
        this.m_isSafeCrossDomain = true;
      Thread.MemoryBarrier();
      this.m_createdDomainID = Thread.GetDomainID();
    }

    public static CultureInfo CreateSpecificCulture(string name)
    {
      CultureInfo cultureInfo;
      try
      {
        cultureInfo = new CultureInfo(name);
      }
      catch (ArgumentException ex1)
      {
        cultureInfo = (CultureInfo) null;
        for (int length = 0; length < name.Length; ++length)
        {
          if (45 == (int) name[length])
          {
            try
            {
              cultureInfo = new CultureInfo(name.Substring(0, length));
              break;
            }
            catch (ArgumentException ex2)
            {
              throw;
            }
          }
        }
        if (cultureInfo == null)
          throw;
      }
      if (!cultureInfo.IsNeutralCulture)
        return cultureInfo;
      else
        return new CultureInfo(cultureInfo.m_cultureData.SSPECIFICCULTURE);
    }

    internal static bool VerifyCultureName(string cultureName, bool throwException)
    {
      for (int index = 0; index < cultureName.Length; ++index)
      {
        char c = cultureName[index];
        if (!char.IsLetterOrDigit(c) && (int) c != 45 && (int) c != 95)
        {
          if (!throwException)
            return false;
          throw new ArgumentException(Environment.GetResourceString("Argument_InvalidResourceCultureName", new object[1]
          {
            (object) cultureName
          }));
        }
      }
      return true;
    }

    internal static bool VerifyCultureName(CultureInfo culture, bool throwException)
    {
      if (!culture.m_isInherited)
        return true;
      else
        return CultureInfo.VerifyCultureName(culture.Name, throwException);
    }

    public static CultureInfo[] GetCultures(CultureTypes types)
    {
      if ((types & CultureTypes.UserCustomCulture) == CultureTypes.UserCustomCulture)
        types |= CultureTypes.ReplacementCultures;
      return CultureData.GetCultures(types);
    }

    [__DynamicallyInvokable]
    public override bool Equals(object value)
    {
      if (object.ReferenceEquals((object) this, value))
        return true;
      CultureInfo cultureInfo = value as CultureInfo;
      if (cultureInfo != null && this.Name.Equals(cultureInfo.Name))
        return this.CompareInfo.Equals((object) cultureInfo.CompareInfo);
      else
        return false;
    }

    [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
    [__DynamicallyInvokable]
    public override int GetHashCode()
    {
      return this.Name.GetHashCode() + this.CompareInfo.GetHashCode();
    }

    [__DynamicallyInvokable]
    public override string ToString()
    {
      return this.m_name;
    }

    [__DynamicallyInvokable]
    public virtual object GetFormat(Type formatType)
    {
      if (formatType == typeof (NumberFormatInfo))
        return (object) this.NumberFormat;
      if (formatType == typeof (DateTimeFormatInfo))
        return (object) this.DateTimeFormat;
      else
        return (object) null;
    }

    public void ClearCachedData()
    {
      CultureInfo.s_userDefaultUICulture = (CultureInfo) null;
      CultureInfo.s_userDefaultCulture = (CultureInfo) null;
      RegionInfo.s_currentRegionInfo = (RegionInfo) null;
      TimeZone.ResetTimeZone();
      TimeZoneInfo.ClearCachedData();
      CultureInfo.s_LcidCachedCultures = (Hashtable) null;
      CultureInfo.s_NameCachedCultures = (Hashtable) null;
      CultureData.ClearCachedData();
    }

    internal static Calendar GetCalendarInstance(int calType)
    {
      if (calType == 1)
        return (Calendar) new GregorianCalendar();
      else
        return CultureInfo.GetCalendarInstanceRare(calType);
    }

    internal static Calendar GetCalendarInstanceRare(int calType)
    {
      switch (calType)
      {
        case 2:
        case 9:
        case 10:
        case 11:
        case 12:
          return (Calendar) new GregorianCalendar((GregorianCalendarTypes) calType);
        case 3:
          return (Calendar) new JapaneseCalendar();
        case 4:
          return (Calendar) new TaiwanCalendar();
        case 5:
          return (Calendar) new KoreanCalendar();
        case 6:
          return (Calendar) new HijriCalendar();
        case 7:
          return (Calendar) new ThaiBuddhistCalendar();
        case 8:
          return (Calendar) new HebrewCalendar();
        case 14:
          return (Calendar) new JapaneseLunisolarCalendar();
        case 15:
          return (Calendar) new ChineseLunisolarCalendar();
        case 20:
          return (Calendar) new KoreanLunisolarCalendar();
        case 21:
          return (Calendar) new TaiwanLunisolarCalendar();
        case 22:
          return (Calendar) new PersianCalendar();
        case 23:
          return (Calendar) new UmAlQuraCalendar();
        default:
          return (Calendar) new GregorianCalendar();
      }
    }

    [SecuritySafeCritical]
    [ComVisible(false)]
    public CultureInfo GetConsoleFallbackUICulture()
    {
      CultureInfo cultureInfo = this.m_consoleFallbackCulture;
      if (cultureInfo == null)
      {
        cultureInfo = CultureInfo.CreateSpecificCulture(this.m_cultureData.SCONSOLEFALLBACKNAME);
        cultureInfo.m_isReadOnly = true;
        this.m_consoleFallbackCulture = cultureInfo;
      }
      return cultureInfo;
    }

    [__DynamicallyInvokable]
    public virtual object Clone()
    {
      CultureInfo cultureInfo = (CultureInfo) this.MemberwiseClone();
      cultureInfo.m_isReadOnly = false;
      if (!this.m_isInherited)
      {
        if (this.dateTimeInfo != null)
          cultureInfo.dateTimeInfo = (DateTimeFormatInfo) this.dateTimeInfo.Clone();
        if (this.numInfo != null)
          cultureInfo.numInfo = (NumberFormatInfo) this.numInfo.Clone();
      }
      else
      {
        cultureInfo.DateTimeFormat = (DateTimeFormatInfo) this.DateTimeFormat.Clone();
        cultureInfo.NumberFormat = (NumberFormatInfo) this.NumberFormat.Clone();
      }
      if (this.textInfo != null)
        cultureInfo.textInfo = (TextInfo) this.textInfo.Clone();
      if (this.calendar != null)
        cultureInfo.calendar = (Calendar) this.calendar.Clone();
      return (object) cultureInfo;
    }

    [__DynamicallyInvokable]
    public static CultureInfo ReadOnly(CultureInfo ci)
    {
      if (ci == null)
        throw new ArgumentNullException("ci");
      if (ci.IsReadOnly)
        return ci;
      CultureInfo cultureInfo = (CultureInfo) ci.MemberwiseClone();
      if (!ci.IsNeutralCulture)
      {
        if (!ci.m_isInherited)
        {
          if (ci.dateTimeInfo != null)
            cultureInfo.dateTimeInfo = DateTimeFormatInfo.ReadOnly(ci.dateTimeInfo);
          if (ci.numInfo != null)
            cultureInfo.numInfo = NumberFormatInfo.ReadOnly(ci.numInfo);
        }
        else
        {
          cultureInfo.DateTimeFormat = DateTimeFormatInfo.ReadOnly(ci.DateTimeFormat);
          cultureInfo.NumberFormat = NumberFormatInfo.ReadOnly(ci.NumberFormat);
        }
      }
      if (ci.textInfo != null)
        cultureInfo.textInfo = TextInfo.ReadOnly(ci.textInfo);
      if (ci.calendar != null)
        cultureInfo.calendar = Calendar.ReadOnly(ci.calendar);
      cultureInfo.m_isReadOnly = true;
      return cultureInfo;
    }

    internal static CultureInfo GetCultureInfoHelper(int lcid, string name, string altName)
    {
      Hashtable hashtable1 = CultureInfo.s_NameCachedCultures;
      if (name != null)
        name = CultureData.AnsiToLower(name);
      if (altName != null)
        altName = CultureData.AnsiToLower(altName);
      if (hashtable1 == null)
        hashtable1 = Hashtable.Synchronized(new Hashtable());
      else if (lcid == -1)
      {
        CultureInfo cultureInfo = (CultureInfo) hashtable1[(object) (name + (object) '�' + altName)];
        if (cultureInfo != null)
          return cultureInfo;
      }
      else if (lcid == 0)
      {
        CultureInfo cultureInfo = (CultureInfo) hashtable1[(object) name];
        if (cultureInfo != null)
          return cultureInfo;
      }
      Hashtable hashtable2 = CultureInfo.s_LcidCachedCultures;
      if (hashtable2 == null)
        hashtable2 = Hashtable.Synchronized(new Hashtable());
      else if (lcid > 0)
      {
        CultureInfo cultureInfo = (CultureInfo) hashtable2[(object) lcid];
        if (cultureInfo != null)
          return cultureInfo;
      }
      CultureInfo cultureInfo1;
      try
      {
        switch (lcid)
        {
          case -1:
            cultureInfo1 = new CultureInfo(name, altName);
            break;
          case 0:
            cultureInfo1 = new CultureInfo(name, false);
            break;
          default:
            cultureInfo1 = new CultureInfo(lcid, false);
            break;
        }
      }
      catch (ArgumentException ex)
      {
        return (CultureInfo) null;
      }
      cultureInfo1.m_isReadOnly = true;
      if (lcid == -1)
      {
        hashtable1[(object) (name + (object) '�' + altName)] = (object) cultureInfo1;
        cultureInfo1.TextInfo.SetReadOnlyState(true);
      }
      else
      {
        string str = CultureData.AnsiToLower(cultureInfo1.m_name);
        hashtable1[(object) str] = (object) cultureInfo1;
        if ((cultureInfo1.LCID != 4 || !(str == "zh-hans")) && (cultureInfo1.LCID != 31748 || !(str == "zh-hant")))
          hashtable2[(object) cultureInfo1.LCID] = (object) cultureInfo1;
      }
      if (-1 != lcid)
        CultureInfo.s_LcidCachedCultures = hashtable2;
      CultureInfo.s_NameCachedCultures = hashtable1;
      return cultureInfo1;
    }

    public static CultureInfo GetCultureInfo(int culture)
    {
      if (culture <= 0)
        throw new ArgumentOutOfRangeException("culture", Environment.GetResourceString("ArgumentOutOfRange_NeedPosNum"));
      CultureInfo cultureInfoHelper = CultureInfo.GetCultureInfoHelper(culture, (string) null, (string) null);
      if (cultureInfoHelper == null)
        throw new CultureNotFoundException("culture", culture, Environment.GetResourceString("Argument_CultureNotSupported"));
      else
        return cultureInfoHelper;
    }

    public static CultureInfo GetCultureInfo(string name)
    {
      if (name == null)
        throw new ArgumentNullException("name");
      CultureInfo cultureInfoHelper = CultureInfo.GetCultureInfoHelper(0, name, (string) null);
      if (cultureInfoHelper == null)
        throw new CultureNotFoundException("name", name, Environment.GetResourceString("Argument_CultureNotSupported"));
      else
        return cultureInfoHelper;
    }

    public static CultureInfo GetCultureInfo(string name, string altName)
    {
      if (name == null)
        throw new ArgumentNullException("name");
      if (altName == null)
        throw new ArgumentNullException("altName");
      CultureInfo cultureInfoHelper = CultureInfo.GetCultureInfoHelper(-1, name, altName);
      if (cultureInfoHelper != null)
        return cultureInfoHelper;
      throw new CultureNotFoundException("name or altName", string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Argument_OneOfCulturesNotSupported"), new object[2]
      {
        (object) name,
        (object) altName
      }));
    }

    public static CultureInfo GetCultureInfoByIetfLanguageTag(string name)
    {
      if (name == "zh-CHT" || name == "zh-CHS")
      {
        throw new CultureNotFoundException("name", string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Argument_CultureIetfNotSupported"), new object[1]
        {
          (object) name
        }));
      }
      else
      {
        CultureInfo cultureInfo = CultureInfo.GetCultureInfo(name);
        if (cultureInfo.LCID <= (int) ushort.MaxValue && cultureInfo.LCID != 1034)
          return cultureInfo;
        throw new CultureNotFoundException("name", string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Argument_CultureIetfNotSupported"), new object[1]
        {
          (object) name
        }));
      }
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static string nativeGetLocaleInfoEx(string localeName, uint field);

    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static int nativeGetLocaleInfoExInt(string localeName, uint field);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static bool nativeSetThreadLocale(string localeName);

    private static bool Init()
    {
      if (CultureInfo.s_InvariantCultureInfo == null)
        CultureInfo.s_InvariantCultureInfo = new CultureInfo("", false)
        {
          m_isReadOnly = true
        };
      CultureInfo.s_userDefaultCulture = CultureInfo.s_userDefaultUICulture = CultureInfo.s_InvariantCultureInfo;
      CultureInfo.s_userDefaultCulture = CultureInfo.InitUserDefaultCulture();
      CultureInfo.s_userDefaultUICulture = CultureInfo.InitUserDefaultUICulture();
      return true;
    }

    [SecuritySafeCritical]
    private static CultureInfo InitUserDefaultCulture()
    {
      string defaultLocaleName = CultureInfo.GetDefaultLocaleName(1024);
      if (defaultLocaleName == null)
      {
        defaultLocaleName = CultureInfo.GetDefaultLocaleName(2048);
        if (defaultLocaleName == null)
          return CultureInfo.InvariantCulture;
      }
      CultureInfo cultureByName = CultureInfo.GetCultureByName(defaultLocaleName, true);
      cultureByName.m_isReadOnly = true;
      return cultureByName;
    }

    private static CultureInfo InitUserDefaultUICulture()
    {
      string defaultUiLanguage = CultureInfo.GetUserDefaultUILanguage();
      if (defaultUiLanguage == CultureInfo.UserDefaultCulture.Name)
        return CultureInfo.UserDefaultCulture;
      CultureInfo cultureByName = CultureInfo.GetCultureByName(defaultUiLanguage, true);
      if (cultureByName == null)
        return CultureInfo.InvariantCulture;
      cultureByName.m_isReadOnly = true;
      return cultureByName;
    }

    private void InitializeFromCultureId(int culture, bool useUserOverride)
    {
      switch (culture)
      {
        case 2048:
        case 3072:
        case 4096:
        case 0:
        case 1024:
          throw new CultureNotFoundException("culture", culture, Environment.GetResourceString("Argument_CultureNotSupported"));
        default:
          this.m_cultureData = CultureData.GetCultureData(culture, useUserOverride);
          this.m_isInherited = this.GetType() != typeof (CultureInfo);
          this.m_name = this.m_cultureData.CultureName;
          break;
      }
    }

    [OnDeserialized]
    private void OnDeserialized(StreamingContext ctx)
    {
      if (this.m_name == null || CultureInfo.IsAlternateSortLcid(this.cultureID))
      {
        this.InitializeFromCultureId(this.cultureID, this.m_useUserOverride);
      }
      else
      {
        this.m_cultureData = CultureData.GetCultureData(this.m_name, this.m_useUserOverride);
        if (this.m_cultureData == null)
          throw new CultureNotFoundException("m_name", this.m_name, Environment.GetResourceString("Argument_CultureNotSupported"));
      }
      this.m_isInherited = this.GetType() != typeof (CultureInfo);
      if (!(this.GetType().Assembly == typeof (CultureInfo).Assembly))
        return;
      if (this.textInfo != null)
        CultureInfo.CheckDomainSafetyObject((object) this.textInfo, (object) this);
      if (this.compareInfo == null)
        return;
      CultureInfo.CheckDomainSafetyObject((object) this.compareInfo, (object) this);
    }

    private static bool IsAlternateSortLcid(int lcid)
    {
      if (lcid == 1034)
        return true;
      else
        return (lcid & 983040) != 0;
    }

    [OnSerializing]
    private void OnSerializing(StreamingContext ctx)
    {
      this.m_name = this.m_cultureData.CultureName;
      this.m_useUserOverride = this.m_cultureData.UseUserOverride;
      this.cultureID = this.m_cultureData.ILANGUAGE;
    }

    private static CultureInfo GetCultureByName(string name, bool userOverride)
    {
      try
      {
        return userOverride ? new CultureInfo(name) : CultureInfo.GetCultureInfo(name);
      }
      catch (ArgumentException ex)
      {
      }
      return (CultureInfo) null;
    }

    private void VerifyWritable()
    {
      if (this.m_isReadOnly)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ReadOnly"));
    }

    [SecurityCritical]
    private static string GetDefaultLocaleName(int localeType)
    {
      string s = (string) null;
      if (CultureInfo.InternalGetDefaultLocaleName(localeType, JitHelpers.GetStringHandleOnStack(ref s)))
        return s;
      else
        return string.Empty;
    }

    [SuppressUnmanagedCodeSecurity]
    [SecurityCritical]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static bool InternalGetDefaultLocaleName(int localetype, StringHandleOnStack localeString);

    [SecuritySafeCritical]
    private static string GetUserDefaultUILanguage()
    {
      string s = (string) null;
      if (CultureInfo.InternalGetUserDefaultUILanguage(JitHelpers.GetStringHandleOnStack(ref s)))
        return s;
      else
        return string.Empty;
    }

    [SuppressUnmanagedCodeSecurity]
    [SecurityCritical]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static bool InternalGetUserDefaultUILanguage(StringHandleOnStack userDefaultUiLanguage);

    [SecuritySafeCritical]
    private static string GetSystemDefaultUILanguage()
    {
      string s = (string) null;
      if (CultureInfo.InternalGetSystemDefaultUILanguage(JitHelpers.GetStringHandleOnStack(ref s)))
        return s;
      else
        return string.Empty;
    }

    [SuppressUnmanagedCodeSecurity]
    [SecurityCritical]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    [MethodImpl(MethodImplOptions.InternalCall)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static bool InternalGetSystemDefaultUILanguage(StringHandleOnStack systemDefaultUiLanguage);
  }
}

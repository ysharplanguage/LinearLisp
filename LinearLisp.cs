using System.Collections.Generic;
using System.Linq;
using System.Symbolics;
using System.Text.RegularExpressions;
namespace System.Symbolics
{
    public sealed class Linear { public readonly int[] Nodes; public readonly object[] Value; public readonly Evaluation[] Wired; public readonly int[] Trace; public Linear(int[] nodes, object[] value, Evaluation[] wired) { Nodes = nodes; Value = value; Wired = wired; Trace = new int[nodes.Length]; } }
    public interface IEnvironment
    {
        object Get(Symbol symbol);
        bool Knows(Symbol symbol);
        IEnvironment Set(Symbol symbol, object value);
        IEnvironment Parent { get; }
        Global Global { get; }
    }
    public interface IFormal : IEnvironment
    {
        IEnvironment Set(int index, object value);
    }
    public sealed class Formal0 : IFormal
    {
        public Formal0(IEnvironment parent) => Parent = parent;
        public object Get(Symbol symbol) => Parent.Get(symbol);
        public bool Knows(Symbol symbol) => !Symbol.Undefined.Equals(Get(symbol));
        public IEnvironment Set(Symbol symbol, object value) { Parent.Set(symbol, value); return this; }
        public IEnvironment Set(int index, object value) => throw new InvalidOperationException();
        public IEnvironment Parent { get; }
        public Global Global => Parent.Global;
    }
    public sealed class Formal1 : IFormal
    {
        private Symbol symbol; private object value;
        public Formal1(Symbol[] parameters, IEnvironment parent) { symbol = parameters[0]; Parent = parent; }
        public Formal1(Symbol[] parameters, object[] values, IEnvironment parent) { symbol = parameters[0]; value = values[0]; Parent = parent; }
        public object Get(Symbol symbol) => symbol.Equals(this.symbol) ? value : Parent.Get(symbol);
        public bool Knows(Symbol symbol) => !Symbol.Undefined.Equals(Get(symbol));
        public IEnvironment Set(Symbol symbol, object value) { if (symbol.Equals(this.symbol)) { this.value = value; return this; } else return Parent.Set(symbol, value); }
        public IEnvironment Set(int index, object value) { this.value = value; return this; }
        public IEnvironment Parent { get; }
        public Global Global => Parent.Global;
    }
    public sealed class Formal2 : IFormal
    {
        private readonly Symbol[] symbols; private readonly object[] values = new object[2];
        public Formal2(Symbol[] parameters, IEnvironment parent) { symbols = parameters; Parent = parent; }
        public Formal2(Symbol[] parameters, object[] values, IEnvironment parent) { symbols = parameters; var i = -1; while (++i < 2) this.values[i] = values[i]; Parent = parent; }
        public object Get(Symbol symbol) { var i = -1; while (++i < 2) if (symbol.Equals(symbols[i])) return values[i]; return Parent.Get(symbol); }
        public bool Knows(Symbol symbol) => !Symbol.Undefined.Equals(Get(symbol));
        public IEnvironment Set(Symbol symbol, object value) { var i = 2; while (0 <= --i) { if (symbol.Equals(symbols[i])) { values[i] = value; return this; } } return Parent.Set(symbol, value); }
        public IEnvironment Set(int index, object value) { values[index] = value; return this; }
        public IEnvironment Parent { get; }
        public Global Global => Parent.Global;
    }
    public sealed class Formal3 : IFormal
    {
        private readonly Symbol[] symbols; private readonly object[] values = new object[3];
        public Formal3(Symbol[] parameters, IEnvironment parent) { symbols = parameters; Parent = parent; }
        public Formal3(Symbol[] parameters, object[] values, IEnvironment parent) { symbols = parameters; var i = -1; while (++i < 3) this.values[i] = values[i]; Parent = parent; }
        public object Get(Symbol symbol) { var i = -1; while (++i < 3) if (symbol.Equals(symbols[i])) return values[i]; return Parent.Get(symbol); }
        public bool Knows(Symbol symbol) => !Symbol.Undefined.Equals(Get(symbol));
        public IEnvironment Set(Symbol symbol, object value) { var i = 3; while (0 <= --i) { if (symbol.Equals(symbols[i])) { values[i] = value; return this; } } return Parent.Set(symbol, value); }
        public IEnvironment Set(int index, object value) { values[index] = value; return this; }
        public IEnvironment Parent { get; }
        public Global Global => Parent.Global;
    }
    public sealed class Formal4 : IFormal
    {
        private readonly Symbol[] symbols; private readonly object[] values = new object[4];
        public Formal4(Symbol[] parameters, IEnvironment parent) { symbols = parameters; Parent = parent; }
        public Formal4(Symbol[] parameters, object[] values, IEnvironment parent) { symbols = parameters; var i = -1; while (++i < 4) this.values[i] = values[i]; Parent = parent; }
        public object Get(Symbol symbol) { var i = -1; while (++i < 4) if (symbol.Equals(symbols[i])) return values[i]; return Parent.Get(symbol); }
        public bool Knows(Symbol symbol) => !Symbol.Undefined.Equals(Get(symbol));
        public IEnvironment Set(Symbol symbol, object value) { var i = 4; while (0 <= --i) { if (symbol.Equals(symbols[i])) { values[i] = value; return this; } } return Parent.Set(symbol, value); }
        public IEnvironment Set(int index, object value) { values[index] = value; return this; }
        public IEnvironment Parent { get; }
        public Global Global => Parent.Global;
    }
    public sealed class Formal5 : IFormal
    {
        private readonly Symbol[] symbols; private readonly object[] values = new object[5];
        public Formal5(Symbol[] parameters, IEnvironment parent) { symbols = parameters; Parent = parent; }
        public Formal5(Symbol[] parameters, object[] values, IEnvironment parent) { symbols = parameters; var i = -1; while (++i < 5) this.values[i] = values[i]; Parent = parent; }
        public object Get(Symbol symbol) { var i = -1; while (++i < 5) if (symbol.Equals(symbols[i])) return values[i]; return Parent.Get(symbol); }
        public bool Knows(Symbol symbol) => !Symbol.Undefined.Equals(Get(symbol));
        public IEnvironment Set(Symbol symbol, object value) { var i = 5; while (0 <= --i) { if (symbol.Equals(symbols[i])) { values[i] = value; return this; } } return Parent.Set(symbol, value); }
        public IEnvironment Set(int index, object value) { values[index] = value; return this; }
        public IEnvironment Parent { get; }
        public Global Global => Parent.Global;
    }
    public readonly struct Symbol : IEquatable<Symbol>
    {
        public static readonly Symbol Undefined = default, Open = new Symbol(-1), Close = new Symbol(-2), Quote = new Symbol(-3), Let = new Symbol(-4), Lambda = new Symbol(-5), EOF = new Symbol(int.MaxValue); public readonly int Id;
        public Symbol(int id) => Id = id;
        public int Class => 0 < Id ? Id >> 24 : -1;
        public int Index => 0 < Id ? Id & ((1 << 24) - 1) : -Id;
        public bool Equals(Symbol other) => other.Id == Id;
        public override bool Equals(object obj) => obj is Symbol symbol && Equals(symbol);
        public override int GetHashCode() => Id;
        public override string ToString() => $"{{{nameof(Symbol)}({$"{Class}:{(0 < Id ? Index : Id)}"})}}";
    }
    public class SymbolProvider : List<string>
    {
        private class BuiltinComparer : IComparer<string> { public int Compare(string left, string right) => !ReferenceEquals(left, right) || (string.CompareOrdinal(left, right) != 0) ? (left.Length < right.Length ? 1 : -1) : 0; }
        private readonly IDictionary<string, Symbol> symbols = new Dictionary<string, Symbol>(); private long uniqueId = -1;
        public const string Open = "(", Close = ")", Quote = "`", Let = "let", Lambda = "=>"; public readonly ISet<string> Builtins = new SortedSet<string>(new BuiltinComparer());
        public SymbolProvider(string[] core = null) { core = core ?? new[] { string.Empty, Open, Close, Quote, Let, Lambda }; for (var at = 0; at < core.Length; at++) Builtin(core[at], out _); }
        public SymbolProvider Builtin(string name, out Symbol builtin) { builtin = Get(name, -1); return this; }
        public Symbol Builtin(string literal, int startAt = -1) { bool Matches(string input, string value, int at) { var from = at; at = 0; while (from < input.Length && at < value.Length && input[from] == value[at]) { from++; at++; } return at == value.Length; } foreach (var name in Builtins) { if (name.Length > 0 && ((startAt >= 0 && Matches(literal, name, startAt)) || literal == name) && symbols[name].Id < 0) { return symbols[name]; } } return Symbol.Undefined; }
        public Symbol Get(string name, int symbolClass = 0) { if (!symbols.TryGetValue(name, out var symbol)) { symbol = new Symbol(symbolClass < 0 ? -symbols.Count : (symbolClass << 24) | symbols.Count); if (symbolClass < 0) Builtins.Add(name); symbols.Add(name, symbol); Add(name); } return symbol; }
        public Symbol Unique(string prefix) => Get($"{prefix}{Threading.Interlocked.Increment(ref uniqueId)}");
        public string NameOf(Symbol symbol) => this[symbol.Index];
    }
    public class Environment : Dictionary<Symbol, object>, IEnvironment
    {
        public static readonly object[] Empty = new object[0];
        public static IEnumerable<object> Sequence(object value, bool required = false) { bool TryGetSequence(object candidate, out IEnumerable<object> sequence) => ((sequence = candidate as object[]) ?? (sequence = candidate as IEnumerable<object>) ?? (sequence = candidate as Collections.IEnumerable != null ? ((Collections.IEnumerable)candidate).Cast<object>() : null)) != null; return TryGetSequence(value, out var found) ? found : !required ? null : throw new Exception($"Not a sequence: {nameof(value)}"); }
        public Environment(IEnvironment parent) => Global = (Parent = parent)?.Global ?? (Global)this;
        public virtual object Get(Symbol symbol) => symbol.Id <= 0 ? (-symbol.Id < Global.Builtins.Count ? Global.Builtins[-symbol.Id] : Symbol.Undefined) : TryGetValue(symbol, out var value) ? value : Parent != null ? Parent.Get(symbol) : Symbol.Undefined;
        public bool Knows(Symbol symbol) => !Symbol.Undefined.Equals(Get(symbol));
        public virtual IEnvironment Set(Symbol symbol, object value) { if (ContainsKey(symbol)) { this[symbol] = value; return this; } else return Parent.Set(symbol, value); }
        public object[] Params(object[] args, int at) { var result = args.Length > at ? new object[args.Length - at] : Empty; for (var i = at; i < args.Length; i++) result[i - at] = args[i]; return result; }
        public IEnvironment Parent { get; }
        public Global Global { get; }
    }
    public sealed class Global : Environment
    {
        private object[] builtins = new object[3] { null, null, null };
        public Global(SymbolProvider symbolProvider) : base(null) => SymbolProvider = symbolProvider ?? throw new ArgumentNullException(nameof(symbolProvider), "Cannot be null");
        public override object Get(Symbol symbol) => symbol.Id <= 0 ? (-symbol.Id < builtins.Length ? builtins[-symbol.Id] : Symbol.Undefined) : TryGetValue(symbol, out var value) ? value : Symbol.Undefined;
        public override IEnvironment Set(Symbol symbol, object value) { if (symbol.Id < 0) { var at = -symbol.Id; var upto = at + 1; if (upto > builtins.Length) Array.Resize(ref builtins, upto); builtins[at] = value; } if (0 < symbol.Id) this[symbol] = value; return this; }
        public SymbolProvider SymbolProvider { get; }
        public IReadOnlyList<object> Builtins => builtins;
    }
    public delegate object Inline(IEnvironment site, int at);
    public delegate object Invoke(params object[] arguments);
    public abstract class Closure : LinearEvaluator
    {
        protected readonly Linear linear; public readonly IEnvironment Environment; public readonly Symbol[] Parameters; public readonly int Body;
        protected Closure(IEnvironment environment, Symbol[] parameters, Linear linear, int body) { this.linear = linear; Environment = environment; Parameters = parameters; Body = body; }
        internal abstract object Inline(IEnvironment site, int at, int actuals);
        public abstract object Invoke(params object[] arguments);
    }
    public sealed class Closure0 : Closure
    {
        internal override object Inline(IEnvironment site, int at, int actuals) => Evaluate(new Formal0(Environment), linear, Body);
        public Closure0(IEnvironment environment, Symbol[] parameters, Linear linear, int body) : base(environment, parameters, linear, body) { }
        public override object Invoke(params object[] arguments) => Evaluate(new Formal0(Environment), linear, Body);
    }
    public sealed class Closure1 : Closure
    {
        internal override object Inline(IEnvironment site, int at, int actuals)
        {
            var parameters = Parameters;
            var bound = new Formal1(parameters, Environment);
            if (0 < actuals) bound.Set(0, Evaluate(site, linear, at + linear.Nodes[at]));
            return Evaluate(bound, linear, Body);
        }
        public Closure1(IEnvironment environment, Symbol[] parameters, Linear linear, int body) : base(environment, parameters, linear, body) { }
        public override object Invoke(params object[] arguments) => Evaluate(new Formal1(Parameters, arguments, Environment), linear, Body);
    }
    public sealed class Closure2 : Closure
    {
        internal override object Inline(IEnvironment site, int at, int actuals)
        {
            var parameters = Parameters;
            var bound = new Formal2(parameters, Environment);
            if (0 < actuals) bound.Set(0, Evaluate(site, linear, at + linear.Nodes[at++]));
            if (1 < actuals) bound.Set(1, Evaluate(site, linear, at + linear.Nodes[at]));
            return Evaluate(bound, linear, Body);
        }
        public Closure2(IEnvironment environment, Symbol[] parameters, Linear linear, int body) : base(environment, parameters, linear, body) { }
        public override object Invoke(params object[] arguments) => Evaluate(new Formal2(Parameters, arguments, Environment), linear, Body);
    }
    public sealed class Closure3 : Closure
    {
        internal override object Inline(IEnvironment site, int at, int actuals)
        {
            var parameters = Parameters;
            var bound = new Formal3(parameters, Environment);
            if (0 < actuals) bound.Set(0, Evaluate(site, linear, at + linear.Nodes[at++]));
            if (1 < actuals) bound.Set(1, Evaluate(site, linear, at + linear.Nodes[at++]));
            if (2 < actuals) bound.Set(2, Evaluate(site, linear, at + linear.Nodes[at]));
            return Evaluate(bound, linear, Body);
        }
        public Closure3(IEnvironment environment, Symbol[] parameters, Linear linear, int body) : base(environment, parameters, linear, body) { }
        public override object Invoke(params object[] arguments) => Evaluate(new Formal3(Parameters, arguments, Environment), linear, Body);
    }
    public sealed class Closure4 : Closure
    {
        internal override object Inline(IEnvironment site, int at, int actuals)
        {
            var parameters = Parameters;
            var bound = new Formal4(parameters, Environment);
            if (0 < actuals) bound.Set(0, Evaluate(site, linear, at + linear.Nodes[at++]));
            if (1 < actuals) bound.Set(1, Evaluate(site, linear, at + linear.Nodes[at++]));
            if (2 < actuals) bound.Set(2, Evaluate(site, linear, at + linear.Nodes[at++]));
            if (3 < actuals) bound.Set(3, Evaluate(site, linear, at + linear.Nodes[at]));
            return Evaluate(bound, linear, Body);
        }
        public Closure4(IEnvironment environment, Symbol[] parameters, Linear linear, int body) : base(environment, parameters, linear, body) { }
        public override object Invoke(params object[] arguments) => Evaluate(new Formal4(Parameters, arguments, Environment), linear, Body);
    }
    public sealed class Closure5 : Closure
    {
        internal override object Inline(IEnvironment site, int at, int actuals)
        {
            var parameters = Parameters;
            var bound = new Formal5(parameters, Environment);
            if (0 < actuals) bound.Set(0, Evaluate(site, linear, at + linear.Nodes[at++]));
            if (1 < actuals) bound.Set(1, Evaluate(site, linear, at + linear.Nodes[at++]));
            if (2 < actuals) bound.Set(2, Evaluate(site, linear, at + linear.Nodes[at++]));
            if (3 < actuals) bound.Set(3, Evaluate(site, linear, at + linear.Nodes[at++]));
            if (4 < actuals) bound.Set(4, Evaluate(site, linear, at + linear.Nodes[at]));
            return Evaluate(bound, linear, Body);
        }
        public Closure5(IEnvironment environment, Symbol[] parameters, Linear linear, int body) : base(environment, parameters, linear, body) { }
        public override object Invoke(params object[] arguments) => Evaluate(new Formal5(Parameters, arguments, Environment), linear, Body);
    }
    public delegate object Evaluation(IEnvironment environment, Linear linear, int at);
    public class LinearEvaluator
    {
        protected static object Rehydrate(Linear linear, int at)
        {
            var nodes = linear.Nodes;
            var it = at;
            if (0 < nodes[it++])
            {
                var length = nodes[it];
                var array = new object[length];
                var arg = it;
                var i = 0;
                while (0 < (it = nodes[++arg])) array[i++] = Rehydrate(linear, it += arg);
                return array;
            }
            else
            {
                return 0 < (it = nodes[it]) || -it < nodes[0] ? new Symbol(it) : linear.Value[-it];
            }
        }
        protected static object Evaluate(IEnvironment environment, Linear linear, int at)
        {
            var wired = linear.Wired;
            var evaluate = wired[at];
            if (evaluate != null)
            {
                return evaluate(environment, linear, at);
            }
            var nodes = linear.Nodes;
            var it = at;
            if (0 < nodes[it++])
            {
                int arity;
                if (0 < (arity = nodes[it]))
                {
                    var suffix = Math.Min(arity, 2);
                    var value = linear.Value;
                    int arg = it, exp = ++it;
                    var i = -1;
                    while (++i < suffix && 0 < (it = nodes[++arg]))
                    {
                        if (nodes[it += arg] == 0 && (it = nodes[it + 1]) < -2 && (it = -it) < nodes[0] && value[it] != null)
                        {
                            wired[at] = evaluate = (Evaluation)value[it]; break;
                        }
                    }
                    return
                        evaluate != null ?
                        evaluate(environment, linear, at) :
                        Evaluate(environment, linear, exp + nodes[exp]) is Closure closure ? closure.Inline(environment, exp + 1, arity - 1) : Rehydrate(linear, at);
                }
                else
                {
                    return Environment.Empty;
                }
            }
            else
            {
                return 0 < (it = nodes[it]) ? environment.Get(new Symbol(it)) : linear.Value[-it];
            }
        }
        public object Rehydrate(Linear linear) => Rehydrate(linear, 1);
        public Linear Linearize(IEnvironment environment, object expression)
        {
            List<int> LinearizeAtom(List<object> v, object a)
            {
                int found;
                if (!(a is Symbol s))
                {
                    found = v.IndexOf(a);
                    if (found < 0)
                    {
                        v.Add(a);
                        found = v.Count - 1;
                    }
                    found = 0 < found ? -found : found;
                }
                else
                {
                    found = s.Id;
                }
                return new List<int> { 0, found };
            }
            List<int> LinearizeArray(List<object> v, object[] a, bool isLambda)
            {
                var length = a.Length;
                var list = new List<int> { 0, length };
                var data = new List<List<int>>();
                var head = length + 1;
                var size = head + 2;
                int last = 0;
                for (var i = 0; i < length; i++)
                {
                    var item = isLambda && i == 0 ? LinearizeAtom(v, ((object[])a[i]).Cast<Symbol>().ToArray()) : Linearize(v, a[i]);
                    data.Add(item);
                    size += item.Count;
                }
                for (var i = 0; i < length; i++) list.Add(0);
                list.Add(-1);
                for (var i = 0; i < length; i++) { list[i + 2] = head-- + last; last += data[i].Count; }
                for (var i = 0; i < length; i++) list.AddRange(data[i]);
                list[0] = size;
                return list;
            }
            List<int> Linearize(List<object> v, object o) =>
                o is object[] a ?
                LinearizeArray(v, a, a.Length == 3 && Symbol.Lambda.Equals(a[1]))
                :
                LinearizeAtom(v, o);
            var nodes = new List<int> { environment.Global.Builtins.Count };
            var value = new List<object>(new object[3] { Symbol.Undefined, null, typeof(void) }.Concat(environment.Global.Builtins.Skip(3).ToArray()));
            nodes.AddRange(Linearize(value, expression));
            return new Linear(nodes.ToArray(), value.ToArray(), new Evaluation[nodes.Count]);
        }
        public object Evaluate(IEnvironment environment, Linear linear) => Evaluate(environment, linear, 1);
    }
    public class Interpreter : LinearEvaluator
    {
        protected static object Quotation(IEnvironment environment, Linear linear, int at) => Rehydrate(linear, at + 7);
        protected static object Definition(IEnvironment environment, Linear linear, int at)
        {
            var nodes = linear.Nodes;
            environment = new Environment(environment);
            at += 3;
            var lets = at + nodes[at++];
            var body = at + nodes[at];
            var defs = nodes[++lets];
            if (0 < defs)
            {
                Symbol symbol = default;
                int i = -1, let;
                at = lets;
                while (0 < (let = nodes[++at]))
                {
                    let += at;
                    if ((++i % 2) == 0)
                    {
                        symbol = new Symbol(nodes[let + 1]);
                    }
                    else
                    {
                        environment.Set(symbol, Evaluate(environment, linear, let));
                    }
                }
            }
            return Evaluate(environment, linear, body);
        }
        private static readonly Type[] Closures = new Type[6] { typeof(Closure0), typeof(Closure1), typeof(Closure2), typeof(Closure3), typeof(Closure4), typeof(Closure5) };
        protected static object Abstraction(IEnvironment environment, Linear linear, int at)
        {

            var nodes = linear.Nodes;
            at += 2;
            var head = at + nodes[at++]; at++; var body = at + nodes[at]; var parameters = (Symbol[])linear.Value[-nodes[head + 1]];
            return Activator.CreateInstance(Closures[parameters.Length], environment, parameters, linear, body);
        }
        protected virtual object Token(object context, string input, ref int offset, out int length) { length = 0; return Symbol.EOF; }
        protected virtual object Parse(object context, string input, object current, ref int offset, int matched)
        {
            if (Symbol.EOF.Equals(current) || Symbol.Undefined.Equals(current)) throw new Exception($"Unexpected {(Symbol.Undefined.Equals(current) ? $"'{input[offset]}'" : "EOF")} at {offset}");
            else if (Symbol.Quote.Equals(current)) { offset += matched; current = Quoted(Symbol.Quote, Parse(context, input, Token(context, input, ref offset, out matched), ref offset, matched)); }
            else if (Symbol.Open.Equals(current))
            {
                var list = new List<object>(); offset += matched; while (!Symbol.EOF.Equals(current = Token(context, input, ref offset, out matched)) && !Symbol.Undefined.Equals(current) && !Symbol.Close.Equals(current)) list.Add(Parse(context, input, current, ref offset, matched));
                if (!Symbol.EOF.Equals(current) && !Symbol.Undefined.Equals(current)) offset += matched; else throw new Exception($"Unexpected {(Symbol.Undefined.Equals(current) ? $"'{input[offset]}'" : "EOF")} at {offset}"); current = list.ToArray();
            }
            else offset += matched;
            return current;
        }
        protected virtual Global AsGlobal(IEnvironment environment) => (Global)environment.Set(Symbol.Quote, (Evaluation)Quotation).Set(Symbol.Let, (Evaluation)Definition).Set(Symbol.Lambda, (Evaluation)Abstraction);
        public object Quoted(Symbol quote, object expression) => new object[2] { quote, expression };
        public bool IsQuoted(object expression, out Symbol quote) => (expression is object[] list && list.Length == 2 && Symbol.Quote.Equals(quote = list[0] is Symbol symbol ? symbol : Symbol.Undefined)) || Symbol.Quote.Equals(quote = Symbol.Undefined);
        public object Evaluate(IEnvironment environment, object expression) => base.Evaluate(environment, Linearize(environment, expression));
        public object Evaluate(IEnvironment environment, string input) => Evaluate(environment, input, out _);
        public object Evaluate(IEnvironment environment, string input, out Global global) => Evaluate(environment, input, out global, out _);
        public object Evaluate(IEnvironment environment, string input, out Global global, out object parsed) => Evaluate(global = AsGlobal(environment ?? new Global(GetSymbolProvider(null))), parsed = Parse(global, input));
        public virtual SymbolProvider GetSymbolProvider(object context) => (context as SymbolProvider) ?? (context as Global)?.SymbolProvider;
        public virtual object Parse(object context, string input) { var offset = 0; var expression = Parse(context, input, Token(context, input, ref offset, out var matched), ref offset, matched); return Symbol.EOF.Equals(Token(context, input, ref offset, out _)) ? expression : throw new Exception($"Unexpected '{input[offset]}' at {offset}"); }
        public virtual string Print(object context, object expression) { var symbols = GetSymbolProvider(context); var sequence = Environment.Sequence(expression); var nonEmpty = sequence != null && sequence.Any(); return sequence != null ? (!IsQuoted(expression, out var quotedWith) ? $"( {string.Join('\x20', sequence.Select(value => Print(context, value)))}{(nonEmpty ? " )" : ")")}" : $"{symbols.NameOf(quotedWith)}{Print(context, ((object[])expression)[1])}") : expression is Symbol ? symbols.NameOf((Symbol)expression) : Print(context, symbols.Get($"[:{expression?.GetType() ?? typeof(void)}]")); }
    }
}

class Program
{
    // An interpreter which knows whitespace, signed 64bit integers, and the core lambda calculus (let and lambda forms only),
    // but no bindings of programmer-defined identifiers yet
    public class DerivedInterpreter : Interpreter
    {
        private SymbolProvider symbolProvider;
        protected static readonly Regex WhiteSpace = new Regex("\\s+", RegexOptions.Compiled);
        protected static readonly Regex Integer = new Regex("\\-?(0|[1-9]+[0-9]*)", RegexOptions.Compiled);

        protected override object Token(object context, string input, ref int offset, out int length)
        {
            var symbols = GetSymbolProvider(context);
            Match match;
            length = 0;
            while ((match = WhiteSpace.Match(input, offset)).Success && match.Index == offset) offset += match.Length;
            if (offset < input.Length)
            {
                Symbol builtin;
                if (char.IsDigit(input[offset]) || (input[offset] == '-' && offset < input.Length - 1 && char.IsDigit(input[offset + 1])))
                {
                    match = Integer.Match(input, offset);
                    length = match.Length;
                    return long.Parse(match.Value);
                }
                if ((builtin = symbols.Builtin(input, offset)).Id < 0)
                {
                    length = symbols.NameOf(builtin).Length;
                    return builtin;
                }
                return Symbol.Undefined;
            }
            return Symbol.EOF;
        }

        public override SymbolProvider GetSymbolProvider(object context) => base.GetSymbolProvider(context) ?? symbolProvider ?? (symbolProvider = new SymbolProvider());

        public override string Print(object context, object expression) => expression is int n ? $"{n}" : base.Print(context, expression);
    }

    // An interpreter which knows: whitespace, signed 64bit integers, the five operations (+, -, *, /, and %),
    // the core lambda calculus (let and lambda forms only), the ternary if-then-else conditional operator - ie: ( condition ? ifTrue : ifFalse ) -
    // and lexically-scoped bindings of programmer-defined identifiers, as well as lexically-scoped variable assignments
    // (eg, for impure, side effectful functions);
    // this evaluator thus allows to implement the recursive factorial or Fibonacci sequence function, and more...
    public class LinearLisp : DerivedInterpreter
    {
        private Symbol brackets, atSign, comma, plus, minus, times, divideBy, percent, lessThan, query, colon, equal, @for, @while;
        protected static readonly Regex Literal = new Regex("\"(\\\\\"|[^\"])*\"", RegexOptions.Compiled);
        protected static readonly Regex Identifier = new Regex("[A-Za-z_][A-Za-z_0-9]*", RegexOptions.Compiled);

        protected static object NewArray(IEnvironment environment, Linear linear, int at)
        {
            var nodes = linear.Nodes;
            at += 1;
            int token = nodes[at++];
            var length = token - 1;
            object[] array;
            if (token == 3 && nodes[token = (token = at + 1) + nodes[token]] == 0 && (token = nodes[token + 1]) < -2 && -token < nodes[0])
            {
                at += 2;
                array = new object[System.Convert.ToInt32(Evaluate(environment, linear, at + nodes[at]))];
            }
            else
            {
                var i = 0;
                int it;
                array = new object[length];
                while (0 < (it = nodes[++at])) array[i++] = Evaluate(environment, linear, it += at);
            }
            return array;
        }

        protected static object Access(IEnvironment environment, Linear linear, int at)
        {
            var trace = linear.Trace;
            int t;
            if (0 < trace[t = at])
            {
                var tarray = (System.Array)Evaluate(environment, linear, trace[t++]);
                var tindex = System.Convert.ToInt32(Evaluate(environment, linear, trace[t]));
                return tarray.GetValue(tindex);
            }
            var nodes = linear.Nodes;
            at += 2;
            var left = at + nodes[at++];
            at++;
            var right = at + nodes[at];
            var array = (System.Array)Evaluate(environment, linear, left);
            var index = System.Convert.ToInt32(Evaluate(environment, linear, right));
            trace[t++] = left;
            trace[t] = right;
            return array.GetValue(index);
        }

        protected static object Enumeration(IEnvironment environment, Linear linear, int at)
        {
            var nodes = linear.Nodes;
            object last;
            at += 2;
            while (true) { last = Evaluate(environment, linear, at + nodes[at]); if (0 < nodes[at + 1]) at += 2; else break; }
            return last;
        }

        protected static object Addition(IEnvironment environment, Linear linear, int at)
        {
            var trace = linear.Trace;
            int t;
            if (0 < trace[t = at])
            {
                return (long)Evaluate(environment, linear, trace[t++]) + (long)Evaluate(environment, linear, trace[t]);
            }
            var nodes = linear.Nodes;
            at += 2;
            var left = at + nodes[at++];
            at++;
            var right = at + nodes[at];
            trace[t++] = left;
            trace[t] = right;
            return (long)Evaluate(environment, linear, left) + (long)Evaluate(environment, linear, right);
        }

        protected static object Subtraction(IEnvironment environment, Linear linear, int at)
        {
            var trace = linear.Trace;
            int t;
            if (0 < trace[t = at])
            {
                return (long)Evaluate(environment, linear, trace[t++]) - (long)Evaluate(environment, linear, trace[t]);
            }
            var nodes = linear.Nodes;
            at += 2;
            var left = at + nodes[at++];
            at++;
            var right = at + nodes[at];
            trace[t++] = left;
            trace[t] = right;
            return (long)Evaluate(environment, linear, left) - (long)Evaluate(environment, linear, right);
        }

        protected static object Multiplication(IEnvironment environment, Linear linear, int at)
        {
            var trace = linear.Trace;
            int t;
            if (0 < trace[t = at])
            {
                return (long)Evaluate(environment, linear, trace[t++]) * (long)Evaluate(environment, linear, trace[t]);
            }
            var nodes = linear.Nodes;
            at += 2;
            var left = at + nodes[at++];
            at++;
            var right = at + nodes[at];
            trace[t++] = left;
            trace[t] = right;
            return (long)Evaluate(environment, linear, left) * (long)Evaluate(environment, linear, right);
        }

        protected static object Division(IEnvironment environment, Linear linear, int at)
        {
            var trace = linear.Trace;
            int t;
            if (0 < trace[t = at])
            {
                return (long)Evaluate(environment, linear, trace[t++]) / (long)Evaluate(environment, linear, trace[t]);
            }
            var nodes = linear.Nodes;
            at += 2;
            var left = at + nodes[at++];
            at++;
            var right = at + nodes[at];
            trace[t++] = left;
            trace[t] = right;
            return (long)Evaluate(environment, linear, left) / (long)Evaluate(environment, linear, right);
        }

        protected static object Modulus(IEnvironment environment, Linear linear, int at)
        {
            var trace = linear.Trace;
            int t;
            if (0 < trace[t = at])
            {
                return (long)Evaluate(environment, linear, trace[t++]) % (long)Evaluate(environment, linear, trace[t]);
            }
            var nodes = linear.Nodes;
            at += 2;
            var left = at + nodes[at++];
            at++;
            var right = at + nodes[at];
            trace[t++] = left;
            trace[t] = right;
            return (long)Evaluate(environment, linear, left) % (long)Evaluate(environment, linear, right);
        }

        protected static object IsLessThan(IEnvironment environment, Linear linear, int at)
        {
            var trace = linear.Trace;
            int t;
            if (0 < trace[t = at])
            {
                return (long)Evaluate(environment, linear, trace[t++]) < (long)Evaluate(environment, linear, trace[t]);
            }
            var nodes = linear.Nodes;
            at += 2;
            var left = at + nodes[at++];
            at++;
            var right = at + nodes[at];
            trace[t++] = left;
            trace[t] = right;
            return (long)Evaluate(environment, linear, left) < (long)Evaluate(environment, linear, right);
        }

        protected static object IfThenElse(IEnvironment environment, Linear linear, int at)
        {
            var trace = linear.Trace;
            int t;
            if (0 < trace[t = at])
            {
                var ttest = trace[t++];
                var tthen = trace[t++];
                var telse = trace[t];
                return (bool)Evaluate(environment, linear, ttest) ? Evaluate(environment, linear, tthen) : Evaluate(environment, linear, telse);
            }
            var nodes = linear.Nodes;
            at += 2;
            var test = at + nodes[at++];
            at++;
            var then = at + nodes[at++];
            at++;
            var @else = at + nodes[at];
            trace[t++] = test;
            trace[t++] = then;
            trace[t] = @else;
            return (bool)Evaluate(environment, linear, test) ? Evaluate(environment, linear, then) : Evaluate(environment, linear, @else);
        }

        protected static object Assignment(IEnvironment environment, Linear linear, int at)
        {
            var nodes = linear.Nodes;
            var trace = linear.Trace;
            Symbol symbol;
            object result;
            int t;
            if (0 < linear.Trace[t = at])
            {
                result = Evaluate(environment, linear, trace[t++]);
                symbol = new Symbol(trace[t++]);
                if (0 <= (t = trace[t]))
                {
                    var index = System.Convert.ToInt32(Evaluate(environment, linear, t));
                    var array = (System.Array)environment.Get(symbol);
                    array.SetValue(result, index);
                }
                else
                {
                    environment.Set(symbol, result);
                }
                return result;
            }
            at += 2;
            var left = at + nodes[at++];
            var oper = at + nodes[at++];
            var right = at + nodes[at];
            result = Evaluate(environment, linear, right);
            if (left < oper - 2)
            {
                trace[t++] = right;
                at += 4;
                symbol = new Symbol(trace[t++] = nodes[at + nodes[at++] + 1]);
                at++;
                var index = System.Convert.ToInt32(Evaluate(environment, linear, trace[t] = at + nodes[at]));
                var array = (System.Array)environment.Get(symbol);
                array.SetValue(result, index);
            }
            else
            {
                trace[t++] = right;
                symbol = new Symbol(trace[t++] = nodes[left + 1]);
                trace[t] = -1;
                environment.Set(symbol, result);
            }
            return result;
        }

        protected static object ForLoop(IEnvironment environment, Linear linear, int at)
        {
            var nodes = linear.Nodes;
            at += 3;
            var bound = at + nodes[at++];
            at++;
            var start = at + nodes[at++];
            at++;
            var count = at + nodes[at++];
            at++;
            var body = at + nodes[at];
            var iter = new Formal1(new Symbol[1] { new Symbol(nodes[bound + 1]) }, environment);
            var from = (long)Evaluate(environment, linear, start);
            var take = (long)Evaluate(environment, linear, count);
            object last = Symbol.Undefined;
            take += from;
            for (var i = from; i < take; i++) last = Evaluate(iter.Set(0, i), linear, body);
            return last;
        }

        protected static object WhileLoop(IEnvironment environment, Linear linear, int at)
        {
            var nodes = linear.Nodes;
            at += 3;
            var test = at + nodes[at++];
            var body = at + nodes[at];
            object last = Symbol.Undefined;
            while ((bool)Evaluate(environment, linear, test)) last = Evaluate(environment, linear, body);
            return last;
        }

        protected override object Token(object context, string input, ref int offset, out int length)
        {
            var symbols = GetSymbolProvider(context);
            Match match;
            length = 0;
            while ((match = WhiteSpace.Match(input, offset)).Success && match.Index == offset) offset += match.Length;
            if (offset < input.Length)
            {
                Symbol builtin;
                if (char.IsDigit(input[offset]) || (input[offset] == '-' && offset < input.Length - 1 && char.IsDigit(input[offset + 1])))
                {
                    match = Integer.Match(input, offset);
                    length = match.Length;
                    return long.Parse(match.Value);
                }
                if ((match = Literal.Match(input, offset)).Success && match.Index == offset)
                {
                    length = match.Value.Length;
                    return match.Value.Substring(1, match.Value.Length - 2).Replace("\\\"", "\"");
                }
                if (char.IsLetter(input[offset]) || input[offset] == '_')
                {
                    match = Identifier.Match(input, offset);
                    length = match.Length;
                    return match.Value != "null" ? symbols.Get(match.Value) : null;
                }
                if ((builtin = symbols.Builtin(input, offset)).Id < 0)
                {
                    length = symbols.NameOf(builtin).Length;
                    return builtin;
                }
                return Symbol.Undefined;
            }
            return Symbol.EOF;
        }

        protected override Global AsGlobal(IEnvironment environment) => !base.AsGlobal(environment).Knows(Times) ? (Global)environment
            .Set(Brackets, (Evaluation)NewArray).Set(AtSign, (Evaluation)Access).Set(Comma, (Evaluation)Enumeration)
            .Set(Plus, (Evaluation)Addition).Set(Minus, (Evaluation)Subtraction).Set(Times, (Evaluation)Multiplication).Set(DivideBy, (Evaluation)Division).Set(Percent, (Evaluation)Modulus)
            .Set(LessThan, (Evaluation)IsLessThan).Set(Query, (Evaluation)IfThenElse)
            .Set(Equal, (Evaluation)Assignment).Set(For, (Evaluation)ForLoop).Set(While, (Evaluation)WhileLoop)
            :
            (Global)environment;

        public override SymbolProvider GetSymbolProvider(object context) => base.GetSymbolProvider(context)
            .Builtin("[]", out brackets).Builtin("@", out atSign).Builtin(",", out comma)
            .Builtin("+", out plus).Builtin("-", out minus).Builtin("*", out times).Builtin("/", out divideBy).Builtin("%", out percent)
            .Builtin("<", out lessThan).Builtin("?", out query).Builtin(":", out colon)
            .Builtin("=", out equal).Builtin("for", out @for).Builtin("while", out @while);

        public override string Print(object context, object expression) => expression is string s ? $"\"{s.Replace("\"", "\\\"")}\"" : expression != null ? base.Print(context, expression) : "null";

        public Symbol Brackets => brackets;

        public Symbol AtSign => atSign;

        public Symbol Comma => comma;

        public Symbol Plus => plus;

        public Symbol Minus => minus;

        public Symbol Times => times;

        public Symbol DivideBy => divideBy;

        public Symbol Percent => percent;

        public Symbol LessThan => lessThan;

        public Symbol Query => query;

        public Symbol Colon => colon;

        public Symbol Equal => equal;

        public Symbol For => @for;

        public Symbol While => @while;
    }

    static long CompiledFactorial(long n) => 0 < n ? n * CompiledFactorial(n - 1) : 1;

    static long CompiledFib(long n) =>
        n < 2 ? n : CompiledFib(n - 1) + CompiledFib(n - 2);

    static void Main(string[] args)
    {
        const int N = 10_000_000;

        var evaluator = new LinearLisp();

        System.Console.WriteLine("( For history, see also: http://dada.perl.it/shootout/fibo.html )");
        System.Console.WriteLine();
        System.Console.WriteLine("Press a key to start...");
        System.Console.ReadKey();

        // Cf. https://news.ycombinator.com/item?id=31427506
        // (Python 3.10 vs JavaScript thread)
        System.Console.WriteLine($"LinearLisp... About to stress the for loop vs lexical scope {N} times...");
        System.Console.WriteLine();
        System.Console.WriteLine("Press a key to continue...");
        System.Console.ReadKey(true);
        var sw = System.Diagnostics.Stopwatch.StartNew();
        long ms;
        var acc = (long)evaluator.Evaluate(null, $@"(
    let
    (
        N {N}

        acc 0

        f ( ( x ) => ( acc + ( 10 * x ) ) )
    )

    ( for i = 0, N :
        ( acc = ( f i ) )
    )
)");
        sw.Stop();
        ms = sw.ElapsedMilliseconds;
        System.Console.WriteLine($"acc = {acc} ... in {ms} ms");

        System.Diagnostics.Debug.Assert(acc == 499_999_950_000_000);

        System.Console.WriteLine();
        System.Console.WriteLine("C# compiled vs LinearLisp, with Fib(32) computed only once, and 20! computed 1,000,000 times...");
        System.Console.WriteLine("Press a key to continue...");
        System.Console.ReadKey(true);

        var result = evaluator.Evaluate(null, @"(
    let
    (
        Fib ( ( n ) => ( ( n < 2 ) ? n : ( ( Fib ( n - 1 ) ) + ( Fib ( n - 2 ) ) ) ) )
    )

    Fib
)  ", out var global);
        var closure = (Closure)result;

        sw = System.Diagnostics.Stopwatch.StartNew();
        var fib32 = CompiledFib(32);
        sw.Stop();
        ms = sw.ElapsedMilliseconds;
        System.Console.WriteLine($"(C# compiled) Fib(32) = {fib32} ... in {ms:0,0} ms");

        System.Console.WriteLine();
        System.Console.WriteLine("Press a key to continue...");
        System.Console.ReadKey(true);

        sw = System.Diagnostics.Stopwatch.StartNew();
        var fib32bis = (long)closure.Invoke(32L);
        sw.Stop();
        ms = sw.ElapsedMilliseconds;
        System.Console.WriteLine($"(LinearLisp) Fib(32) = {fib32bis} ... in {ms:0,0} ms");

        System.Diagnostics.Debug.Assert(fib32 == fib32bis);

        System.Console.WriteLine();
        System.Console.WriteLine("Press a key to continue...");
        System.Console.ReadKey(true);

        const int N_fact = 1_000_000;

        closure = (Closure)evaluator.Evaluate(global, @"(
    let
    (
        Fact ( ( n ) => ( ( 1 < n ) ? ( n * ( Fact ( n - 1 ) ) ) : 1 ) )
    )

    Fact
)");

        var sw1 = System.Diagnostics.Stopwatch.StartNew();
        long fact20 = 0;
        for (var i = 0; i < N_fact; i++)
        {
            fact20 = CompiledFactorial(20);
        }
        sw1.Stop();
        var elapsed = sw1.ElapsedMilliseconds;
        System.Console.WriteLine($"(C# compiled) 20! = {fact20} x {N_fact:0,0} times ... in {elapsed:0,0} ms");

        System.Console.WriteLine();
        System.Console.WriteLine("Press a key to continue...");
        System.Console.ReadKey(true);

        var sw2 = System.Diagnostics.Stopwatch.StartNew();
        long fact20bis = 0;
        for (var i = 0; i < N_fact; i++)
        {
            fact20bis = (long)closure.Invoke(20L);
        }
        sw2.Stop();
        elapsed = sw2.ElapsedMilliseconds;
        System.Console.WriteLine($"(LinearLisp) 20! = {fact20bis} x {N_fact:0,0} times ... in {elapsed:0,0} ms");
        System.Diagnostics.Debug.Assert(fact20 == fact20bis);

        System.Console.WriteLine();
        System.Console.WriteLine("Press a key... (to exit LinearLisp)");
        System.Console.ReadKey(true);
    }
}
